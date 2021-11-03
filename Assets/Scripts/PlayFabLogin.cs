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

        // ���O�C���̏��(���N�G�X�g)���쐬���Đݒ�
        var request = new LoginWithCustomIDRequest {
            CustomId = "GettingStartedGuide",
            CreateAccount = true
        };

        // PlayFab �փ��O�C���B��񂪊m�F�ł���܂őҋ@
        var result = await PlayFabClientAPI.LoginWithCustomIDAsync(request);

        // �G���[�̓��e�����ăn���h�����O���s���A���O�C���ɐ������Ă��邩�𔻒�
        var message = result.Error is null ? $"Login success! My PlayFabID is {result.Result.PlayFabId}" : result.Error.GenerateErrorReport();

        Debug.Log(message);
    }
}
