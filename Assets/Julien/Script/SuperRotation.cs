using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SuperRotation : MonoBehaviour
{
    private Animator _animator;
    private int _triggerRotationId;

    private void Awake()
    {
        _animator = GetComponent<Animator>();
        _triggerRotationId = Animator.StringToHash("Rotation");
    }

    public void MakeItRotate()
    {
        _animator.SetTrigger(_triggerRotationId);
    }
}
