using UnityEngine;

/// <summary>
/// PlayerPrefs �̃w���p�[�N���X
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
    /// ���[���A�h���X�𗘗p���ă��O�C���ς̏ꍇ�� true
    /// </summary>
    public static bool IsLoginEmailAdress {

        set {
            PlayerPrefs.SetString("IsLoginEmailAdress", value.ToString());
            PlayerPrefs.Save();
        }

        // && �Z���]��(�V���[�g�T�[�L�b�g)  && result �́Aresult ? true : false �̕]�����s���Ă���ȗ����� 
        // && �̍��ӂ��U�ł���΁A�E�ӂ͕]�����ꂸ�ɋU�ɂȂ�
        // && �̍��ӂ��^�ł���΁A�E�ӂ��^�Ȃ�^�A���ӂ��U�Ȃ�U�ɂȂ�(�܂�A�������^�ȊO�͂��ׂċU�ɂȂ�)
        // �Z���]���@https://qiita.com/gyu-don/items/a0aed0f94b8b35c43290
        // �u�[���_�����Z�q (C# ���t�@�����X) https://docs.microsoft.com/ja-jp/dotnet/csharp/language-reference/operators/boolean-logical-operators
        get => bool.TryParse(PlayerPrefs.GetString("IsLoginEmailAdress"), out bool result) && result; 
    }
}
