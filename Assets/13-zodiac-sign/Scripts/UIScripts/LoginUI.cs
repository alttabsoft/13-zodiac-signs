using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace _13_zodiac_sign.Scripts.UIScripts
{
    public class LoginUIScript : MonoBehaviour
    {
        [SerializeField] private TMP_InputField emailInputField;
        [SerializeField] private TMP_InputField passWordInputField;
        [SerializeField] private Button loginButton;
        [SerializeField] private Button closeButton;
        [SerializeField] private Button logoutButton;
        [SerializeField] private Button registerUIButton;
        private Image _emailInfoImage;
        private TextMeshProUGUI _emailInfoText;
        private MainMenuUI _mainMenuUI;
    
        // Start is called before the first frame update
        private void Start()
        {
            _mainMenuUI = GetComponentInParent<MainMenuUI>();
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
                        // Debug.Log("the email is :" + emailInputField.text + " | the password is :" + passWordInputField);
                        var emailAndPassword = emailInputField.text + ":" + passWordInputField.text;
                        StartCoroutine(InteractToServerService.Login(emailInputField.text, passWordInputField.text));
                    });
                }
                if (buttonName == "CloseButton")
                {
                    closeButton = button;
                    button.onClick.AddListener(() =>
                    {
                        gameObject.SetActive(false); // LoginUI 창 닫기
                    });
                }
                if (buttonName == "LogoutButton")
                {
                    logoutButton = button;
                    button.onClick.AddListener(() =>
                    {
                        StartCoroutine(InteractToServerService.Logout());
                    });
                }
                
                if (buttonName == "RegisterUIButton")
                {
                    registerUIButton = button;
                    var registerUI = _mainMenuUI.RegisterUI;

                    button.onClick.AddListener(() =>
                    {
                        if (registerUI.activeSelf)
                        {
                            gameObject.SetActive(true);
                            registerUI.SetActive(false);
                        }
                        else
                        {
                            gameObject.SetActive(false);
                            registerUI.SetActive(true);
                        }
                    });
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
            // 로그인 정보 있을 때
            // JWT 토큰이 있다 == 정상적으로 로그인 되었다
            if (!string.IsNullOrEmpty(UserInfoManager.Inst.UserJwtToken) && UserInfoManager.Inst.IsUserEmailChanged) 
            {
                UserInfoManager.Inst.IsUserEmailChanged = false;
                // Debug.Log("JWT 토큰 보임 :" + UserInfoManager.Inst.UserJwtToken);
                emailInputField.gameObject.SetActive(false);
                passWordInputField.gameObject.SetActive(false);
            
                loginButton.gameObject.SetActive(false);
                logoutButton.gameObject.SetActive(true);

                _emailInfoText.text = UserInfoManager.Inst.UserEmail;
                _emailInfoImage.gameObject.SetActive(true);
                _emailInfoImage.gameObject.SetActive(true);
                
            }
            
            // 로그인 정보 없을 때
            // JWT 토큰이 없다
            else if (string.IsNullOrEmpty(UserInfoManager.Inst.UserJwtToken)&& UserInfoManager.Inst.IsUserEmailChanged)
            {
                UserInfoManager.Inst.IsUserEmailChanged = false;
                // Debug.Log("JWT 토큰 안보임 :"+UserInfoManager.Inst.UserJwtToken);
                emailInputField.gameObject.SetActive(true);
                passWordInputField.gameObject.SetActive(true);
                
                logoutButton.gameObject.SetActive(false);
                loginButton.gameObject.SetActive(true);
            
                _emailInfoImage.gameObject.SetActive(false);
            }
        }
    }
}