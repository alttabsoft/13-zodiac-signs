using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    public bool isPlayer;
    public int CurrentX
    {
        set;
        get;
    }

    public int CurrentY
    {
        set;
        get;
    }

    public void SetPosition(int x, int y)
    {
        CurrentX = x;
        CurrentY = y;
    }

    public virtual bool[,] PossibleMoves()
    {
        return new bool[5, 5];
    }
    
    public bool Move(int x, int y, ref bool[,] r)
    {
        if (x >= 0 && x < 5 && y >= 0 && y < 5)
        {
            Player p = BoardManager.Instance.PlayerAxis[x, y];
            
            if (p == null)
                r[x, y] = true;
            else
            {
                if (isPlayer != p.isPlayer)
                    r[x, y] = true;
                return true;
            }
        }
        return false;
    }
}
