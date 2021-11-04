using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using PlayFab;
using PlayFab.ClientModels;
using System.Threading.Tasks;
using System;
using Cysharp.Threading.Tasks;

public static class LoginManager {

    /// <summary>
    /// コンストラクタ
    /// </summary>
    static LoginManager(){
        // TitleId 設定
        PlayFabSettings.staticSettings.TitleId = "DDCD3";

        Debug.Log("TitleID 設定: " + PlayFabSettings.staticSettings.TitleId);
    }

    /// <summary>
    /// ログインと同時に取得する情報の設定。
    /// InfoRequestParameters の設定値になり、true にしておくことで各情報が自動的に取得できるようになる
    /// 各パラメータの初期値はすべて false
    /// 取得が多くなるほどログイン時間がかかり、メモリを消費するので気を付ける
    /// 取得結果は InfoResultPayLoad に入っている。false のものはすべて null になる
    /// </summary>
    public static GetPlayerCombinedInfoRequestParams CombinedInfoRequestParams { get; }
        = new GetPlayerCombinedInfoRequestParams {
            GetUserAccountInfo = true,
            GetPlayerProfile = true,
            GetTitleData = true,
            GetUserData = true,
            GetUserInventory = true,
            GetUserVirtualCurrency = true,
            GetPlayerStatistics = true
        };

    public static async void PrepareLoginPlayPab() {

        Debug.Log("ログイン 準備 開始");

        await LoginAndUpdateLocalCacheAsync();

        // デバッグ用

        // TitleId 設定
        //PlayFabSettings.staticSettings.TitleId = "DDCD3";

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

        var userId = PlayerPrefsManager.UserId;

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
                InfoRequestParameters = CombinedInfoRequestParams
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
            PlayerPrefsManager.UserId = newUserId;

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
            InfoRequestParameters = CombinedInfoRequestParams
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

    /// <summary>
    /// Email とパスワードでログイン(アカウント回復用)
    /// </summary>
    /// <param name="email"></param>
    /// <param name="password"></param>
    /// <returns></returns>
    public static async Task<(bool, string)> LoginEmailAndPasswordAsync(string email, string password) {

        // Email によるログインリクエストの作成
        var request = new LoginWithEmailAddressRequest {
            Email = email,
            Password = password,
            InfoRequestParameters = CombinedInfoRequestParams
        };

        // PlayFab にログイン
        var response = await PlayFabClientAPI.LoginWithEmailAddressAsync(request);

        // エラーハンドリング
        if (response.Error != null) {
            switch (response.Error.Error) {
                case PlayFabErrorCode.InvalidParams:
                case PlayFabErrorCode.InvalidEmailOrPassword:
                case PlayFabErrorCode.AccountNotFound:
                    Debug.Log("メールアドレスかパスワードが正しくありません");
                    break;
                default:
                    Debug.Log(response.Error.GenerateErrorReport());
                    break;
            }

            return (false, "メールアドレスかパスワードが正しくありません");
        }

        // PlayerPrefas を初期化して、ログイン結果の UserId を登録し直す
        PlayerPrefs.DeleteAll();

        // 新しく PlayFab から UserId を取得
        // InfoResultPayload はクライアントプロフィールオプション(InfoRequestParameters)で許可されてないと null になる
        PlayerPrefsManager.UserId = response.Result.InfoResultPayload.AccountInfo.CustomIdInfo.CustomId;

        // Email でログインしたことを記録する
        PlayerPrefsManager.IsLoginEmailAdress = true;

        return (true, "Email によるログインが完了しました。");
    }
}
