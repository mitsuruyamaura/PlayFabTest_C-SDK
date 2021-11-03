using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using PlayFab;
using PlayFab.ClientModels;
using System.Threading.Tasks;

public class PlayFabLogin : MonoBehaviour
{
    async void Start() {

        PlayFabSettings.staticSettings.TitleId = "DDCD3";

        // ログインの情報(リクエスト)を作成して設定
        var request = new LoginWithCustomIDRequest {
            CustomId = "GettingStartedGuide",
            CreateAccount = true
        };

        // PlayFab へログイン。情報が確認できるまで待機
        var result = await PlayFabClientAPI.LoginWithCustomIDAsync(request);

        // エラーの内容を見てハンドリングを行い、ログインに成功しているかを判定
        var message = result.Error is null ? $"Login success! My PlayFabID is {result.Result.PlayFabId}" : result.Error.GenerateErrorReport();

        Debug.Log(message);
    }
}
