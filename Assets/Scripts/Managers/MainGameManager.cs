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

        // �������������I������܂őҋ@
        await UniTask.WaitUntil(() => ApplicationEntryPoint.Initialized, cancellationToken : this.GetCancellationTokenOnDestroy());


        // TODO �������������I�����Ă���A���̏��������s����(���O�C�����Ă��Ȃ��� API �̌Ăяo�������s���邽��)

    }
}
