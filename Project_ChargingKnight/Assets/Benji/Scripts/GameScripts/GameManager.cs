using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class GameManager
{

    public static int defaultPlayerNum = 1;
    public static int playersNmbrs = 1;
    public static float playersHeight = 0.25f;
    public static Color P1Color = Color.red;
    public static Color P2Color = Color.blue;

    public static Color SetPlayerColor(int playerNum)
    {
        if (playerNum == 1)
        {
            return P1Color;
        }
        else
        if (playerNum == 2)
        {
            return P2Color;
        }else
        {
            return Color.white;
        }

    }
}


