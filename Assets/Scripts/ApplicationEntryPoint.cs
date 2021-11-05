using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cysharp.Threading.Tasks;
using System;
using System.Linq;
using System.Text;


public static class ApplicationEntryPoint
{
    public static bool Initialized { get; private set; }

    [RuntimeInitializeOnLoadMethod(RuntimeInitializeLoadType.BeforeSceneLoad)]
    private static async UniTaskVoid InitializeAsync() {
        //Application.logMessageReceivedThreaded +=

        Debug.Log("初期化開始");

        await LoginManager.LoginAndUpdateLocalCacheAsync();

        Debug.Log("初期化完了");

        //if (string.IsNullOrEmpty(PlayerProfileManager.UserDisplayName)) {
        //    Debug.Log("ユーザー名が登録されていないため、強制的に TitleScene から起動します");
        //}

        // Debug モード中は、指定したシーンから開始
        if (GameData.instance.isDebugOn) {
            SceneStateManager.NextScene(GameData.instance.debugSceneName);

            //SceneStateManager.NextScene(SceneName.Main);
        }

        Initialized = true;
    }
}
