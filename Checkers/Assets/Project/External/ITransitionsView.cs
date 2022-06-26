using System;

namespace External.Views
{
     public interface ITransitionsView
     {
          void FadeInAsync(Action onDone = null);
          void FadeOutAsync(Action onDone = null);
     }
}
