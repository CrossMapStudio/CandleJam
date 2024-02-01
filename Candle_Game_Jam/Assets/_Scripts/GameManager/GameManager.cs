using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class GameManager
{
    // --- Game Over State
    // --- Game Win State
    // --- Game Pause State
    // --- Item Spawn

    public static void Game_Over(int GameState = 0)
    {
        if (GameState == 0)
            Debug.Log("You Suck!");
        else if (GameState == 1)
            Debug.Log("You Win!");
        else if (GameState == 2)
            Debug.Log("Game Paused");
    }
    // --- Handle Independent Stats --- Allows for Calling Static
}
