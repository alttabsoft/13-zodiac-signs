using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace _13_zodiac_sign.Scripts.UIScripts
{
    public class RegisterUIScript : MonoBehaviour
    {
        [SerializeField] private TMP_InputField emailInputField;
        [SerializeField] private TMP_InputField passWordInputField;
        [SerializeField] private Button registerButton;
        [SerializeField] private Button closeButton;
    
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
                if (buttonName == "CloseButton")
                {
                    closeButton = button;
                    button.onClick.AddListener(() =>
                    {
                        gameObject.SetActive(false); // RegisterUI 창 닫기
                    });
                }

                if (buttonName == "RegisterButton")
                {
                    registerButton = button;
                    button.onClick.AddListener(() =>
                    {
                        StartCoroutine(InteractToServerService.Register(emailInputField.text, passWordInputField.text));
                    });
                }
            }
            #endregion
        }
    
        private void Update()
        {
            if (!string.IsNullOrEmpty(UserInfoManager.Inst.UserJwtToken) && UserInfoManager.Inst.IsUserEmailChanged) 
            {
                
            }
            
            else if (string.IsNullOrEmpty(UserInfoManager.Inst.UserJwtToken)&& UserInfoManager.Inst.IsUserEmailChanged)
            {

            }
        }
    }
}