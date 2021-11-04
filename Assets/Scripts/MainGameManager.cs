using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cysharp.Threading.Tasks;

public class MainGameManager : MonoBehaviour
{
    [SerializeField]
    private AccountCanvas accountCanvas;
    
    void Start()
    {
        LoginManager.PrepareLoginPlayPab();
    }
}
