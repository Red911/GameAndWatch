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
                ActivateObject(m_currentNode, wantedNode);
                
                m_currentNode = wantedNode;
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
    }
}