using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class RotateCube : MonoBehaviour
{
    // Event 
    public event EventHandler onCubeRotationEnd; 
    
    // sides
    public List<List<GameObject>> front = new List<List<GameObject>>();
    public List<List<GameObject>> back = new List<List<GameObject>>();
    public List<List<GameObject>> up = new List<List<GameObject>>();
    public List<List<GameObject>> down = new List<List<GameObject>>();
    public List<List<GameObject>> left = new List<List<GameObject>>();
    public List<List<GameObject>> right = new List<List<GameObject>>();
    
    // ===== 필요 없을 시 수정
    public static bool autoRotating = false;
    public static bool started = false;
    // ==== 필요 없을 시 수정

    private bool isRotating = false;
    private int[] randomLine = new int[3];
    private float rotationAmount = 90f; // 회전할 각도
    private Vector3 centerPoint; // 중심점
    private float rotationSpeedPerMinute = 90f; // 회전에 소요될 시간 (초)

    private int[] testArray = new int[3] { 0, 1, 2 };

    private void Awake()
    {
        // Cube Manager에 이벤트 추가
        CubeManager.Instance.tempRotateCube += rotatingCubes;
            
        
        // 랜덤한 라인 인덱스 받아오는 코드
        // CubeManager.Instance.onCallRotateCube += rotatingCubes;
    }
    
    // ==== 임의의 3 라인을 회전하는 코드
    private void rotatingCubes(object sender, EventArgs e)
    {
        if (isRotating)
        {
            return;
        }
        
        // 임의의 3개의 줄 회전
        StartCoroutine(startToRotateCube());
    }
    
    private IEnumerator startToRotateCube()
    {
        isRotating = true;
        // 세로 & 시계 방향 회전
        yield return StartCoroutine(RotateObjectsAroundCenter(front , Vector3.left, rotationAmount,0));
        yield return new WaitForSeconds(0.5f);
        // 가로 & 반시계방향 회전
        yield return StartCoroutine(RotateObjectsAroundCenter(down, Vector3.up, rotationAmount * -1, 3));
        yield return new WaitForSeconds(0.5f);
        // 세로 && 시계 방향 회전
        yield return StartCoroutine(RotateObjectsAroundCenter(left, Vector3.forward, rotationAmount, 3));
        // 회전 방향 변경
        rotationAmount *= -1;
        isRotating = false;
    }
    // ==== 임의의 3 라인을 회전하는 코드
    
    
    
    // ==== 랜덤한 라인 인덱스 받아오는 코드
    // private void rotatingCubes(object sender, CustomEventArgs e)
    // {
    //     randomLine = e.MyArray;
    //     // 임의의 3개의 줄 회전
    //     StartCoroutine(startToRotateCube());
    // }
    //
    // private IEnumerator startToRotateCube()
    // {
    //     // 세로 & 시계 방향 회전
    //     yield return StartCoroutine(RotateObjectsAroundCenter(front , Vector3.left, rotationAmount,randomLine[0]));
    //     yield return new WaitForSeconds(0.5f);
    //     // 가로 & 반시계방향 회전
    //     yield return StartCoroutine(RotateObjectsAroundCenter(left, Vector3.forward, rotationAmount * -1, randomLine[1]));
    //     yield return new WaitForSeconds(0.5f);
    //     // 세로 && 시계 방향 회전
    //     yield return StartCoroutine(RotateObjectsAroundCenter(front, Vector3.left, rotationAmount, randomLine[2]));
    //     // 회전 방향 변경
    //     rotationAmount *= -1;
    // }
    // ==== 랜덤한 라인 인덱스 받아오는 코드
    
    
    // 회전 하는 면의 중간 큐브를 계산
    private void CalculateCenterPoint(List<List<GameObject>> list, int k)
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

    // 회전하는 면의 리스트 & 회전 기준축 & 회전 방향 & 회전하는 줄
    public IEnumerator RotateObjectsAroundCenter(List<List<GameObject>> lists, Vector3 rotationDirection, float totalRotation, int l)
    {
        CalculateCenterPoint(lists, l); // 회전하는 면의 해당 줄의 중간 큐브를 계산
        
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
                        // 중간 큐브를 기준으로 rotationDirection(기준이 되는 회전축)으로 회전함
                        m.transform.RotateAround(centerPoint, rotationDirection, rotationThisFrame);
                    }
                    i++;
                }
            }
            currentRotation += direction * Mathf.Abs(rotationThisFrame); // 현재 회전량 업데이트
            yield return null;
        }
        
        // 만약 이벤트 핸들가 비어있지 않다면 함수 호출
        onCubeRotationEnd?.Invoke(this, EventArgs.Empty);
    }
}