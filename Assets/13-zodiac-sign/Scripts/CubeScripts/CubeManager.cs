using UnityEngine;
using System;
using Random = UnityEngine.Random;


public class CubeManager : MonoSingleTon<CubeManager>
{
    public event EventHandler tempRotateCube;

    public delegate void CustomEventHandler(object sender, CustomEventArgs e);
    public event CustomEventHandler onCallRotateCube;
    
    private RotateCube rotateCube;
    private int playerPositionedLine = 99;

    // 0 - 4 == 선의 인덱스, 5 == 회전축
    private int[] randomLine = new int[5];  
    

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

    
    // F = Front, L = Left
    // N % 2
    // 0 | 1 | 2 | 3 | 4 | 
    // F | L | F | L | F |
    // ====== 랜덤한 줄 생성을 위한 코드 
    private void rotateCubes(object sender, EventArgs e)
    {
        onCallRotateCube?.Invoke(this, new CustomEventArgs(randomLine));
    }

    private void setRandomLine()
    {
        int idx = 0;
        while (idx < 5)
        {
            int randomNumber = 0;
            
            do
            {
                randomNumber = Random.Range(0, 5);
                
            } while (randomNumber == playerPositionedLine);

            randomLine[idx] = randomNumber;
            idx++;
        }
    }
    // ====== 랜덤한 줄 생성을 위한 코드 
}
