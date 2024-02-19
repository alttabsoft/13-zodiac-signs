using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoardManager : MonoBehaviour
{
    public static BoardManager Instance
    {
        get;
        set;
    }

    private bool[,] allowedMoves
    {
        get;
        set;
    }

    private float rawSelectionX = 0.0f;
    private float rawSelectionY = 0.0f;
    
    private int selectionX = -1;
    private int selectionY = -1;

    public GameObject playerPrefab;
    public GameObject enemyPrefab;
    private List<GameObject> activePlayer;
    
    // 칸의 개수
    private static readonly int xLen = 5;
    private static readonly int yLen = 5;
    // 한칸의 크기
    private static readonly float tileSize = 1.0f;
    
    private Quaternion whiteOrientation = Quaternion.Euler(0, 0, 0);
    private Quaternion blackOrientation = Quaternion.Euler(0, 180, 0);

    public Player[,] PlayerAxis
    {
        get;
        set;
    }
    private Player selectedPlayer = null;
    private Player selectedEnemy;

    public bool isPlayerTurn = true;

    private Material previousMat;
    public Material selectedMat;

    // Use this for initialization
    void Start()
    {
        Instance = this;
        PlayerAxis = new Player[xLen, yLen]; // 배열을 초기화합니다.
        activePlayer = new List<GameObject>(); // activePlayer 리스트를 초기화합니다.
        SpawnPlayer(0, -2, true);
        SpawnPlayer(0, 2, false);
    }

    // Update is called once per frame
    void Update()
    {
        UpdateSelection();

        if (Input.GetMouseButtonDown(0))
        {
            if (selectionX >= -2 && selectionY >= -2)
            {
                if (selectedPlayer == null)
                {
                    SelectPlayer(selectionX, selectionY);
                }
                else
                {
                    MovePlayer(selectionX, selectionY);
                }
            }
        }

        if (Input.GetKey("escape"))
            Application.Quit();
    }

    // TODO: 유저턴으로 넘어오면 자동으로 SelectPlayer 상태로 바꾸기
    private void SelectPlayer(int x, int y)
    {
        Debug.Log("X: " + x + " Y: " + y);
        Debug.Log("X hit point: " + rawSelectionX + " Y hit point: " + rawSelectionY);
        if (PlayerAxis[x + 2, y + 2] == null) return;

        if (PlayerAxis[x + 2, y + 2].isPlayer != isPlayerTurn) return;

        bool hasAtLeastOneMove = false;

        allowedMoves = PlayerAxis[x + 2, y + 2].PossibleMoves();
        for (int i = 0; i < 5; i++)
        {
            for (int j = 0; j < 5; j++)
            {
                if (allowedMoves[i, j])
                {
                    hasAtLeastOneMove = true;
                    i = 5;
                    break;
                }
            }
        }

        if (!hasAtLeastOneMove)
            return;

        selectedPlayer = PlayerAxis[x + 2, y + 2];
        previousMat = selectedPlayer.GetComponent<MeshRenderer>().material;
        selectedMat.mainTexture = previousMat.mainTexture;
        selectedPlayer.GetComponent<MeshRenderer>().material = selectedMat;

        BoardHighlights.Instance.HighLightAllowedMoves(allowedMoves);
    }

    private void MovePlayer(int x, int y)
    {
        if (allowedMoves[x + 2, y + 2])
        {
            Player p = PlayerAxis[x + 2, y + 2];

            PlayerAxis[selectedPlayer.CurrentX + 2, selectedPlayer.CurrentY + 2] = null;
            selectedPlayer.transform.position = GetTileCenter(x, y);
            selectedPlayer.SetPosition(x, y);
            PlayerAxis[x + 2, y + 2] = selectedPlayer;
            isPlayerTurn = !isPlayerTurn;
        }

        selectedPlayer.GetComponent<MeshRenderer>().material = previousMat;

        BoardHighlights.Instance.HideHighlights();
        selectedPlayer = null;
    }

    private void UpdateSelection()
    {
        if (!Camera.main) return;

        RaycastHit hit;
        if (Physics.Raycast(Camera.main.ScreenPointToRay(Input.mousePosition), out hit, 50.0f, LayerMask.GetMask("CubeBoardPlane")))
        {
            rawSelectionX = hit.point.x;
            rawSelectionY = hit.point.z;
            // selectionX = (int)(hit.point.x + 0.5f);
            // selectionY = (int)(hit.point.z + 0.5f);
            if (hit.point.x > 0.5)
            {
                selectionX = (int)(hit.point.x + 0.5f);
            }
            else
            {
                selectionX = (int)(hit.point.x - 0.5f);
            }

            if (hit.point.z > 0.5)
            {
                selectionY = (int)(hit.point.z + 0.5f);
            }
            else
            {
                selectionY = (int)(hit.point.z - 0.5f);
            }
        }
        else
        {
            selectionX = -3;
            selectionY = -3;
        }
    }

    // Spawn function for player and enemy
    private void SpawnPlayer(int x, int y, bool isPlayer)
    {
        Vector3 position = GetTileCenter(x, y);
        GameObject go;

        if (isPlayer)
        {
            go = Instantiate(playerPrefab, position, whiteOrientation) as GameObject;
        }
        else
        {
            go = Instantiate(enemyPrefab, position, blackOrientation) as GameObject;
        }

        Vector3 temp;
        
        temp = go.transform.localScale;
        temp.x *= 0.5f;
        temp.y *= 0.5f;
        temp.z *= 0.5f;
        go.transform.localScale = temp;
        
        go.transform.SetParent(transform);
        if (go.GetComponent<Player>() == null)
        {
            Debug.LogError("Player component not found on instantiated GameObject.");
            return;
        }
        PlayerAxis[x + 2, y + 2] = go.GetComponent<Player>();
        PlayerAxis[x + 2, y + 2].SetPosition(x, y);
        activePlayer.Add(go);
    }

    private Vector3 GetTileCenter(int x, int y)
    {
        Vector3 origin = Vector3.zero;
        origin.x += (tileSize * x);
        origin.z += (tileSize * y);
        origin.y = 2.6f;

        return origin;
    }
}
