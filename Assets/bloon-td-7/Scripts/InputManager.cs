using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputManager : MonoBehaviour
{
    [SerializeField]
    private Camera SceneCamera;

    private Vector3 LastPos;

    [SerializeField]
    private LayerMask PlacementLayerMask;

    public (Vector3, bool) GetPlacementPosition()
    {
        Vector3 mousePos = Input.mousePosition;
        mousePos.z = SceneCamera.nearClipPlane;
        Ray mouseray = SceneCamera.ScreenPointToRay(mousePos);

        bool valid = false;
        RaycastHit hit;
        if (Physics.Raycast(mouseray, out hit, 1000, PlacementLayerMask) && (hit.transform.gameObject.tag != "BTD7PlacementBlocker"))
        {
            valid = true;
            LastPos = hit.point;
        }
        else
        {
            LastPos = new Vector3(0, -100, 0);
        }
        
        return (LastPos, valid);
    }
}