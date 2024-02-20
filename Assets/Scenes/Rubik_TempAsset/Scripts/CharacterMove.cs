using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class CharacterMove : FloatingJoystick
{
    private GameObject player;
    //[SerializeField] GameObject playerBody;

    private CharacterController playerCharacterController;
    //private CharacterController playerBodyCharacterController;

    private bool isTriggerDown = false;
    
    // Start is called before the first frame update
    protected override void Start()
    {
        base.Start();
        background.gameObject.SetActive(false);
        player = GameObject.FindGameObjectWithTag("Player").gameObject;
        //playerBody= GameObject.FindGameObjectWithTag("PlayerBody").gameObject;
        
        playerCharacterController = player.GetComponent<CharacterController>();
        //playerBodyCharacterController = playerBody.GetComponent<CharacterController>();
    }

    // Update is called once per frame
    
    public override void OnPointerDown(PointerEventData eventData)
    {
        isTriggerDown = true;
        background.anchoredPosition = ScreenPointToAnchoredPosition(eventData.position);
        background.gameObject.SetActive(true);
        base.OnPointerDown(eventData);
    }
    
    public override void OnPointerUp(PointerEventData eventData)
    {
        background.gameObject.SetActive(false);
        base.OnPointerUp(eventData);
    }



    void FixedUpdate()
    {
        if (isTriggerDown)
        {
            if (player.transform.position.z <= -6.07)
            {
                if (player.transform.position.z >= -6.62429)
                {
                    playerCharacterController.Move(new Vector3(this.Horizontal, 0, this.Vertical).normalized * Time.deltaTime);
                }
                else
                { 
                    if (this.Vertical >= 0)
                    {
                        playerCharacterController.Move(new Vector3(this.Horizontal, 0, this.Vertical).normalized * Time.deltaTime);
                    }
                    else
                    {
                        playerCharacterController.Move(new Vector3(this.Horizontal, 0, 0).normalized * Time.deltaTime);
                    }
                }
            }
            else
            {
                if (this.Vertical <= 0)
                {
                    playerCharacterController.Move(new Vector3(this.Horizontal, 0, this.Vertical).normalized * Time.deltaTime);
                }
                else
                {
                    playerCharacterController.Move(new Vector3(this.Horizontal, 0, 0).normalized * Time.deltaTime);
                }
            }
        }
    }
}
