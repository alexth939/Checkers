namespace System.Collections.Generic
{
     public static class IEnumerableExtensions
     {
          public static IEnumerable<T> MergeWith<T>(this IEnumerable<T> baseCollection, params IEnumerable<T>[] otherCollections)
          {
               return new MergedEnumerable<T>(baseCollection, otherCollections);
          }

          public static IEnumerator<T> GetLoopedEnumerator<T>(this IEnumerable<T> origin)
          {
               while(true)
               {
                    var originEnumerator = origin.GetEnumerator();
                    while(originEnumerator.MoveNext())
                    {
                         yield return originEnumerator.Current;
                    }
               }
          }

          public static IEnumerator<T> GetLoopedEnumerator<T>(this IEnumerable<T> origin, Func<bool> isEnough)
          {
               while(true)
               {
                    var originEnumerator = origin.GetEnumerator();
                    while(originEnumerator.MoveNext())
                    {
                         yield return originEnumerator.Current;

                         if(isEnough())
                              yield break;
                    }
               }
          }

          public static IEnumerator<T> GetLoopedEnumerator<T>(this IEnumerable<T> origin, ulong amountLimit)
          {
               while(true)
               {
                    var originEnumerator = origin.GetEnumerator();
                    while(originEnumerator.MoveNext())
                    {
                         yield return originEnumerator.Current;

                         if(--amountLimit == 0)
                              yield break;
                    }
               }
          }

          private sealed class MergedEnumerable<T>: IEnumerable<T>
          {
               private readonly IEnumerable<T> _baseCollection;
               private readonly IEnumerable<T>[] _otherCollections;

               public MergedEnumerable(IEnumerable<T> baseCollection, params IEnumerable<T>[] otherCollections)
               {
                    _baseCollection = baseCollection;
                    _otherCollections = otherCollections;
               }

               public IEnumerator<T> GetEnumerator()
               {
                    IEnumerator<T> baseEnumerator = _baseCollection.GetEnumerator();

                    while(baseEnumerator.MoveNext())
                    {
                         yield return baseEnumerator.Current;
                    }

                    IEnumerator collectionsEnumerator = _otherCollections.GetEnumerator();
                    IEnumerator<T> subEnumerator;

                    while(collectionsEnumerator.MoveNext())
                    {
                         subEnumerator = (collectionsEnumerator.Current as IEnumerable<T>).GetEnumerator();
                         while(subEnumerator.MoveNext() || TryPushNextSubEnumerator())
                         {
                              yield return subEnumerator.Current;
                         }
                    }

                    bool TryPushNextSubEnumerator()
                    {
                         if(collectionsEnumerator.MoveNext() == false)
                              return false;

                         subEnumerator = (collectionsEnumerator.Current as IEnumerable<T>).GetEnumerator();
                         subEnumerator.MoveNext();
                         return true;
                    }
               }

               IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();
          }
     }
}
