using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class CharacterMove : FloatingJoystick
{
    private GameObject player;
    private GameObject shadow;
    //[SerializeField] GameObject playerBody;

    private CharacterController playerCharacterController;
    private CharacterController shadowCharacterController;
    //private CharacterController playerBodyCharacterController;

    private bool isTriggerDown = false;
    
    // Start is called before the first frame update
    protected override void Start()
    {
        base.Start();
        background.gameObject.SetActive(false);
        player = GameObject.FindGameObjectWithTag("Player").gameObject;
        shadow = GameObject.FindGameObjectWithTag("Shadow").gameObject;
        //playerBody= GameObject.FindGameObjectWithTag("PlayerBody").gameObject;
        
        playerCharacterController = player.GetComponent<CharacterController>();
        shadowCharacterController = shadow.GetComponent<CharacterController>();
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
            if (player.transform.position.z <= -5)
            {
                if (player.transform.position.z >= -5.48)
                {
                    playerCharacterController.Move(new Vector3(this.Horizontal, 0, this.Vertical).normalized * Time.deltaTime);
                    shadowCharacterController.Move(new Vector3(this.Horizontal, 0, this.Vertical).normalized * Time.deltaTime);
                }
                else
                { 
                    if (this.Vertical >= 0)
                    {
                        playerCharacterController.Move(new Vector3(this.Horizontal, 0, this.Vertical).normalized * Time.deltaTime);
                        shadowCharacterController.Move(new Vector3(this.Horizontal, 0, this.Vertical).normalized * Time.deltaTime);
                    }
                    else
                    {
                        playerCharacterController.Move(new Vector3(this.Horizontal, 0, 0).normalized * Time.deltaTime);
                        shadowCharacterController.Move(new Vector3(this.Horizontal, 0, 0).normalized * Time.deltaTime);
                    }
                }
            }
            else
            {
                if (this.Vertical <= 0)
                {
                    playerCharacterController.Move(new Vector3(this.Horizontal, 0, this.Vertical).normalized * Time.deltaTime);
                    shadowCharacterController.Move(new Vector3(this.Horizontal, 0, this.Vertical).normalized * Time.deltaTime);
                }
                else
                {
                    playerCharacterController.Move(new Vector3(this.Horizontal, 0, 0).normalized * Time.deltaTime);
                    shadowCharacterController.Move(new Vector3(this.Horizontal, 0, 0).normalized * Time.deltaTime);
                }
            }
        }
    }
}
