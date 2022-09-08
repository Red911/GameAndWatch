using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RandomObjectGenerator : MonoBehaviour
{
    public GameBoard gameBoard;

    public int columns = 0;
    private int row = 0;
    public float speed = 10f;
    private int randomTime;
    public int randomMin;
    public int randomMax;
    

    IEnumerator Start()
    {
        while (true)
        {
            randomTime = Random.Range(randomMin, randomMax);
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
