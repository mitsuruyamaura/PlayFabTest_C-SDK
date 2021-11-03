using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UniRx;
using System;

public class AccountCanvas : MonoBehaviour
{
    [SerializeField]
    private Button btnSubmit;

    [SerializeField]
    private Button btnCancel;

    [SerializeField]
    private InputField emailInput;

    [SerializeField]
    private InputField passwordInput;

    [SerializeField]
    private Text txtEmail;

    [SerializeField]
    private Text txtPassword;

    private (string email, string pasword) inputValue;


    void Start()
    {
        // ƒ{ƒ^ƒ“‚Ì“o˜^
        btnSubmit?.OnClickAsObservable()
            .ThrottleFirst(TimeSpan.FromSeconds(0.5f))
            .Subscribe(_ => OnClickSubmit());

        btnCancel?.OnClickAsObservable()
            .ThrottleFirst(TimeSpan.FromSeconds(0.5f))
            .Subscribe(_ => OnCliclCancel());

        // InputField
        emailInput?.OnEndEditAsObservable()
            .Subscribe(x => UpdateDispayEmail(x));

        passwordInput?.OnEndEditAsObservable()
            .Subscribe(x => UpdateDisplayPassword(x));

    }


    private void UpdateDispayEmail(string newEmail) {
        txtEmail.text = newEmail;
        inputValue.email = newEmail;
        Debug.Log(inputValue);
    }


    private void UpdateDisplayPassword(string newPassword) {
        txtPassword.text = newPassword;
        inputValue.pasword = newPassword;
        Debug.Log(inputValue);
    }

    
    private void OnClickSubmit() {


        Debug.Log("OK");
    }


    private void OnCliclCancel() {
        Debug.Log("NG");
    }
}
