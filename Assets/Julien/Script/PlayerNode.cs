using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerNode : MonoBehaviour
{
    [SerializeField] private PlayerNode _up;
    [SerializeField] private PlayerNode _down;
    [SerializeField] private PlayerNode _left;
    [SerializeField] private PlayerNode _right;

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
