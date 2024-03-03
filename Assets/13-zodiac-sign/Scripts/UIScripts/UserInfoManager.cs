using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

public class UserInfoManager : MonoBehaviour //상위 클래스로 UserManager를 고려중입니다.
{
    // 다른 스크립트에서 인스턴스에 접근하기 위한 프로퍼티
    // 인스턴스가 없는 경우 생성
    // Scene 전환 시 파괴되지 않도록 설정
    public static UserInfoManager Inst
    { get
        {
            if (_inst == null)
            {
                GameObject container = new GameObject("UserEmailManager");
                _inst = container.AddComponent<UserInfoManager>();
                DontDestroyOnLoad(container);
            }
            return _inst;
        }
    }
    // 전역적으로 접근 가능한 유저 이메일 변수, 메소드 선언
    public string UserEmail
    {
        get { return _userEmail; }
        set
        {
            if (_userEmail != value)
            {
                _userEmail = value;
                _isUserEmailChanged = true;
            }
        }
    }

    public bool IsUserEmailChanged
    {
        get => _isUserEmailChanged;
        set => _isUserEmailChanged = value;
    }

    // 전역적으로 접근 가능한 유저 JwT토큰 변수, 메소드 선언
    public string UserJwtToken
    {
        get { return _userJwtToken; }
        set { _userJwtToken = value; }
    }
    
    // 전역적으로 접근 가능한 유저 Csrf토큰 변수, 메소드 선언
    public string CsrfToken
    {
        get { return _csrfToken; }
        set { _csrfToken = value; }
    }
    
    private static UserInfoManager _inst = null;
    
    private String _userEmail = null;
    private String _userJwtToken = null;
    private String _csrfToken = null;

    private bool _isUserEmailChanged = false;

    private void Awake()
    {
        // Singleton Pattern

        #region Singleton Pattern

        if (_inst == null)
        {
            _inst = FindAnyObjectByType<UserInfoManager>();
            if (_inst == null)
            {
                _inst = this;
            }
        }
        else
        {
            Destroy(this.gameObject);
        }

        #endregion
    }
    
    [SerializeField]private bool condition;
}