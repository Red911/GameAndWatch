using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;


public class PlayerMovementController : MonoBehaviour
{
    [SerializeField] private PlayerNode _baseNode;
    [SerializeField] private float _opacityActive = 1f;
    [SerializeField] private float _opacityDisable = .3f;
    [SerializeField] private bool _useImage;

    [SerializeField] private GameEvent DeathEvent;
    [SerializeField] private GameEventInt UpdateScoreEvent;
    [SerializeField] private GameObject _Patient;

    private PlayerNode m_currentNode;

    private bool m_hasPatient;
    private int m_score;

    public int life = 3;
    public GameBoard gameBoard;
    private bool isDead;

    public AudioManager audio;
    private void Awake()
    {
        m_currentNode = _baseNode;
        ActivateObject(m_currentNode, m_currentNode);
        UpdateScoreEvent.Raise(m_score);
    }

    private void Update()
    {
        CheckDeath();
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

    private void ActivateObject(PlayerNode previous, PlayerNode newposition)
    {
        if (_useImage)
        {
            Image tempImage = previous.GetComponent<Image>();
            var tempColor = tempImage.color;
            tempColor.a = _opacityDisable;
            tempImage.color = tempColor;

            tempImage = newposition.GetComponent<Image>();
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

    private void CheckDeath()
    {
        if (m_currentNode.HasBeenHit)
        {
            isDead = true;
            audio.playerAudio.PlayOneShot(audio.hit);
            // Your dead;
            life--;
            StartCoroutine(SlowTime());
            Debug.Log($"Your died with a score of : {m_score}");
            
            DeathEvent.Raise();
            m_hasPatient = false;
            _Patient.SetActive(true);
            
            if (life <= 0)
            {
                life = 0;
                m_score = 0;
                UpdateScoreEvent.Raise(m_score);
            }
            

        }
    }

    IEnumerator SlowTime()
    {
        Time.timeScale = 0.00001f;
        gameBoard.Clear();
        yield return new WaitForSecondsRealtime(5f);
        Time.timeScale = 1f;
        
        if (isDead)
        {
            ActivateObject(m_currentNode, _baseNode);
            m_currentNode = _baseNode;
            isDead = false;
        }
    }
    

}