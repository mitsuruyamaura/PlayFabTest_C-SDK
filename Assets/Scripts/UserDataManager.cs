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

    // TODO Level �Ȃǂ̏�����������


    /// <summary>
    /// PlayFab �̍ŐV�f�[�^���擾���ă��[�J���ɃL���b�V��
    /// </summary>
    /// <param name="userData"></param>
    public static void SyncPlayFabToClient(Dictionary<string, UserDataRecord> userData) {

        // TODO Exp Level �Ȃǂ�ݒ�

        // ���[�U�[�̏����擾�B�擾�ł����ꍇ�ɂ͕������A�擾�ł��Ȃ��ꍇ�ɂ͐V�K���[�U�[�̍쐬
        User = userData.TryGetValue("User", out var user) 
            ? JsonConvert.DeserializeObject<User>(user.Value) : User.Create();

        Debug.Log("PlayFab �̃��[�U�[�f�[�^���擾");

        // TODO ���ɂ�����������Βǉ�


        // Debug (���\�b�h�� async ��ǉ�����)
        //(bool isSuccess, string errorMessage) updateUser = await UpdateUserDataAsync();

        //if (updateUser.isSuccess) {
        //    Debug.Log("PlayFab �̃��[�U�[�f�[�^�X�V����");
        //}
    }

    /// <summary>
    /// PlayFab �̃��[�U�[�f�[�^�̍X�V
    /// </summary>
    /// <returns></returns>
    public static async UniTask<(bool isSuccess, string errorMessage)> UpdateUserDataAsync() {

        var userJson = JsonConvert.SerializeObject(User);

        var request = new UpdateUserDataRequest {
            Data = new Dictionary<string, string> { { "User", userJson } }
        };

        var response = await PlayFabClientAPI.UpdateUserDataAsync(request);

        if (response.Error != null) {

            Debug.Log("�G���[");
        }

        return (true, string.Empty);
    }
}
