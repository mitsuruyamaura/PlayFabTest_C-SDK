using System;
using UniRx;
using UnityEngine;
using UnityEngine.UI;

public class RankingEntryCanvas : MonoBehaviour
{
    [SerializeField]
    private Button btnSubmit;

    [SerializeField]
    private Button btnCancel;

    [SerializeField]
    private InputField displayNameInput;

    [SerializeField]
    private GameObject rankingPopUp;

    [SerializeField]
    private Button btnClosePopUp;

    private string displayName;  // ユーザー名登録用
    private Canvas rankingEntryCanvas;

    [SerializeField] private Leaderboard leaderboardPrefab;
    [SerializeField] private Transform LeaderboardTran;
    

    void Start() {
        if (!TryGetComponent(out rankingEntryCanvas)) {
            return;
        }

        rankingEntryCanvas.enabled = false;
        rankingPopUp.SetActive(false);

        // ボタンの登録
        btnSubmit?.OnClickAsObservable()
            .ThrottleFirst(TimeSpan.FromSeconds(0.5f))
            .Subscribe(_ => OnClickSubmit());

        btnCancel?.OnClickAsObservable()
            .ThrottleFirst(TimeSpan.FromSeconds(0.5f))
            .Subscribe(_ => OnCliclCancel());

        btnClosePopUp?.OnClickAsObservable()
            .ThrottleFirst(TimeSpan.FromSeconds(0.5f))
            .Subscribe(_ => CloseRankingEntryCanvas());

        // InputField(文字入力を監視し、画面の表示更新も行う)
        displayNameInput?.OnEndEditAsObservable()
            .Subscribe(x => UpdateDispayName(x));
    }

    /// <summary>
    /// ユーザー名の値と表示の更新
    /// </summary>
    /// <param name="newName"></param>
    private void UpdateDispayName(string newName) {
        displayName = newName;
        Debug.Log(displayName);
    }

    /// <summary>
    /// 登録ボタンを押下した際の処理
    /// </summary>
    private async void OnClickSubmit() {

        Debug.Log("OK アカウント連携の承認開始");

        // Email とパスワードを利用して、ユーザーアカウントの連携を試みる
        (bool isSuccess, string message) response = await PlayerPlofileManager.UpdateUserDisplayNameAsync(displayName);

        // Debug用
        if (response.isSuccess) {
            Debug.Log("ユーザー名 更新成功");
        } else {
            Debug.Log("ユーザー名 更新失敗");
        }
        
        // ランキング情報を制御するクラスを生成
        RankingManager rankingManager = new();
        
        // 送る情報を引数にセットする
        await rankingManager.UpdatePlayerStatisticsAsync();

        // 受信
        var level = await rankingManager.GetLeaderboardAsync("Level");
        var highScore = await rankingManager.GetLeaderboardAsync("HighScore");

        // LeaderBoard 生成と設定
        for (int i = 0; i < level.Leaderboard.Count; i++) {
            Leaderboard leaderboard = Instantiate(leaderboardPrefab, LeaderboardTran, false);
            leaderboard.ShowLeaderBoardInfo(displayName, level.Leaderboard[i], highScore.Leaderboard[i]);
        }
        
        // 画面表示
        rankingPopUp.SetActive(true);
    }

    /// <summary>
    /// キャンセルボタンを押下した際の処理
    /// </summary>
    private void OnCliclCancel() {
        rankingEntryCanvas.enabled = false;
        
        //gameObject.SetActive(false);

        Debug.Log("キャンセル");
    }

    /// <summary>
    /// RankingEntryCanvas 非表示
    /// </summary>
    private void CloseRankingEntryCanvas() {

        rankingPopUp.SetActive(false);

        rankingEntryCanvas.enabled = false;
    }

    /// <summary>
    /// RankingEntryCanvas 表示
    /// </summary>
    public void ShowRankingEntryCanvas() {
        rankingEntryCanvas.enabled = true;
    }
}