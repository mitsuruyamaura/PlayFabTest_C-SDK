using PlayFab.ClientModels;
using UnityEngine;
using UnityEngine.UI;

public class Leaderboard : MonoBehaviour
{
    [SerializeField] private Text[] txtLeaderboards;  // ランキング用。ユーザー名、Level、HighScore の順に利用する

    /// <summary>
    /// ランキング情報を設定
    /// </summary>
    /// <param name="displayName"></param>
    /// <param name="LeaderboardLevel"></param>
    /// <param name="LeaderboardHighScore"></param>
    public void ShowLeaderBoardInfo(string displayName, PlayerLeaderboardEntry LeaderboardLevel, PlayerLeaderboardEntry LeaderboardHighScore) {
        txtLeaderboards[0].text = displayName;
        txtLeaderboards[1].text = LeaderboardLevel.StatValue.ToString();
        txtLeaderboards[2].text = LeaderboardHighScore.StatValue.ToString();
        
        Debug.Log($"UserName : { LeaderboardLevel.DisplayName }");
        Debug.Log($"Level : { LeaderboardLevel.StatValue }");
        Debug.Log($"HighScore : { LeaderboardHighScore.StatValue }");
    }
}