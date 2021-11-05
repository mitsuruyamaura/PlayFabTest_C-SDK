using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class User
{
    public int level;
    public bool tutorialFlag;

    /// <summary>
    /// 新規ユーザーの作成
    /// </summary>
    /// <returns></returns>
    public static User Create() {
        User user = new User {
            level = 0,
            tutorialFlag = false
        };

        Debug.Log("新規ユーザーの作成");

        return user;
    }
}
