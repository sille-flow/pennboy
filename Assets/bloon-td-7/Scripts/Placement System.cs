using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlacementSystem : MonoBehaviour
{
    [SerializeField]
    private Camera SceneCamera;

    private Vector3 LastPos;

    [SerializeField]
    private LayerMask PlacementLayerMask;

    public Vector3 GetPlacementPosition()
    {
        Vector3 mousePos = Input.mousePosition;
        mousePos.z = SceneCamera.nearClipPlane;
        Ray mouseray = SceneCamera.ScreenPointToRay(mousePos);

        RaycastHit hit;
        if (Physics.Raycast(mouseray, out hit, 100, PlacementLayerMask))
        {
            LastPos = mousePos;
        }
        return LastPos;
    }
}
