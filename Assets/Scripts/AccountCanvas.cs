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

    [SerializeField]
    private Button btnEmainLogin;


    private (string email, string pasword) inputValue;�@�@// Email �ƃp�X���[�h�o�^�p


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

        btnEmainLogin?.OnClickAsObservable()
            .ThrottleFirst(TimeSpan.FromSeconds(0.5f))
            .Subscribe(_ => OnClickEmailLogin());

        // InputField
        emailInput?.OnEndEditAsObservable()
            .Subscribe(x => UpdateDispayEmail(x));

        passwordInput?.OnEndEditAsObservable()
            .Subscribe(x => UpdateDisplayPassword(x));
    }

    /// <summary>
    /// Email �̒l�ƕ\���̍X�V
    /// </summary>
    /// <param name="newEmail"></param>
    private void UpdateDispayEmail(string newEmail) {
        txtEmail.text = newEmail;
        inputValue.email = newEmail;
        Debug.Log(inputValue);
    }

    /// <summary>
    /// �p�X���[�h�̒l�ƕ\���̍X�V
    /// </summary>
    /// <param name="newPassword"></param>
    private void UpdateDisplayPassword(string newPassword) {
        txtPassword.text = newPassword;
        inputValue.pasword = newPassword;
        Debug.Log(inputValue);
    }

    /// <summary>
    /// OK �{�^�������������ۂ̏���
    /// </summary>
    private async void OnClickSubmit() {

        Debug.Log("OK �A�J�E���g�A�g�̏��F�J�n");

        // Email �ƃp�X���[�h�𗘗p���āA���[�U�[�A�J�E���g�̘A�g�����݂�
        bool isLink = await PlayFabAccountLink.SetEmailAndPasswordAsync(inputValue.email, inputValue.pasword);

        if (isLink) {
            Debug.Log("�A�g����");

            completePopUp.SetActive(true);
        } else {
            Debug.Log("�A�g���s");
        }
    }

    /// <summary>
    /// NG �{�^�������������ۂ̏���
    /// </summary>
    private void OnCliclCancel() {
        this.gameObject.SetActive(false);

        Debug.Log("NG");
    }

    /// <summary>
    /// CompletePopUp ���^�b�v�����ۂ̏���
    /// </summary>
    private void OnClickCloseCompletePopUp() {

        completePopUp.SetActive(false);

        this.gameObject.SetActive(false);
    }

    /// <summary>
    /// Email �Ń��O�C���{�^�������������ۂ̏���
    /// </summary>
    private async void OnClickEmailLogin() {

        // Email �Ń��O�C�������݂�
        bool isLogin =  await LoginManager.LoginEmailAndPasswordAsync(inputValue.email, inputValue.pasword);

        if (isLogin) {
            Debug.Log("Email �ɂ�郍�O�C������");

            completePopUp.SetActive(true);
        } else {
            Debug.Log("���O�C�����s");
        }
    }
}
