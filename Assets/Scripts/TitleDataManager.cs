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

    // TODO　マスターデータ用の変数を追加


    /// <summary>
    /// PlayFab のマスターデータ(TilteData) をローカルにキャッシュ
    /// </summary>
    /// <param name="titleData"></param>
    public static void SyncPlayFabToClient(Dictionary<string, string> titleData) {

        SkillMasterData = JsonConvert.DeserializeObject<SkillMasterData[]>(titleData["SkillMasterData"]).ToDictionary(x => x.no);

        Debug.Log("TitleData SkillMasterData キャッシュ");

        GameData.instance.SetSkillMaster();

        // TODO 他のマスターデータも追加

    }

}
