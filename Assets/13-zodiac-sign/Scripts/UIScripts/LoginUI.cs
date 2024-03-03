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
        private Image _emailInfoImage;
        private TextMeshProUGUI _emailInfoText;
    
        // Start is called before the first frame update
        private void Start()
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
                            "the email is :" + emailInputField.text + " | the password is :" + passWordInputField);
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
            if (!string.IsNullOrEmpty(UserInfoManager.Inst.UserJwtToken) && UserInfoManager.Inst.IsUserEmailChanged) // JWT 토큰이 있다 == 정상적으로 로그인 되었다
            {
                UserInfoManager.Inst.IsUserEmailChanged = false;
                Debug.Log("JWT 토큰 보임 :" + UserInfoManager.Inst.UserJwtToken);
                emailInputField.gameObject.SetActive(false);
                passWordInputField.gameObject.SetActive(false);
            
                loginButton.gameObject.SetActive(false);
                closeButton.gameObject.SetActive(true);

                _emailInfoText.text = UserInfoManager.Inst.UserEmail;
                _emailInfoImage.gameObject.SetActive(true);
                _emailInfoImage.gameObject.SetActive(true);
                
            }
            else if (string.IsNullOrEmpty(UserInfoManager.Inst.UserJwtToken)&& UserInfoManager.Inst.IsUserEmailChanged)
            {
                UserInfoManager.Inst.IsUserEmailChanged = false;
                Debug.Log("JWT 토큰 안보임 :"+UserInfoManager.Inst.UserJwtToken);
                emailInputField.gameObject.SetActive(true);
                passWordInputField.gameObject.SetActive(true);
                
                loginButton.gameObject.SetActive(true);
                closeButton.gameObject.SetActive(false);
            
                _emailInfoImage.gameObject.SetActive(false);
            }
        }
    }
}