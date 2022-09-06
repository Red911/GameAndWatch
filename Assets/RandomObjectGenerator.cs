﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RandomObjectGenerator : MonoBehaviour
{
    public GameBoard gameBoard;

    public int columns = 0;
    private int row = 0;
    private float speed = 1f;
    

    IEnumerator Start()
    {
        while (true)
        {
            float randomTime = Random.Range(0, 6f);
            yield return new WaitForSeconds(randomTime);
            row = 0;
            

           do
            {
                gameBoard.SetValueAt(columns, row);
                yield return new WaitForSeconds(speed);
                gameBoard.SetValueAt(columns, row, false);
                row++;
                
            }
            while (row <= 2) ;


        }
        
    }
}