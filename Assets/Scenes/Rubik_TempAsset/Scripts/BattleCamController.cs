using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BattleCamController : MonoBehaviour
{
    [SerializeField] private GameObject Cam;
    [SerializeField] private GameObject Shadow;

    private Vector3 Gap;
    
    private void Start()
    {
        Gap = Cam.transform.position - Shadow.transform.position;
    }

    void FixedUpdate()
    {
        if (Shadow.transform.position.x > -0.15)
        {
            if (Shadow.transform.position.x < 10.05)
            {
                Cam.transform.position = new Vector3(Shadow.transform.position.x + Gap.x, Cam.transform.position.y, Cam.transform.position.z);
            }
        }
    }
}
