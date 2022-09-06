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

    private PlayerNode m_currentNode;

    private void Awake()
    {
        m_currentNode = _baseNode;
    }

    public void OnInputMove(InputAction.CallbackContext ctx)
    {
        Vector2 movement = ctx.ReadValue<Vector2>();
        
        if (movement != Vector2.zero && ctx.performed)
        {
            PlayerNode wantedNode = m_currentNode.GetPlayerNodeWithMovement(movement);
            if (wantedNode != null)
            {
                Image tempImage = m_currentNode.GetComponent<Image>();
                var tempColor = tempImage.color;
                tempColor.a = _opacityDisable;
                tempImage.color = tempColor;

                tempImage = wantedNode.GetComponent<Image>();
                tempColor = tempImage.color;
                tempColor.a = _opacityActive;
                tempImage.color = tempColor;
                
                
                m_currentNode = wantedNode;
            }
        }
    }
}