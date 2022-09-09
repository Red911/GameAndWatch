using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;
using UnityEngine.UI;


public class PlayerMovementController : MonoBehaviour
{
    [SerializeField] private int _gameIndex = 1;
    
    [SerializeField] private PlayerNode _baseNode;
    [SerializeField] private float _opacityActive = 1f;
    [SerializeField] private float _opacityDisable = .3f;
    [SerializeField] private bool _useImage;

    [SerializeField] private GameEventInt DeathEvent;
    [SerializeField] private GameEventInt WinEvent;
    [SerializeField] private GameEventInt UpdateScoreEvent;
    [SerializeField] private GameEventInt UpdateTimerEvent;
    [SerializeField] private GameObject _Patient;
    
    [SerializeField] private float _TimerInSecond;

    private PlayerNode m_currentNode;

    private bool m_hasPatient;
    private int m_score;

    public int life = 3;
    public GameBoard gameBoard;
    private bool isDead;

    private float _timer;

    public AudioManager audio;
    private void Awake()
    {
        m_currentNode = _baseNode;
        ActivateObject(m_currentNode, m_currentNode);
        UpdateScoreEvent.Raise(m_score);
        if (_gameIndex == 2)
        {
            _timer = _TimerInSecond;
            UpdateTimerEvent.Raise(Mathf.RoundToInt(_timer));
        }
    }

    private void Update()
    {
        if (_gameIndex == 1)
        {
            audio.enviromentAudio.clip = audio.birdsSounds;
            audio.enviromentAudio.loop = true;
            CheckHasWinGame1();
            CheckDeathGame1();
        }
        else if (_gameIndex == 2)
        {
            audio.enviromentAudio.clip = audio.carDriving;
            audio.enviromentAudio.loop = true;
            CheckDeathGame2();
            _timer -= Time.deltaTime;
            UpdateTimerEvent.Raise(Mathf.RoundToInt(_timer));

            if (_timer <= 0)
            {
                // Win
                Debug.Log("Victoire !!!!!!!");
                WinEvent.Raise(2);
            }
        }
    }

    public void OnInputMove(InputAction.CallbackContext ctx)
    {
        Vector2 movement = ctx.ReadValue<Vector2>();
        
        if (movement != Vector2.zero && ctx.performed && isDead == false)
        {
            audio.playerAudio.PlayOneShot(audio.movement);
            PlayerNode wantedNode = m_currentNode.GetPlayerNodeWithMovement(movement);
            if (wantedNode != null)
            {
                ActivateObject(m_currentNode, wantedNode);
                
                m_currentNode = wantedNode;

                if (_gameIndex == 1)
                {
                    if (m_currentNode.IsGoal)
                    {
                        audio.playerAudio.PlayOneShot(audio.getPatient);
                        m_hasPatient = true;
                        _Patient.SetActive(false);
                    }
                    else if (m_currentNode.IsSpawn && m_hasPatient)
                    {

                        m_hasPatient = false;
                        _Patient.SetActive(true);
                        m_score++;
                        UpdateScoreEvent.Raise(m_score);
                        Debug.Log($"You save a patient new score : {m_score}");
                    }
                }
            }
        }
    }

    private void ActivateObject(PlayerNode previous, PlayerNode newposition)
    {
        if (_useImage)
        {
            SpriteRenderer tempImage = previous.GetComponent<SpriteRenderer>();
            var tempColor = tempImage.color;
            tempColor.a = _opacityDisable;
            tempImage.color = tempColor;

            tempImage = newposition.GetComponent<SpriteRenderer>();
            tempColor = tempImage.color;
            tempColor.a = _opacityActive;
            tempImage.color = tempColor;
        }
        else
        {
            Material tempMaterial = previous.GetComponent<MeshRenderer>().material;
            var tempColor = tempMaterial.color;
            tempColor.a = _opacityDisable;
            tempMaterial.color = tempColor;

            tempMaterial = newposition.GetComponent<MeshRenderer>().material;
            tempColor = tempMaterial.color;
            tempColor.a = _opacityActive;
            tempMaterial.color = tempColor;
        }

        previous.IsActivated = false;
        newposition.IsActivated = true;
    }
    
    private void CheckDeathGame2()
    {
        if (m_currentNode.HasBeenHit)
        {
            isDead = true;
            audio.playerAudio.volume = 1f;
            audio.playerAudio.PlayOneShot(audio.hit);
            
            // Your dead;
            life--;
            StartCoroutine(SlowTime());
            UpdateScoreEvent.Raise(life);
            
            DeathEvent.Raise(2);


        }
    }

    private void CheckDeathGame1()
    {
        if (m_currentNode.HasBeenHit)
        {
            isDead = true;
            audio.playerAudio.volume = 1f;
            audio.playerAudio.PlayOneShot(audio.hit);
            audio.playerAudio.volume = 0.5f;
            
            // Your dead;
            life--;
            StartCoroutine(SlowTime());
            Debug.Log($"Your died with a score of : {m_score}");
            
            DeathEvent.Raise(1);
            m_hasPatient = false;
            _Patient.SetActive(true);


        }
    }

    public void CheckHasWinGame1()
    {
        if (m_score == 4)
        {
            audio.playerAudio.volume = 0.25f;
            audio.playerAudio.clip = audio.victory;
            audio.playerAudio.Play();
            audio.playerAudio.volume = 0.5f;
            WinEvent.Raise(1);
        }
    }

    IEnumerator SlowTime()
    {
        Time.timeScale = 0.00001f;
        gameBoard.Clear();
        yield return new WaitForSecondsRealtime(2f);
        Time.timeScale = 1f;
        
        if (isDead)
        {
            ActivateObject(m_currentNode, _baseNode);
            m_currentNode = _baseNode;
            isDead = false;
        }


        if (_gameIndex == 1)
        {
            if (life <= 0)
            {
                life = 0;
                m_score = 0;
                UpdateScoreEvent.Raise(m_score);
                SceneManager.LoadScene(0);
            }
        }
        else if (_gameIndex == 2)
        {
            if (life <= 0)
            {
                // Lose game
                SceneManager.LoadScene(0);
            }
        }
        
        
    }
    

}