using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using PlayFab;
using PlayFab.ClientModels;
using System.Threading.Tasks;

public static class PlayFabAccountLink
{
    public static async Task<bool> SetEmailAndPasswordAsync(string email, string password) {

        var request = new AddUsernamePasswordRequest {
            Username = PlayerPrefsManager.UserID,
            Email = email,
            Password = password
        };

        var response = await PlayFabClientAPI.AddUsernamePasswordAsync(request);

        if (response.Error != null) {
            switch (response.Error.Error) {
                case PlayFabErrorCode.InvalidParams:
                    Debug.Log("有効なメールアドレスと、6～100文字以内のパスワードを入力し直してください。");
                    break;
                case PlayFabErrorCode.EmailAddressNotAvailable:
                    Debug.Log("このメールアドレスはすでに使用されています。");
                    break;
                case PlayFabErrorCode.InvalidEmailAddress:
                    Debug.Log("このメールアドレスは使用出来ません。");
                    break;
                case PlayFabErrorCode.InvalidPassword:
                    Debug.Log("このパスワードは無効です。");
                    break;
                default:
                    Debug.Log(response.Error.GenerateErrorReport());
                    break;
            }

            return false;
        } else {

            Debug.Log("Email とパスワードの登録完了");

            return true;
        }
    }
}
