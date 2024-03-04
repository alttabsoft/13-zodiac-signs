using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UIElements;

public class MenuController : MonoBehaviour
{
    private UIDocument _doc;
    private Button _playButton;
    private Label _displayTxt;
    private int count = 0;
    private Button _saveButton;
    private Button _loadButton;
    private Button _loginButton;

    private void Awake()
    {
        Application.targetFrameRate = 60;
        
        _doc = GetComponent<UIDocument>();
        _playButton = _doc.rootVisualElement.Q<Button>("play-button");
        _displayTxt = _doc.rootVisualElement.Q<Label>("display-txt");
        _saveButton = _doc.rootVisualElement.Q<Button>("save-button");
        _loadButton = _doc.rootVisualElement.Q<Button>("load-button");
        _loginButton = _doc.rootVisualElement.Q<Button>("login-button");
        
        _playButton.clicked += PlayButtonOnClicked;
        _saveButton.clicked += SaveButtonOnClicked;
        _loadButton.clicked += LoadButtonOnClicked;
        _loginButton.clicked += LoginButtonOnClicked;
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }
    
    private void PlayButtonOnClicked()
    {
        Debug.Log("Play button clicked");
        count++;
        _displayTxt.text = count.ToString();
    }
    
    private void SaveButtonOnClicked()
    {
        Debug.Log("Save button clicked");
        PlayerPrefs.SetInt("count", count);
        PlayerPrefs.Save();
    }
    
    private void LoadButtonOnClicked()
    {
        Debug.Log("Load button clicked");
        count = PlayerPrefs.GetInt("count");
        _displayTxt.text = count.ToString();
    }

    private void LoginButtonOnClicked()
    {
        Debug.Log("Login button clicked");
        // 그냥 창을 하나 더 띄운다.. 그게 좋아뵘
        _doc.gameObject.SetActive(false);
    }
}
