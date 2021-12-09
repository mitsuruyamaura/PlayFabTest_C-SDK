using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Cysharp.Threading.Tasks;
using PlayFab;
using PlayFab.ClientModels;

public static class CatalogueManager
{ 
    /// <summary>
    /// �J�^���O�̑S�f�[�^
    /// </summary>
    public static Dictionary<string, CatalogItem> CatalogItems { get; private set; }


    public static async UniTask SyncPlayFabToClient() {

        var response = await PlayFabClientAPI.GetCatalogItemsAsync(new GetCatalogItemsRequest());

        if (response.Error != null) {

            // �G���[�̂��߁A��O�������s��

        } else {

            CatalogItems = response.Result.Catalog.ToDictionary(x => x.ItemId);

            // Debug
            GameData.instance.SetCatalogItems();
        }
    }
}
