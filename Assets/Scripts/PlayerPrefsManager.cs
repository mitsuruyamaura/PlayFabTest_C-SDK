using UnityEngine;

/// <summary>
/// PlayerPrefs �̃w���p�[�N���X
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
