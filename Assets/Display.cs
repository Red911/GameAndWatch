using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class Display : MonoBehaviour
{
    private GameObject[,] grid = new GameObject[5, 4];
    
    
    [Tooltip("la table de jeu qui quel objet sont affiché")]
    public GameBoard gameBoard;

    [Tooltip("Parents games objects qui contienne les obstacles")]
    public GameObject[] columns;

    

    private void Start()
    {
        SetupGrid();
        
        StartCoroutine(UpdateDisplay());
    }

    private void SetupGrid()
    {
        for (int x = 0; x < columns.Length; x++)
        {
            int y = 0;
            foreach (Transform i in columns[x].transform)
            {
                grid[x, y] = i.gameObject;
                y++;
            }
        }
        
    }
    
    IEnumerator UpdateDisplay()
    {
        while(true)
        {
            for (int y = 0; y < 4; y++)
            {
                for (int x = 0; x < 5; x++)
                {
                    if (grid[x, y] != null)
                    {
                        // grid[x, y].SetActive(gameBoard.GetValueAt(x, y));
                        // Debug.Log(grid[x, y].GetComponent<MeshRenderer>().material.name);
                        Material mat = grid[x, y].GetComponent<MeshRenderer>().material;
                        mat.color = new Color(mat.color.r, mat.color.g, mat.color.b, gameBoard.GetValueAt(x, y) ? 1f : .3f);
                        // grid[x, y].GetComponent<MeshRenderer>().material = mat;
                    }
                }
            }
            yield return null;
        }
        
    }
}
