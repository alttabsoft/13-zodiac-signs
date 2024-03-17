using System;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

namespace _13_zodiac_sign.Scripts.UIScripts
{
    public class CubeSceneUI : MonoBehaviour
    {
        private GameObject _loginUI;
        private GameObject _registerUI;

        public GameObject RegisterUI
        {
            get => _registerUI;
        }
        
        private void Start()
        {
            var buttons = GetComponentsInChildren<Button>(includeInactive: true); // 버튼별 역할 할당,
            foreach (var button in buttons)
            {
                var buttonName = button.transform.name;
                if (buttonName == "BattleSceneButton")
                {
                    button.onClick.AddListener(() =>
                    {
                        SceneManager.LoadScene("BattleScene");
                    });
                }
                if (buttonName == "CubeSceneButton")
                {
                    button.onClick.AddListener(() =>
                    {
                        SceneManager.LoadScene("CubeScene");
                    });
                }
                
                if (buttonName == "MainMenuSceneButton")
                {
                    button.onClick.AddListener(() =>
                    {
                        SceneManager.LoadScene("MainMenuScene");
                    });
                }

                if (buttonName == "OpenLoginUIButton")
                {
                    button.onClick.AddListener(() =>
                    {
                        // LoginUI On/Off 기능
                        if (_loginUI.activeSelf)
                        {_loginUI.SetActive(false);}
                        else
                        {_loginUI.SetActive(true);}
                    });
                }
            }
        }
    }
}