using System.Collections.Generic;
using UnityEngine;
using Newtonsoft.Json;
using System.Linq;

/// <summary>
/// �^�C�g���f�[�^�Ǘ��p
/// </summary>
public static class TitleDataManager
{
    public static Dictionary<string, SkillMasterData> SkillMasterData { get; private set; }

    public static Dictionary<string, CharacterMasterData> CharacterMasterData { get; private set; }


    // TODO�@�}�X�^�[�f�[�^�p�̕ϐ���ǉ�


    /// <summary>
    /// PlayFab �̃}�X�^�[�f�[�^(TilteData) �����[�J���ɃL���b�V��
    /// </summary>
    /// <param name="titleData"></param>
    public static void SyncPlayFabToClient(Dictionary<string, string> titleData) {

        SkillMasterData = JsonConvert.DeserializeObject<SkillMasterData[]>(titleData["SkillMasterData"]).ToDictionary(x => x.no);

        Debug.Log("TitleData SkillMasterData �L���b�V��");

        CharacterMasterData = JsonConvert.DeserializeObject<CharacterMasterData[]>(titleData["CharacterMasterData"]).ToDictionary(x => x.CharacterId);

        Debug.Log("TitleData CharacterMasterData �L���b�V��");

        // Debug �p
        GameData.instance.SetMasterDatas();

        // TODO ���̃}�X�^�[�f�[�^���ǉ�

    }
}
