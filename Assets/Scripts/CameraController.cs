using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    private Vector3 _posOffset; // Camera's position relative to the platform it is centered on

    private void Start()
    {
        _posOffset = transform.position;
    }

    public void MoveCamera(Vector3 currentPlatformPos)
    {
        transform.position = currentPlatformPos + _posOffset;
    }
}
