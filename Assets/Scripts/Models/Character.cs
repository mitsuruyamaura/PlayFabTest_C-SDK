/// <summary>
/// �L�����N�^�[
/// </summary>
[System.Serializable]
public class Character
{
    public string InstanceId;// { get; set; }
    public string CharacterId;// { get; set; }

    public int Level;// { get; set; }

    public int Hp;// => (int)(Master.Hp + Master.Hp * Level * 0.5f);  // ���x���𔽉f����ꍇ
    public int Atk;// { get; set; }

    // TODO ���ɂ��ǉ���������񂪂���΁A�}�X�^�[�ɍ��킹�Ēǉ�����

    /// <summary>
    /// ���̃L�����p�̃}�X�^�[�f�[�^
    /// </summary>
    public CharacterMasterData Master => TitleDataManager.CharacterMasterData[CharacterId];


    /// <summary>
    /// �L�����쐬
    /// </summary>
    /// <param name="instanceId"></param>
    /// <param name="characterId"></param>
    /// <returns></returns>
    public static Character CreateChara(string instanceId, string characterId) {

        return new Character {
            InstanceId = instanceId,
            CharacterId = characterId,
            Level = 1
        };
    }
}
