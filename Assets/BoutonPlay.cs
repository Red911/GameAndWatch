using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class BoutonPlay : MonoBehaviour
{
    public GameObject credit;
    
    public int levelIndex;
    public void Load()
    {
        SceneManager.LoadScene(levelIndex);
    }

    public void OnCredit()
    {
        credit.gameObject.SetActive(true);
    }
    
    public void OffCredit()
    {
        credit.gameObject.SetActive(false);
    }
    

    public void Quit()
    {
        Application.Quit();
    }
}
