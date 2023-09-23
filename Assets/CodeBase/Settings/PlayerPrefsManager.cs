using UnityEngine;

namespace CodeBase.Settings
{
    public static class PlayerPrefsManager
    {
        private const string NAME = "name";

        public static void SetName(string name) =>
            PlayerPrefs.SetString(NAME, name);

        public static string GetName() =>
            PlayerPrefs.GetString(NAME);
    }
}