using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Cysharp.Threading.Tasks;
using PlayFab;
using PlayFab.ClientModels;

public static class CatalogueManager
{ 
    /// <summary>
    /// カタログの全データ
    /// </summary>
    public static Dictionary<string, CatalogItem> CatalogItems { get; private set; }


    public static async UniTask SyncPlayFabToClient() {

        var response = await PlayFabClientAPI.GetCatalogItemsAsync(new GetCatalogItemsRequest());

        if (response.Error != null) {

            // エラーのため、例外処理を行う

        } else {

            CatalogItems = response.Result.Catalog.ToDictionary(x => x.ItemId);

            // Debug
            GameData.instance.SetCatalogItems();
        }
    }
}
