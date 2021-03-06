using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using PlayFab.ClientModels;

public class GameData : MonoBehaviour
{
    public static GameData instance;

    public bool isDebugOn;

    public SceneNameType debugSceneName;

    public SkillMasterData[] skillMasters;
    public CatalogItem[] catalogItems;
    public CharacterMasterData[] charaMasters;
    public List<Character> inventroyCharacters;


    void Awake() {
        if (instance == null) {
            instance = this;
            DontDestroyOnLoad(gameObject);
        } else {
            Destroy(gameObject);
        }
    }

    /// <summary>
    /// デバッグ用。タイトルデータ内にある各マスターデータの可視化用
    /// </summary>
    public void SetMasterDatas() {

        // スキルのマスターデータ
        skillMasters = new SkillMasterData[TitleDataManager.SkillMasterData.Count];

        skillMasters = TitleDataManager.SkillMasterData.Select(x => x.Value).ToArray();

        Debug.Log(skillMasters[0].atk);

        // キャラのマスターデータ
        charaMasters = new CharacterMasterData[TitleDataManager.CharacterMasterData.Count];

        charaMasters = TitleDataManager.CharacterMasterData.Select(x => x.Value).ToArray();

        Debug.Log(charaMasters[0].Cost);
    }

    /// <summary>
    /// デバッグ用。カタログ内のデータの可視化用
    /// </summary>
    public void SetCatalogItems() {

        catalogItems = new CatalogItem[CatalogueManager.CatalogItems.Count];

        catalogItems = CatalogueManager.CatalogItems.Select(x => x.Value).ToArray();

        Debug.Log(catalogItems[0].ItemId);
        Debug.Log(catalogItems[1].DisplayName);
    }

    /// <summary>
    /// デバッグ用。インベントリ内のデータの可視化用
    /// </summary>
    public void SetInventoryDatas() {

        inventroyCharacters = UserDataManager.User.Characters.Select(x => x.Value).ToList();
    }
}
