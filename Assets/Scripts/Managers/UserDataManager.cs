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
    public static User User { get; set; }


    public static Dictionary<string, Character> Characters => User.Characters;

    // TODO Level などの情報を持たせる


    /// <summary>
    /// プレイヤーデータの作成と更新(プレイヤーデータ(タイトル)に１つだけ登録する方法)
    /// </summary>
    /// <param name="updateUserName">key</param>
    /// <param name="value">value</param>
    /// <param name="userDataPermission"></param>
    public static async UniTask UpdatePlayerDataAsync(Dictionary<string, string> updateUserData, UserDataPermission userDataPermission = UserDataPermission.Private) {

        var request = new UpdateUserDataRequest {
            Data = updateUserData,

            // アクセス許可の変更
            Permission = userDataPermission
        };

        var response = await PlayFabClientAPI.UpdateUserDataAsync(request);

        if (response.Error != null) {

            Debug.Log("エラー");
            return;
        }

        Debug.Log("プレイヤーデータ　更新");
    }

    /// <summary>
    /// プレイヤーデータの削除
    /// </summary>
    /// <param name="deleteUserName">削除するユーザーの名前</param>
    public static async void DeletePlayerDataAsync(string deleteUserName) {

        var request = new UpdateUserDataRequest {
            KeysToRemove = new List<string> { deleteUserName }
        };

        var response = await PlayFabClientAPI.UpdateUserDataAsync(request);

        if (response.Error != null) {

            Debug.Log("エラー");
            return;
        }

        Debug.Log("プレイヤーデータ　削除");
    }

    /// <summary>
    /// PlayFab のユーザーデータの更新(Json を利用する場合)
    /// </summary>
    /// <param name="userName">key</param>
    /// <param name="userDataPermission"></param>
    /// <returns></returns>
    public static async UniTask<(bool isSuccess, string errorMessage)> UpdateUserDataByJsonAsync(string userName, UserDataPermission userDataPermission = UserDataPermission.Private) {

        var userJson = JsonConvert.SerializeObject(User);

        var request = new UpdateUserDataRequest {
            Data = new Dictionary<string, string> {
                { userName, userJson }
            },

            // アクセス許可の変更
            Permission = userDataPermission
        };

        var response = await PlayFabClientAPI.UpdateUserDataAsync(request);

        if (response.Error != null) {

            Debug.Log("エラー");
        }

        return (true, string.Empty);
    }

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

        // インベントリに所持しているアイテムの情報から、Character クラスのアイテム情報を検索
        User.Characters = InventoryManager.Characters.ToDictionary(
            x => x.InstanceId,
            x => 
            {
                if (User.Characters is null) {
                    return Character.CreateChara(x.InstanceId, x.CharacterId);
                }

                return User.Characters.TryGetValue(x.InstanceId, out var character)
                ? character
                : Character.CreateChara(x.InstanceId, x.CharacterId);
            });

        Debug.Log($"所持している Chara : { User.Characters.Count } 体");

        GameData.instance.SetInventoryDatas();

        // TODO 他にも処理があれば追加

        // Debug (メソッドに async を追加する)
        //(bool isSuccess, string errorMessage) updateUser = await UpdateUserDataAsync();

        //if (updateUser.isSuccess) {
        //    Debug.Log("PlayFab のユーザーデータ更新完了");
        //}
    }
}
