using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainGameManager : MonoBehaviour
{
    [SerializeField]
    private AccountCanvas accountCanvas;
    
    void Start()
    {
        LoginManager.PrepareLoginPlayPab();
    }


}
