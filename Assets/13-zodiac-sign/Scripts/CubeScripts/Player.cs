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
        if (x >= -2 && x < 3 && y >= -2 && y < 3)
        {
            Player p = BoardManager.Instance.PlayerAxis[x + 2, y + 2];
            
            if (p == null)
                r[x + 2, y + 2] = true;
            else
            {
                if (isPlayer != p.isPlayer)
                    r[x + 2, y + 2] = true;
                return true;
            }
        }
        return false;
    }
}
