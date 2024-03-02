using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class MobApproach : MonoBehaviour
{
    public GameObject canvas;
    public GameObject hpBarPrefab;
    public GameObject hpBarInstance;
    private Transform playerTransform;
    
    private float gap = 0.2f;

    // Start is called before the first frame update
    void Start()
    {
        playerTransform = GameObject.FindGameObjectWithTag("Shadow").transform;
        hpBarInstance = Instantiate(hpBarPrefab, canvas.transform);
    }
    
    // Update is called once per frame
    void FixedUpdate()
    {
        transform.position = Vector3.MoveTowards(transform.position, playerTransform.position,
            2 * Time.deltaTime);
        
        hpBarInstance.transform.position = Camera.main.WorldToScreenPoint(transform.position + new Vector3(0, -gap, gap));
    }
}
