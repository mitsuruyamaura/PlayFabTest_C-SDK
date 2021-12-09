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


    void Awake() {
        if (instance == null) {
            instance = this;
            DontDestroyOnLoad(gameObject);
        } else {
            Destroy(gameObject);
        }
    }

    /// <summary>
    /// �f�o�b�O�p�B�^�C�g���f�[�^���ɂ���e�}�X�^�[�f�[�^�̉����p
    /// </summary>
    public void SetMasterDatas() {

        // �X�L���̃}�X�^�[�f�[�^
        skillMasters = new SkillMasterData[TitleDataManager.SkillMasterData.Count];

        skillMasters = TitleDataManager.SkillMasterData.Select(x => x.Value).ToArray();

        Debug.Log(skillMasters[0].atk);

        // �L�����̃}�X�^�[�f�[�^
        charaMasters = new CharacterMasterData[TitleDataManager.CharacterMasterData.Count];

        charaMasters = TitleDataManager.CharacterMasterData.Select(x => x.Value).ToArray();

        Debug.Log(charaMasters[0].Cost);
    }

    /// <summary>
    /// �f�o�b�O�p�B�J�^���O���̃f�[�^�̉����p
    /// </summary>
    public void SetCatalogItems() {

        catalogItems = new CatalogItem[CatalogueManager.CatalogItems.Count];

        catalogItems = CatalogueManager.CatalogItems.Select(x => x.Value).ToArray();

        Debug.Log(catalogItems[0].ItemId);
        Debug.Log(catalogItems[1].DisplayName);
    }
}
