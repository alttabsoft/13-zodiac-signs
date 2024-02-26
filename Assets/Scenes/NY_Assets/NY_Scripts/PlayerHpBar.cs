using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerHpBar : MonoBehaviour
{
    public Slider hpBar;
    // TODO: 액션씬 플레이어 데이터 따로 관리하기
    public float maxHp;
    public float currentHp;
    
    void Update()
    {
        hpBar.value = currentHp / maxHp;
    }
}
