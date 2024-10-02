using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    private Vector3 _posOffset; // Camera's position relative to the platform it is centered on
    private float duration = 1;

    private void Start()
    {
        _posOffset = transform.position;
    }

    IEnumerator SmoothPan(Vector3 dest, float duration)
    {
        Vector3 start = transform.position;
        float time = 0;
        while (time < duration)
        {
            float u = time / duration;
            transform.position = Vector3.Lerp(start, dest, CubiceaseInOutCubicBezier(u));
            time += Time.deltaTime;
            yield return null;
        }
    }

    private float CubiceaseInOutCubicBezier(float u)
    {
        return u < 0.5 ? 4 * u * u * u : 1 - Mathf.Pow(-2 * u + 2, 3) / 2;
    }

    public void MoveCamera(Vector3 currentPlatformPos)
    {
        StartCoroutine(SmoothPan(currentPlatformPos + _posOffset, 1));
    }


}
