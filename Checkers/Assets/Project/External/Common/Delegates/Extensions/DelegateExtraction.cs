using System;

namespace External.DelegateExtensions
{
     public static class CoolParameterExtractionMethods
     {
          public static void Extract<T>(this Action<T> archive, out T result) where T : new()
          {
               result = new T();
               archive.Invoke(result);
          }

          public static T Extract<T>(this Action<T> archive) where T : new()
          {
               T result = new T();
               archive.Invoke(result);
               return result;
          }

          public static void Extract<T1, T2>(this Action<T1, T2> archive, out T1 result1, out T2 result2)
               where T1 : new()
               where T2 : new()
          {
               result1 = new T1();
               result2 = new T2();
               archive.Invoke(result1, result2);
          }

          public static (T1, T2) Extract<T1, T2>(this Action<T1, T2> archive)
               where T1 : new()
               where T2 : new()
          {
               T1 result1 = new T1();
               T2 result2 = new T2();
               archive.Invoke(result1, result2);
               return (result1, result2);
          }
     }
}
