/// <summary>
/// キャラクター
/// </summary>
[System.Serializable]
public class Character
{
    public string InstanceId;// { get; set; }
    public string CharacterId;// { get; set; }

    public int Level;// { get; set; }

    public int Hp;// => (int)(Master.Hp + Master.Hp * Level * 0.5f);  // レベルを反映する場合
    public int Atk;// { get; set; }

    // TODO 他にも追加したい情報があれば、マスターに合わせて追加する

    /// <summary>
    /// このキャラ用のマスターデータ
    /// </summary>
    public CharacterMasterData Master => TitleDataManager.CharacterMasterData[CharacterId];


    /// <summary>
    /// キャラ作成
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
