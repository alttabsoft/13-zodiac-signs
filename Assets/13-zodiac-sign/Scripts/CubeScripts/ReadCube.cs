using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ReadCube : MonoBehaviour
{
    public Transform tUp;
    public Transform tDown;
    public Transform tLeft;
    public Transform tRight;
    public Transform tFront;
    public Transform tBack;

    private List<GameObject> frontRays = new List<GameObject>();
    private List<GameObject> backRays = new List<GameObject>();
    private List<GameObject> upRays = new List<GameObject>();
    private List<GameObject> downRays = new List<GameObject>();
    private List<GameObject> leftRays = new List<GameObject>();
    private List<GameObject> rightRays = new List<GameObject>();   

    private string tagName = "Block";
    RotateCube rotateCube;
    public GameObject emptyGO;
       
    // Start is called before the first frame update
    void Start()
    {
        SetRayTransforms();

        rotateCube = FindObjectOfType<RotateCube>();
        ReadState();
        RotateCube.started = true;
    }

    // Update is called once per frame
    void Update()
    {
    }

    public void ReadState()
    {
        // RayCaseAll에서 감지된 모든 블록들의 배열을 가져오기
        // 어떤 색의 면이 어디에 있는지 알 수 있게 됨
        rotateCube.up = ReadFace(upRays, tUp);
        rotateCube.down = ReadFace(downRays, tDown);
        rotateCube.left = ReadFace(leftRays, tLeft);
        rotateCube.right = ReadFace(rightRays, tRight);
        rotateCube.front = ReadFace(frontRays, tFront);
        rotateCube.back = ReadFace(backRays, tBack);
    }


    void SetRayTransforms()
    {
        // Ray 위치에서 큐브의 중앙으로 RayCastAll을 하기
        upRays = BuildRays(tUp, new Vector3(90, 90, 0));
        downRays = BuildRays(tDown, new Vector3(270, 90, 0));
        leftRays = BuildRays(tLeft, new Vector3(0, 180, 0));
        rightRays = BuildRays(tRight, new Vector3(0, 0, 0));
        frontRays = BuildRays(tFront, new Vector3(0, 90, 0));
        backRays = BuildRays(tBack, new Vector3(0, 270, 0));
    }


    List<GameObject> BuildRays(Transform rayTransform, Vector3 direction)
    {
        // 우리는 맵의 정보를 0~24 번으로 정의
        int rayCount = 0;
        List<GameObject> rays = new List<GameObject>();

        for (int y = 2; y > -3; y--)
        {
            for (int x = -2; x < 3; x++)
            {
                Vector3 startPos = new Vector3( rayTransform.localPosition.x + x,
                                                rayTransform.localPosition.y + y,
                                                rayTransform.localPosition.z);
                GameObject rayStart = Instantiate(emptyGO, startPos, Quaternion.identity, rayTransform);
                rayStart.name = rayCount.ToString();
                rays.Add(rayStart);
                rayCount++;
            }
        }
        rayTransform.localRotation = Quaternion.Euler(direction);
        return rays;

    }

    public List<List<GameObject>> ReadFace(List<GameObject> rayStarts, Transform rayTransform)
    {
        List<List<GameObject>> facesHit = new List<List<GameObject>>();

        foreach (GameObject rayStart in rayStarts)
        {
            Vector3 ray = rayStart.transform.position;
            RaycastHit[] hits;

            hits = Physics.RaycastAll(ray, rayTransform.forward, Mathf.Infinity);

            if (hits.Length > 0)
            {
                List<GameObject> newList = new List<GameObject>();
                
                foreach (RaycastHit hit in hits)
                {
                    if (hit.collider.gameObject.CompareTag(tagName))
                    {
                        Debug.DrawRay(ray, rayTransform.forward * hit.distance, Color.red);
                        newList.Add(hit.collider.gameObject);
                        Debug.Log(hit.collider.gameObject.name);
                    }
                }

                facesHit.Add(newList);
            }
            else
            {
                Debug.DrawRay(ray, rayTransform.forward * 1000, Color.green); // 충돌하지 않았을 경우의 Ray 표시
            }
            
        }
        return facesHit;
    }

}


