using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : Player
{
    public override bool[,] PossibleMoves()
    {
        bool[,] r = new bool[5, 5];

        for (int i = -1; i < 2; i++)
        {
            for (int j = -1; j < 2; j++)
            {
                Move(CurrentX + i, CurrentY + j, ref r);
            }
        }

        return r;
    }
}