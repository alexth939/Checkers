namespace External.Common
{
     public sealed class NullMethods
     {
          public static bool AnyNull(params object[] objects)
          {
               var objectsEnumerator = objects.GetEnumerator();
               while(objectsEnumerator.MoveNext())
                    if(objectsEnumerator.Current == null)
                         return true;
               return false;
          }
     }
}
