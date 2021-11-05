using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cysharp.Threading.Tasks;
using Newtonsoft.Json;
using PlayFab;
using PlayFab.ClientModels;
using System.Linq;

public static class UserDataManager
{
    public static User User { get; private set; }

    // TODO Level などの情報を持たせる


    /// <summary>
    /// PlayFab の最新データを取得してローカルにキャッシュ
    /// </summary>
    /// <param name="userData"></param>
    public static void SyncPlayFabToClient(Dictionary<string, UserDataRecord> userData) {

        // TODO Exp Level などを設定

        // ユーザーの情報を取得。取得できた場合には複合化、取得できない場合には新規ユーザーの作成
        User = userData.TryGetValue("User", out var user) 
            ? JsonConvert.DeserializeObject<User>(user.Value) : User.Create();

        Debug.Log("PlayFab のユーザーデータを取得");

        // TODO 他にも処理があれば追加


        // Debug (メソッドに async を追加する)
        //(bool isSuccess, string errorMessage) updateUser = await UpdateUserDataAsync();

        //if (updateUser.isSuccess) {
        //    Debug.Log("PlayFab のユーザーデータ更新完了");
        //}
    }

    /// <summary>
    /// PlayFab のユーザーデータの更新
    /// </summary>
    /// <returns></returns>
    public static async UniTask<(bool isSuccess, string errorMessage)> UpdateUserDataAsync() {

        var userJson = JsonConvert.SerializeObject(User);

        var request = new UpdateUserDataRequest {
            Data = new Dictionary<string, string> { { "User", userJson } }
        };

        var response = await PlayFabClientAPI.UpdateUserDataAsync(request);

        if (response.Error != null) {

            Debug.Log("エラー");
        }

        return (true, string.Empty);
    }
}
