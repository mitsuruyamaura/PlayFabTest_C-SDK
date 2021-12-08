using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Newtonsoft.Json;
using System;
using System.Linq;
using System.Text;

public static class TitleDataManager
{
    public static Dictionary<string, SkillMasterData> SkillMasterData { get; private set; }

    // TODO�@�}�X�^�[�f�[�^�p�̕ϐ���ǉ�


    /// <summary>
    /// PlayFab �̃}�X�^�[�f�[�^(TilteData) �����[�J���ɃL���b�V��
    /// </summary>
    /// <param name="titleData"></param>
    public static void SyncPlayFabToClient(Dictionary<string, string> titleData) {

        SkillMasterData = JsonConvert.DeserializeObject<SkillMasterData[]>(titleData["SkillMasterData"]).ToDictionary(x => x.no);

        Debug.Log("TitleData SkillMasterData �L���b�V��");

        GameData.instance.SetSkillMaster();

        // TODO ���̃}�X�^�[�f�[�^���ǉ�

    }

}
