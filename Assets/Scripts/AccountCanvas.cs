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

    [SerializeField]
    private GameObject completePopUp;

    [SerializeField]
    private Button btnClosePopUp;


    private (string email, string pasword) inputValue;


    void Start()
    {
        completePopUp.SetActive(false);

        // �{�^���̓o�^
        btnSubmit?.OnClickAsObservable()
            .ThrottleFirst(TimeSpan.FromSeconds(0.5f))
            .Subscribe(_ => OnClickSubmit());

        btnCancel?.OnClickAsObservable()
            .ThrottleFirst(TimeSpan.FromSeconds(0.5f))
            .Subscribe(_ => OnCliclCancel());

        btnClosePopUp?.OnClickAsObservable()
            .ThrottleFirst(TimeSpan.FromSeconds(0.5f))
            .Subscribe(_ => OnClickCloseCompletePopUp());

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

    
    private async void OnClickSubmit() {

        Debug.Log("OK ���F�J�n");

        bool isLink = await PlayFabAccountLink.SetEmailAndPasswordAsync(inputValue.email, inputValue.pasword);

        if (isLink) {
            Debug.Log("�A�g����");

            completePopUp.SetActive(true);
        } else {
            Debug.Log("�A�g���s");
        }
    }


    private void OnCliclCancel() {
        Debug.Log("NG");
    }


    private void OnClickCloseCompletePopUp() {

        completePopUp.SetActive(false);

        this.gameObject.SetActive(false);
    }
}
