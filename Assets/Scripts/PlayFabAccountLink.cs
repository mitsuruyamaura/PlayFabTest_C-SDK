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
                    Debug.Log("�L���ȃ��[���A�h���X�ƁA6�`100�����ȓ��̃p�X���[�h����͂������Ă��������B");
                    break;
                case PlayFabErrorCode.EmailAddressNotAvailable:
                    Debug.Log("���̃��[���A�h���X�͂��łɎg�p����Ă��܂��B");
                    break;
                case PlayFabErrorCode.InvalidEmailAddress:
                    Debug.Log("���̃��[���A�h���X�͎g�p�o���܂���B");
                    break;
                case PlayFabErrorCode.InvalidPassword:
                    Debug.Log("���̃p�X���[�h�͖����ł��B");
                    break;
                default:
                    Debug.Log(response.Error.GenerateErrorReport());
                    break;
            }

            return false;
        } else {

            Debug.Log("Email �ƃp�X���[�h�̓o�^����");

            return true;
        }
    }
}
