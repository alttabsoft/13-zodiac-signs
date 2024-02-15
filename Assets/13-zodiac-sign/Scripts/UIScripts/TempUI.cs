using System;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

namespace _13_zodiac_sign.Scripts.UIScripts
{
    public class TempUI : MonoBehaviour
    {
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
            }
        }
    }
}