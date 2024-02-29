using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using _13_zodiac_sign.Scripts.UIScripts;
using TMPro;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.UI;

public class LoginUIScript : MonoBehaviour
{
    [SerializeField] private TMP_InputField emailInputField;
    [SerializeField] private TMP_InputField passWordInputField;
    [SerializeField] private Button loginButton;
    [SerializeField] private Button closeButton;
    private Image _emailInfoImage;
    private TextMeshProUGUI _emailInfoText;
    
    // Start is called before the first frame update
    void Start()
    {
        var buttons = GetComponentsInChildren<Button>(includeInactive: true);
        var tmpInputFields = GetComponentsInChildren<TMP_InputField>(includeInactive: true);
        var images = GetComponentsInChildren<Image>(includeInactive: true);
        
        #region Initialize Button, Text, InputField
        foreach (TMP_InputField tmpInputField in tmpInputFields)
        {
            var inputFieldName = tmpInputField.transform.name;
            if (inputFieldName == "EmailInputField")
            {
                emailInputField = tmpInputField;
            }
            if (inputFieldName == "PassWordInputField")
            {
                passWordInputField = tmpInputField;
                passWordInputField.contentType = TMP_InputField.ContentType.Password;
            }

        }
        foreach (var button in buttons)
        {
            var buttonName = button.transform.name;
            if (buttonName == "LoginButton")
            {
                loginButton = button;
                button.onClick.AddListener(() =>
                {
                    Debug.Log(
                        "the email is :" + emailInputField.text + "| the password is :" + passWordInputField);
                    var emailAndPassword = emailInputField.text + ":" + passWordInputField.text;
                    StartCoroutine(InteractToServerService.Login(emailInputField.text, passWordInputField.text));
                });
            }
            if (buttonName == "CloseButton")
            {
                closeButton = button;
            }
        }

        foreach (var image in images)
        {
            var imageName = image.transform.name;
            if (imageName == "EmailInfoImage")
            {
                _emailInfoImage = image;
                _emailInfoText = image.gameObject.GetComponentInChildren<TextMeshProUGUI>(includeInactive: true);
            }
        }
        #endregion
    }
    
    private void Update()
    {
        if (!string.IsNullOrEmpty(UserInfoManager.Inst.UserJwtToken)) // JWT 토큰이 있다 == 정상적으로 로그인 되었다
        {
            // Debug.Log("JWT 토큰 보임 :" + UserInfoManager.Inst.UserJwtToken);
            emailInputField.gameObject.SetActive(false);
            passWordInputField.gameObject.SetActive(false);
            
            loginButton.gameObject.SetActive(false);
            closeButton.gameObject.SetActive(true);
            
            _emailInfoImage.gameObject.SetActive(true);
        }
        else if (string.IsNullOrEmpty(UserInfoManager.Inst.UserJwtToken))
        {
            // Debug.Log("JWT 토큰 안보임 :"+UserInfoManager.Inst.UserJwtToken);
            emailInputField.gameObject.SetActive(true);
            passWordInputField.gameObject.SetActive(true);
                
            loginButton.gameObject.SetActive(true);
            closeButton.gameObject.SetActive(false);
            
            _emailInfoImage.gameObject.SetActive(false);
        }
    }
}