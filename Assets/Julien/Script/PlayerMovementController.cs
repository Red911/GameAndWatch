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

    private PlayerNode m_currentNode;

    private bool m_hasPatient;
    private int m_score;

    private void Awake()
    {
        m_currentNode = _baseNode;
        ActivateObject(m_currentNode, m_currentNode);
    }

    private void Update()
    {
        CheckDeath();
    }

    public void OnInputMove(InputAction.CallbackContext ctx)
    {
        Vector2 movement = ctx.ReadValue<Vector2>();
        
        if (movement != Vector2.zero && ctx.performed)
        {
            PlayerNode wantedNode = m_currentNode.GetPlayerNodeWithMovement(movement);
            if (wantedNode != null)
            {
                ActivateObject(m_currentNode, wantedNode);
                
                m_currentNode = wantedNode;
                if (m_currentNode.IsGoal)
                {
                    m_hasPatient = true;
                }
                else if (m_currentNode.IsSpawn && m_hasPatient)
                {
                    m_hasPatient = false;
                    m_score++;
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
            // Your dead;
            Debug.Log($"Your died with a score of : {m_score}");
            ActivateObject(m_currentNode, _baseNode);
            m_currentNode = _baseNode;
        }
    }
}