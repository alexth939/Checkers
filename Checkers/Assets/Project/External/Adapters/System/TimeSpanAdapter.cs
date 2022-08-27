using System;

namespace System
{
     public struct TimeSpanAdapter
     {
          public static TimeSpan New(int seconds) => new TimeSpan(hours: 0, minutes: 0, seconds: seconds);
          public static TimeSpan New(float seconds)
          {
               int wholeSeconds = (int)seconds;
               int milliseconds = (int)Math.Round((seconds - wholeSeconds) * 1000);

               return new TimeSpan(0, 0, 0, wholeSeconds, milliseconds);
          }
     }
}
