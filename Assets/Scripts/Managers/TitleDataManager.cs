using System.Collections.Generic;
using UnityEngine;
using Newtonsoft.Json;
using System.Linq;

/// <summary>
/// タイトルデータ管理用
/// </summary>
public static class TitleDataManager
{
    public static Dictionary<string, SkillMasterData> SkillMasterData { get; private set; }

    public static Dictionary<string, CharacterMasterData> CharacterMasterData { get; private set; }


    // TODO　マスターデータ用の変数を追加


    /// <summary>
    /// PlayFab のマスターデータ(TilteData) をローカルにキャッシュ
    /// </summary>
    /// <param name="titleData"></param>
    public static void SyncPlayFabToClient(Dictionary<string, string> titleData) {

        SkillMasterData = JsonConvert.DeserializeObject<SkillMasterData[]>(titleData["SkillMasterData"]).ToDictionary(x => x.no);

        Debug.Log("TitleData SkillMasterData キャッシュ");

        CharacterMasterData = JsonConvert.DeserializeObject<CharacterMasterData[]>(titleData["CharacterMasterData"]).ToDictionary(x => x.CharacterId);

        Debug.Log("TitleData CharacterMasterData キャッシュ");

        // Debug 用
        GameData.instance.SetMasterDatas();

        // TODO 他のマスターデータも追加

    }
}
