using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cysharp.Threading.Tasks;

public class MainGameManager : MonoBehaviour
{
    [SerializeField]
    private AccountCanvas accountCanvas;
    
    async UniTaskVoid Start()
    {
        //LoginManager.PrepareLoginPlayPab();

        // 初期化処理が終了するまで待機
        await UniTask.WaitUntil(() => ApplicationEntryPoint.Initialized, cancellationToken : this.GetCancellationTokenOnDestroy());


        // TODO 初期化処理が終了してから、他の処理を実行する(ログインしていないと API の呼び出しも失敗するため)

    }
}
