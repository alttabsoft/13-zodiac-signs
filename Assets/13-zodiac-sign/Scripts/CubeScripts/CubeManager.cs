using UnityEngine;
using System;
using System.Collections.Generic;
using Random = UnityEngine.Random;

public class CubeManager : MonoSingleTon<CubeManager>
{
    public event EventHandler tempRotateCube;

    public delegate void CustomEventHandler(object sender, CustomEventArgs e);
    public event CustomEventHandler onCallRotateCube;
    
    private RotateCube rotateCube;
    private int playerPositionedLine = 99;


    private int[] randomLine = new int[3];
    
    private void Start()
    {
        rotateCube = FindObjectOfType<RotateCube>();
    }

    private void Update()
    {
        // 테스트를 위한 코드
        if (Input.GetKeyDown(KeyCode.Space))
        {
            tempRotateCube?.Invoke(this, EventArgs.Empty);
            
            
            // 랜덤 라인 생성
            // setRandomLine();
            // onCallRotateCube?.Invoke(this, new CustomEventArgs(randomLine));
        }
    }
    
    
    
    
    // ====== 랜덤한 줄 생성을 위한 코드 
    private void rotateCubes(object sender, EventArgs e)
    {
        onCallRotateCube?.Invoke(this, new CustomEventArgs(randomLine));
    }

    private void setRandomLine()
    {
        int idx = 0;
        while (idx < 3)
        {
            int randomNumber = 0;
            
            do
            {
                randomNumber = Random.Range(0, 5);
                
            } while (randomNumber == playerPositionedLine);
            
            Debug.Log($"나의 랜덤 숫자 {randomNumber}");
            
            randomLine[idx] = randomNumber;
            idx++;
        }
    }
    // ====== 랜덤한 줄 생성을 위한 코드 
}
