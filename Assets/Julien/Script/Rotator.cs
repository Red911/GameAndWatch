using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class Rotator : MonoBehaviour
{

    // Update is called once per frame
    void Update()
    {
        transform.LookAt(new Vector3(Mouse.current.position.ReadValue().x, 10, Mouse.current.position.ReadValue().y));
    }
}
