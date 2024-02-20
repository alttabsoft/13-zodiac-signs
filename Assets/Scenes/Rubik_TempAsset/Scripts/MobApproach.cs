using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MobApproach : MonoBehaviour
{
    private Transform playerTransform;

    // Start is called before the first frame update
    void Start()
    {
        playerTransform = GameObject.FindGameObjectWithTag("Player").transform;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        transform.position = Vector3.MoveTowards(transform.position, playerTransform.position,
            2 * Time.deltaTime);
    }
}
