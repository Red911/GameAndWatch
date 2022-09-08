using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class BoutonPlay : MonoBehaviour
{
    public int levelIndex;
    public void Load()
    {
        SceneManager.LoadScene(levelIndex);
    }
}
