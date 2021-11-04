using UnityEngine;

/// <summary>
/// PlayerPrefs のヘルパークラス
/// </summary>
public static class PlayerPrefsManager {
    public static string UserID {
        set {
            PlayerPrefs.SetString("UserID", value);
            PlayerPrefs.Save();
        }
        get => PlayerPrefs.GetString("UserID");
    }

    /// <summary>
    /// メールアドレスを利用してログイン済の場合は true
    /// </summary>
    public static bool IsLoginEmailAdress {

        set {
            PlayerPrefs.SetString("IsLoginEmailAdress", value.ToString());
            PlayerPrefs.Save();
        }

        // && 短絡評価(ショートサーキット)  && result は、result ? true : false の評価を行っている省略書式 
        // && の左辺が偽であれば、右辺は評価されずに偽になる
        // && の左辺が真であれば、右辺が真なら真、左辺が偽なら偽になる(つまり、両方が真以外はすべて偽になる)
        // 短絡評価　https://qiita.com/gyu-don/items/a0aed0f94b8b35c43290
        // ブール論理演算子 (C# リファレンス) https://docs.microsoft.com/ja-jp/dotnet/csharp/language-reference/operators/boolean-logical-operators
        get => bool.TryParse(PlayerPrefs.GetString("IsLoginEmailAdress"), out bool result) && result; 
    }
}
