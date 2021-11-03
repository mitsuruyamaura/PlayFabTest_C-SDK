using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using PlayFab;
using PlayFab.ClientModels;
using System.Threading.Tasks;
using System;

public static class LoginManager {

    public static async void PrepareLoginPlayPab() {

        Debug.Log("�J�n");

        PlayFabSettings.staticSettings.TitleId = "DDCD3";

        await LoginAndUpdateLocalCacheAsync();

        // �f�o�b�O�p
        //// ���O�C���̏��(���N�G�X�g)���쐬���Đݒ�
        //var request = new LoginWithCustomIDRequest {
        //    CustomId = "GettingStartedGuide",
        //    CreateAccount = true
        //};

        //// PlayFab �փ��O�C���B��񂪊m�F�ł���܂őҋ@
        //var result = await PlayFabClientAPI.LoginWithCustomIDAsync(request);

        //// �G���[�̓��e�����ăn���h�����O���s���A���O�C���ɐ������Ă��邩�𔻒�
        //var message = result.Error is null ? $"Login success! My PlayFabID is {result.Result.PlayFabId}" : result.Error.GenerateErrorReport();

        //Debug.Log(message);
    }


    /// <summary>
    /// ���[�U�[�f�[�^�ƃ^�C�g���f�[�^��������
    /// </summary>
    /// <returns></returns>
    public static async Task LoginAndUpdateLocalCacheAsync() {

        Debug.Log("�������J�n");

        var userId = PlayerPrefsManager.UserID;

        var loginResult = string.IsNullOrEmpty(userId) ? await CreateNewUserAsync() : await LoadUserAsync(userId);

        // TODO �擾�����f�[�^���L���b�V������
        //await UpdateLocalCacheAsync(loginResult);
    }

    /// <summary>
    /// �V�K���[�U�[���쐬���� UserId �� PlayerPrefs �ɕۑ�
    /// </summary>
    /// <returns></returns>
    private static async Task<LoginResult> CreateNewUserAsync() {

        Debug.Log("���[�U�[�f�[�^�Ȃ��B�V�K���[�U�[�쐬");

        while (true) {

            // UserId �̍̔�
            var newUserId = Guid.NewGuid().ToString().Replace("-", "").Substring(0, 20);

            // ���O�C�����N�G�X�g�̍쐬
            var request = new LoginWithCustomIDRequest {
                CustomId = newUserId,
                CreateAccount = true,
            };

            // PlayFab �Ƀ��O�C��
            var response = await PlayFabClientAPI.LoginWithCustomIDAsync(request);

            // �G���[�n���h�����O
            if (response.Error != null) {
                Debug.Log("Error");
            }

            // ������ LastLoginTime �ɒl�������Ă���ꍇ�ɂ́A�̔Ԃ��� ID ���������[�U�[�Əd�����Ă���̂Ń��g���C����
            if (response.Result.LastLoginTime.HasValue) {
                continue;
            }

            // PlayerPrefs �� UserId ���L�^����
            PlayerPrefsManager.UserID = newUserId;

            return response.Result;
        }
    }

    /// <summary>
    /// ���O�C�����ă��[�U�[�f�[�^�����[�h
    /// </summary>
    /// <param name="userId"></param>
    /// <returns></returns>
    private static async Task<LoginResult> LoadUserAsync(string userId) {

        Debug.Log("���[�U�[�f�[�^����B���O�C���J�n");

        // ���O�C�����N�G�X�g�̍쐬
        var request = new LoginWithCustomIDRequest {
            CustomId = userId,
            CreateAccount = false,

            // TODO InfoRequestParameters �̐ݒ� 

        };

        // PlayFab �Ƀ��O�C��
        var response = await PlayFabClientAPI.LoginWithCustomIDAsync(request);

        // �G���[�n���h�����O
        //if (response.Error != null) {
        //    Debug.Log("Error");
        //}

        // �G���[�̓��e�����ăn���h�����O���s���A���O�C���ɐ������Ă��邩�𔻒�
        var message = response.Error is null ? $"Login success! My PlayFabID is {response.Result.PlayFabId}" : response.Error.GenerateErrorReport();

        Debug.Log(message);

        return response.Result;
    }
}
