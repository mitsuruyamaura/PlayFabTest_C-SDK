using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Cysharp.Threading.Tasks;
using PlayFab;
using PlayFab.ClientModels;

/// <summary>
/// インベントリの管理
/// </summary>
public static class InventoryManager
{
    /// <summary>
    /// インベントリで管理(所持)しているキャラ
    /// </summary>
    public static List<(string InstanceId, string CharacterId)> Characters { get; private set; }

    // TODO スタミナ回復薬、プレゼントなどの管理情報を追加


    /// <summary>
    /// PlayFab から取得したインベントリのデータをローカルにキャッシュ
    /// </summary>
    /// <param name="inventory"></param>

    public static void SyncPlayFabToClient(IEnumerable<ItemInstance> inventory) {

        Characters = inventory
            .Where(x => x.ItemClass == ItemClass.Character.ToString())
            .Select(y => (y.ItemInstanceId, y.ItemId)).ToList();

        // TODO スタミナ回復薬、プレゼントの処理を追加

    }
}
