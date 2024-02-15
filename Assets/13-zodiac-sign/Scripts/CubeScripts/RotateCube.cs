using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using UnityEngine;
using Debug = UnityEngine.Debug;

public class RotateCube : MonoBehaviour
{
    // sides
    public List<List<GameObject>> front = new List<List<GameObject>>();
    public List<List<GameObject>> back = new List<List<GameObject>>();
    public List<List<GameObject>> up = new List<List<GameObject>>();
    public List<List<GameObject>> down = new List<List<GameObject>>();
    public List<List<GameObject>> left = new List<List<GameObject>>();
    public List<List<GameObject>> right = new List<List<GameObject>>();

    public static bool autoRotating = false;
    public static bool started = false;
    
    private Vector3 centerPoint; // 중심점
    private float rotationAmount = 90f; // 회전할 각도
    private float rotationSpeedPerMinute = 90f; // 회전에 소요될 시간 (초)
    
    // Start is called before the first frame update
    void Start()
    {
    
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            PressKey(front, 0);
        }
        if (Input.GetKeyDown(KeyCode.Alpha2))
        {
            PressKey(front, 1);
        }
        if (Input.GetKeyDown(KeyCode.Alpha3))
        {
            PressKey(front, 2);
        }
        if (Input.GetKeyDown(KeyCode.Alpha4))
        { 
            PressKey(front, 3);
        }
        if (Input.GetKeyDown(KeyCode.Alpha5))
        {
            PressKey(front, 4);
        }
        if (Input.GetKeyDown(KeyCode.Q))
        {
            SwitchRotateDirection();
        }
    }

    public void SwitchRotateDirection()
    {
        if (rotationAmount == 90f)
        {
            rotationAmount = -90f;
        }
        else
        {
            rotationAmount = 90f;
        }
    }
    
    public void PressKey(List<List<GameObject>> lists ,int k)
    {
        CalculateCenterPoint(lists, k);
        StartCoroutine(RotateObjectsAroundCenter(lists ,rotationAmount, k)); // 90도, 2초 동안 회전
    }
    
    public void CalculateCenterPoint(List<List<GameObject>> list, int k)
    {
        int idx = 0;
        if (list.Count == 0) return;
        centerPoint = new Vector3(0,0,0);

        Vector3 sum = Vector3.zero;
        foreach (List<GameObject> objs in list)
        {
            foreach (GameObject obj in objs)
            {
                if (idx % 5 == k)
                {
                    sum += obj.transform.position;
                }
                idx++;
            }
        }
        centerPoint = sum / list.Count;
    }

    public IEnumerator RotateObjectsAroundCenter(List<List<GameObject>> lists, float totalRotation, int l)
    {
        float currentRotation = 0;
        float direction = Mathf.Sign(totalRotation); // 회전 방향

        while ((direction == 1 && currentRotation < totalRotation) || (direction == -1 && currentRotation > totalRotation))
        {
            float step = rotationSpeedPerMinute * Time.deltaTime; // 이번 업데이트에서 회전해야 할 각도
            float rotationThisFrame = direction * Mathf.Min(Mathf.Abs(step), Mathf.Abs(totalRotation - currentRotation));
            int i = 0;
            foreach ( List<GameObject> k in lists)
            {
                foreach (GameObject m in k)
                {
                    if (i % 5 == l)
                    {
                        m.transform.RotateAround(centerPoint, Vector3.left, rotationThisFrame);
                    }
                    i++;
                }
            }
            currentRotation += direction * Mathf.Abs(rotationThisFrame); // 현재 회전량 업데이트
            yield return null;
        }
        
        
    }
}
