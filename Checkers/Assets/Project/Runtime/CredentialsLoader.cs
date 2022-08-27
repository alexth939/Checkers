using UnityEngine;
using ProjectDefaults;

namespace Runtime
{
     internal sealed class CredentialsLoader
     {
          internal void Deconstruct(out string email, out string password)
          {
               email = TryLoadLastEmail();
               password = TryLoadLastPassword();
          }

          private string TryLoadLastEmail()
          {
               return PlayerPrefs.GetString(Names.LastUsedEmailKey, defaultValue: string.Empty);
          }

          private string TryLoadLastPassword()
          {
               return PlayerPrefs.GetString(Names.LastUsedPasswordKey, defaultValue: string.Empty);
          }
     }
}
