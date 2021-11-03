using UnityEngine;

/// <summary>
/// PlayerPrefs のヘルパークラス
/// </summary>
public static class PlayerPrefsManager
{
   public static string UserID {
        set {
            PlayerPrefs.SetString("UserID", value);
            PlayerPrefs.Save();
        }
        get => PlayerPrefs.GetString("UserID");
    }
}
