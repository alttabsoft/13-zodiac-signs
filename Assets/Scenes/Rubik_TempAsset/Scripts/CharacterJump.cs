using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterJump : MonoBehaviour
{ 
    [SerializeField] CharacterController playerBodyCharacterController;

    private int jumpCount = 0;
    private bool isJumping = false;
    
    // Start is called before the first frame update
    void Start()
    {
        
    }

    public void PlayerJumpStart()
    {
        if (!isJumping)
        {
            isJumping = true;
            StartCoroutine(PlayerJump());
        }
    }
    private IEnumerator PlayerJump()
    {
        var jumpWeight = 0.03f;
        
        while (jumpCount < 10)
        {
            jumpCount++;
            playerBodyCharacterController.Move(new Vector3(0,0,2) * jumpWeight);
            yield return new WaitForFixedUpdate();
        }
        jumpCount = 0;
        
        while (jumpCount < 10)
        {
            jumpCount++;
            playerBodyCharacterController.Move(new Vector3(0,0,-2) * jumpWeight);
            yield return new WaitForFixedUpdate();
        }
        jumpCount = 0;
        isJumping = false;
        
        yield return null;
    }
    
    // Update is called once per frame
    void Update()
    {
    }
}
