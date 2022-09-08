using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameBoard : MonoBehaviour
{
    
    public bool[,] grid = new bool[4, 3];

    public bool GetValueAt(int x, int y)
    {
        return grid[x, y];
    }

    public void SetValueAt(int x, int y, bool newValue = true)
    {
        grid[x, y] = newValue;
    }

    public void Clear()
    {
        for (int y = 0; y < 3; y++)
        {
            for (int x = 0; x < 4; x++)
            {
                grid[x, y] = false;
            }
        }
    }

    private void Awake()
    {
        Clear();
    }
}
