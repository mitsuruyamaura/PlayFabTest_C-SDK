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
    /// �R���X�g���N�^
    /// </summary>
    static LoginManager(){
        // TitleId �ݒ�
        PlayFabSettings.staticSettings.TitleId = "DDCD3";

        Debug.Log("TitleID �ݒ�: " + PlayFabSettings.staticSettings.TitleId);
    }

    /// <summary>
    /// ���O�C���Ɠ����Ɏ擾������̐ݒ�B
    /// InfoRequestParameters �̐ݒ�l�ɂȂ�Atrue �ɂ��Ă������ƂŊe��񂪎����I�Ɏ擾�ł���悤�ɂȂ�
    /// �e�p�����[�^�̏����l�͂��ׂ� false
    /// �擾�������Ȃ�قǃ��O�C�����Ԃ�������A�������������̂ŋC��t����
    /// �擾���ʂ� InfoResultPayLoad �ɓ����Ă���Bfalse �̂��̂͂��ׂ� null �ɂȂ�
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

    /// <summary>
    /// PlayFab �ւ̃��O�C������
    /// </summary>
    public static async UniTask PrepareLoginPlayPab() {

        Debug.Log("���O�C�� ���� �J�n");

        await LoginAndUpdateLocalCacheAsync();

        // �f�o�b�O�p

        //TitleId �ݒ�
        //PlayFabSettings.staticSettings.TitleId = "DDCD3";

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
    public static async UniTask LoginAndUpdateLocalCacheAsync() {

        Debug.Log("�������J�n");

        var userId = PlayerPrefsManager.UserId;

        // ���[�U�[ID �̎擾�����݂āA�擾�ł��Ȃ��ꍇ�ɂ͓����ŐV�K�쐬����
        var loginResult = string.IsNullOrEmpty(userId)
            ? await CreateNewUserAsync() : await LoadUserAsync(userId);

        // debug
        //await CreateUserDataAsync();


        // �f�[�^�������Ŏ擾����ݒ�ɂ��Ă���̂ŁA�擾�����f�[�^�����[�J���ɃL���b�V������
        await UpdateLocalCacheAsync(loginResult);
    }

    /// <summary>
    /// �V�K���[�U�[���쐬���� UserId �� PlayerPrefs �ɕۑ�
    /// </summary>
    /// <returns></returns>
    private static async UniTask<LoginResult> CreateNewUserAsync() {

        Debug.Log("���[�U�[�f�[�^�Ȃ��B�V�K���[�U�[�쐬");

        while (true) {

            // UserId �̍̔�
            var newUserId = Guid.NewGuid().ToString().Replace("-", "").Substring(0, 20);

            // ���O�C�����N�G�X�g�̍쐬
            var request = new LoginWithCustomIDRequest {
                CustomId = newUserId,
                CreateAccount = true,
                InfoRequestParameters = CombinedInfoRequestParams
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
            PlayerPrefsManager.UserId = newUserId;

            return response.Result;
        }
    }

    /// <summary>
    /// ���O�C�����ă��[�U�[�f�[�^�����[�h
    /// </summary>
    /// <param name="userId"></param>
    /// <returns></returns>
    private static async UniTask<LoginResult> LoadUserAsync(string userId) {

        Debug.Log("���[�U�[�f�[�^����B���O�C���J�n");

        // ���O�C�����N�G�X�g�̍쐬
        var request = new LoginWithCustomIDRequest {
            CustomId = userId,
            CreateAccount = false,
            InfoRequestParameters = CombinedInfoRequestParams
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

    /// <summary>
    /// Email �ƃp�X���[�h�Ń��O�C��(�A�J�E���g�񕜗p)
    /// </summary>
    /// <param name="email"></param>
    /// <param name="password"></param>
    /// <returns></returns>
    public static async UniTask<(bool, string)> LoginEmailAndPasswordAsync(string email, string password) {

        // Email �ɂ�郍�O�C�����N�G�X�g�̍쐬
        var request = new LoginWithEmailAddressRequest {
            Email = email,
            Password = password,
            InfoRequestParameters = CombinedInfoRequestParams
        };

        // PlayFab �Ƀ��O�C��
        var response = await PlayFabClientAPI.LoginWithEmailAddressAsync(request);

        // �G���[�n���h�����O
        if (response.Error != null) {
            switch (response.Error.Error) {
                case PlayFabErrorCode.InvalidParams:
                case PlayFabErrorCode.InvalidEmailOrPassword:
                case PlayFabErrorCode.AccountNotFound:
                    Debug.Log("���[���A�h���X���p�X���[�h������������܂���");
                    break;
                default:
                    Debug.Log(response.Error.GenerateErrorReport());
                    break;
            }

            return (false, "���[���A�h���X���p�X���[�h������������܂���");
        }

        // PlayerPrefas �����������āA���O�C�����ʂ� UserId ��o�^������
        PlayerPrefs.DeleteAll();

        // �V���� PlayFab ���� UserId ���擾
        // InfoResultPayload �̓N���C�A���g�v���t�B�[���I�v�V����(InfoRequestParameters)�ŋ�����ĂȂ��� null �ɂȂ�
        PlayerPrefsManager.UserId = response.Result.InfoResultPayload.AccountInfo.CustomIdInfo.CustomId;

        // Email �Ń��O�C���������Ƃ��L�^����
        PlayerPrefsManager.IsLoginEmailAdress = true;

        return (true, "Email �ɂ�郍�O�C�����������܂����B");
    }

    /// <summary>
    /// PlayFab ����擾�����f�[�^�Q�����[�J��(�[��)�ɃL���b�V��
    /// </summary>
    /// <param name="loginResult"></param>
    /// <returns></returns>

    public static async UniTask UpdateLocalCacheAsync(LoginResult loginResult) {

        // �J�^���O�ނ̏������B���̃C���X�^���X�̏������ɂ��K�v�Ȃ̂ōŏ��ɍs��
        await UniTask.WhenAll(

            CatalogueManager.SyncPlayFabToClient()

        );

        // �^�C�g���f�[�^�̃L���b�V��
        TitleDataManager.SyncPlayFabToClient(loginResult.InfoResultPayload.TitleData);

        // ���[�U�[���Ȃǂ̃L���b�V��
        PlayerPlofileManager.SyncPlayFabToClient(loginResult.InfoResultPayload.PlayerProfile, loginResult.InfoResultPayload.PlayerStatistics);


        // �C���x���g���̃L���b�V��
        InventoryManager.SyncPlayFabToClient(loginResult.InfoResultPayload.UserInventory);


        // ���[�U�[�f�[�^�̃L���b�V��
        UserDataManager.SyncPlayFabToClient(loginResult.InfoResultPayload.UserData);

        
        // TODO ������������ǉ�


        Debug.Log("�e��f�[�^�̃L���b�V������");
    }

    /// <summary>
    /// �f�o�b�O�p
    /// </summary>
    private static async UniTask CreateUserDataAsync() {

        //var createData = new Dictionary<string, string> {
        //    { "Level", "0"}
        //};



        //await UserDataManager.UpdatePlayerDataAsync(createData);

        UserDataManager.User = User.Create();
        string key = "User";

        await UserDataManager.UpdateUserDataByJsonAsync(key);

        Debug.Log("���[�U�[�f�[�^ �o�^����");
    } 
}