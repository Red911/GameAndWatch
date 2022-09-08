using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameNWatchGameChanger : MonoBehaviour
{
    [SerializeField] private List<Material> Games;
    [SerializeField] private MeshRenderer Screen;
    [SerializeField] private GameObject v2;
    [SerializeField] private List<GameObject> GameScene;

    private int currentGame = 0;

    public void SwitchGame(int game)
    {
        Screen.material = Games[game];
        v2.SetActive(true);
        GameScene[currentGame].SetActive(false);
        GameScene[game].SetActive(true);
        currentGame = game;
    }
}
