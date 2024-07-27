using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cysharp.Threading.Tasks;
using PlayFab;
using PlayFab.ClientModels;
using UnityEngine.Assertions;

public class RankingManager
{
    // private async UniTaskVoid Start() {
    //     await UpdatePlayerStatisticsAsync();
    // }

    /// <summary>
    /// ランキング(LeaderBoard)への情報送信(登録)
    /// </summary>
    /// <param name="level"></param>
    /// <param name="highScore"></param>
    public async UniTask UpdatePlayerStatisticsAsync(int level = 1, int highScore = 1) {
        var request = new UpdatePlayerStatisticsRequest {
            Statistics = new () {
                new () {
                    StatisticName = "Level",
                    Value = level
                },
                new () {
                    StatisticName = "HighScore",
                    Value = highScore,
                }
            }
        };

        var response = await PlayFabClientAPI.UpdatePlayerStatisticsAsync(request);
        if (response.Error != null) {
            Debug.Log(response.Error.GenerateErrorReport());
        }
        Debug.Log("ランキング情報 送信完了");
    }

    /// <summary>
    /// ランキング(LeaderBoard)から情報取得
    /// </summary>
    /// <param name="statisticName"></param>
    public async UniTask<GetLeaderboardResult> GetLeaderboardAsync(string statisticName) {

        var request = new GetLeaderboardRequest {
            StatisticName = statisticName,
            
            // Playfab の GameManager 内のクライアントプロフィールオプション設定で許可が必要のため、利用しない
            // ProfileConstraints = new() {
            //     ShowDisplayName = true,
            //     ShowStatistics = true
            // }
        };

        var response = await PlayFabClientAPI.GetLeaderboardAsync(request);

        if (response.Error != null) {
            Debug.Log(response.Error.GenerateErrorReport());
        }

        foreach (var item in response.Result.Leaderboard) {
            Debug.Log(item.Position + 1);
            Debug.Log(item.PlayFabId);
            Debug.Log(item.DisplayName);
            Debug.Log(item.StatValue);
        }
        Debug.Log($"{statisticName} : ランキング情報 取得完了");
        
        return response.Result;
    }
}