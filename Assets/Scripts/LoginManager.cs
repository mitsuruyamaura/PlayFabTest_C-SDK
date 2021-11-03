using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using PlayFab;
using PlayFab.ClientModels;
using System.Threading.Tasks;
using System;

public static class LoginManager {

    public static async void PrepareLoginPlayPab() {

        Debug.Log("開始");

        PlayFabSettings.staticSettings.TitleId = "DDCD3";

        await LoginAndUpdateLocalCacheAsync();

        // デバッグ用
        //// ログインの情報(リクエスト)を作成して設定
        //var request = new LoginWithCustomIDRequest {
        //    CustomId = "GettingStartedGuide",
        //    CreateAccount = true
        //};

        //// PlayFab へログイン。情報が確認できるまで待機
        //var result = await PlayFabClientAPI.LoginWithCustomIDAsync(request);

        //// エラーの内容を見てハンドリングを行い、ログインに成功しているかを判定
        //var message = result.Error is null ? $"Login success! My PlayFabID is {result.Result.PlayFabId}" : result.Error.GenerateErrorReport();

        //Debug.Log(message);
    }


    /// <summary>
    /// ユーザーデータとタイトルデータを初期化
    /// </summary>
    /// <returns></returns>
    public static async Task LoginAndUpdateLocalCacheAsync() {

        Debug.Log("初期化開始");

        var userId = PlayerPrefsManager.UserID;

        var loginResult = string.IsNullOrEmpty(userId) ? await CreateNewUserAsync() : await LoadUserAsync(userId);

        // TODO 取得したデータをキャッシュする
        //await UpdateLocalCacheAsync(loginResult);
    }

    /// <summary>
    /// 新規ユーザーを作成して UserId を PlayerPrefs に保存
    /// </summary>
    /// <returns></returns>
    private static async Task<LoginResult> CreateNewUserAsync() {

        Debug.Log("ユーザーデータなし。新規ユーザー作成");

        while (true) {

            // UserId の採番
            var newUserId = Guid.NewGuid().ToString().Replace("-", "").Substring(0, 20);

            // ログインリクエストの作成
            var request = new LoginWithCustomIDRequest {
                CustomId = newUserId,
                CreateAccount = true,
            };

            // PlayFab にログイン
            var response = await PlayFabClientAPI.LoginWithCustomIDAsync(request);

            // エラーハンドリング
            if (response.Error != null) {
                Debug.Log("Error");
            }

            // もしも LastLoginTime に値が入っている場合には、採番した ID が既存ユーザーと重複しているのでリトライする
            if (response.Result.LastLoginTime.HasValue) {
                continue;
            }

            // PlayerPrefs に UserId を記録する
            PlayerPrefsManager.UserID = newUserId;

            return response.Result;
        }
    }

    /// <summary>
    /// ログインしてユーザーデータをロード
    /// </summary>
    /// <param name="userId"></param>
    /// <returns></returns>
    private static async Task<LoginResult> LoadUserAsync(string userId) {

        Debug.Log("ユーザーデータあり。ログイン開始");

        // ログインリクエストの作成
        var request = new LoginWithCustomIDRequest {
            CustomId = userId,
            CreateAccount = false,

            // TODO InfoRequestParameters の設定 

        };

        // PlayFab にログイン
        var response = await PlayFabClientAPI.LoginWithCustomIDAsync(request);

        // エラーハンドリング
        //if (response.Error != null) {
        //    Debug.Log("Error");
        //}

        // エラーの内容を見てハンドリングを行い、ログインに成功しているかを判定
        var message = response.Error is null ? $"Login success! My PlayFabID is {response.Result.PlayFabId}" : response.Error.GenerateErrorReport();

        Debug.Log(message);

        return response.Result;
    }
}
