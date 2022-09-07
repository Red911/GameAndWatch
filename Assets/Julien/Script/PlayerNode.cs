using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerNode : MonoBehaviour
{
    [SerializeField] private bool _isGoal;
    [SerializeField] private bool _isSpawn;
    
    [SerializeField] private PlayerNode _up;
    [SerializeField] private PlayerNode _down;
    [SerializeField] private PlayerNode _left;
    [SerializeField] private PlayerNode _right;

    [SerializeField] private GameObject _deathDetection;
    
    private bool m_isActivated;

    public bool IsActivated
    {
        get => m_isActivated;
        set => m_isActivated = value;
    }

    public bool HasBeenHit
    {
        get => _deathDetection != null && _deathDetection.activeSelf;
    }

    public bool IsGoal
    {
        get => _isGoal;
        set => _isGoal = value;
    }

    public bool IsSpawn
    {
        get => _isSpawn;
        set => _isSpawn = value;
    }
    
    public PlayerNode UP
    {
        get => _up;
    }

    public PlayerNode DOWN
    {
        get => _down;
    }

    public PlayerNode LEFT
    {
        get => _left;
    }

    public PlayerNode RIGHT
    {
        get => _right;
    }

    public PlayerNode GetPlayerNodeWithMovement(Vector2 movement)
    {
        if (movement.x >= 1)
            return RIGHT;
        if (movement.x <= -1)
            return LEFT;
        if (movement.y >= 1)
            return UP;
        if (movement.y <= -1)
            return DOWN;

        return null;
    }
}
