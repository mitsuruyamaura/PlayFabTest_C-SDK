using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Cysharp.Threading.Tasks;
using PlayFab;
using PlayFab.ClientModels;

/// <summary>
/// �C���x���g���̊Ǘ�
/// </summary>
public static class InventoryManager
{
    /// <summary>
    /// �C���x���g���ŊǗ�(����)���Ă���L����
    /// </summary>
    public static List<(string InstanceId, string CharacterId)> Characters { get; private set; }

    // TODO �X�^�~�i�񕜖�A�v���[���g�Ȃǂ̊Ǘ�����ǉ�


    /// <summary>
    /// PlayFab ����擾�����C���x���g���̃f�[�^�����[�J���ɃL���b�V��
    /// </summary>
    /// <param name="inventory"></param>

    public static void SyncPlayFabToClient(IEnumerable<ItemInstance> inventory) {

        Characters = inventory
            .Where(x => x.ItemClass == ItemClass.Character.ToString())
            .Select(y => (y.ItemInstanceId, y.ItemId)).ToList();

        // TODO �X�^�~�i�񕜖�A�v���[���g�̏�����ǉ�

    }
}
