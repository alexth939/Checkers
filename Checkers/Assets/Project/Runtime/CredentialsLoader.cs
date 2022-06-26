using UnityEngine;
using ProjectDefaults;

namespace Runtime
{
     public sealed class CredentialsLoader
     {
          public void Deconstruct(out string email, out string password)
          {
               email = TryLoadLastEmail();
               password = TryLoadLastPassword();
          }

          private string TryLoadLastEmail()
          {
               return PlayerPrefs.GetString(ProjectConstants.LastUsedEmailKey, defaultValue: string.Empty);
          }

          private string TryLoadLastPassword()
          {
               return PlayerPrefs.GetString(ProjectConstants.LastUsedPasswordKey, defaultValue: string.Empty);
          }
     }
}
