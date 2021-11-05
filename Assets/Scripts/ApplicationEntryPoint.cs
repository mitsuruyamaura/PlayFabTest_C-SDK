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

        Debug.Log("�������J�n");

        await LoginManager.LoginAndUpdateLocalCacheAsync();

        Debug.Log("����������");

        //if (string.IsNullOrEmpty(PlayerProfileManager.UserDisplayName)) {
        //    Debug.Log("���[�U�[�����o�^����Ă��Ȃ����߁A�����I�� TitleScene ����N�����܂�");
        //}

        // Debug ���[�h���́A�w�肵���V�[������J�n
        if (GameData.instance.isDebugOn) {
            SceneStateManager.NextScene(GameData.instance.debugSceneName);

            //SceneStateManager.NextScene(SceneName.Main);
        }

        Initialized = true;
    }
}
