using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class User
{
    public int level;
    public bool tutorialFlag;

    /// <summary>
    /// �V�K���[�U�[�̍쐬
    /// </summary>
    /// <returns></returns>
    public static User Create() {
        User user = new User {
            level = 0,
            tutorialFlag = false
        };

        Debug.Log("�V�K���[�U�[�̍쐬");

        return user;
    }
}
