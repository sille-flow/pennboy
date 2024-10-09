using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [SerializeField] float maxHeight = 2f;
    [SerializeField] float distance = 2.5f;
    [SerializeField] float Duration = 1f;
    [SerializeField] float minimumHeight = 5f;
    [SerializeField] PlatformManager platformManager; 
    [SerializeField] CameraController cameraController;

    public Transform m_Transform;
    public GameObject mainCamera;

    float jumpStartTime = 0;
    float jumpElapsedTime = 0;
    bool isJumping = false;
    Vector3 startingPos = Vector3.zero;
    Vector3 destination = Vector3.zero;
    float duration = 1;
    // Start is called before the first frame update
    void Start()
    {
        float duration = Duration;
        m_Transform = gameObject.GetComponent<Transform>();
        //mainCamera = GameObject.Find("")
    }

    // Update is called once per frame
    void Update()
    {
        if (!isJumping)
        {
            if (Input.GetKeyDown(KeyCode.Space))
            {
                jumpStartTime = Time.time;
                startingPos = m_Transform.position;
                //Debug.Log("Jump Held Down");
            }
            if (Input.GetKeyUp(KeyCode.Space) && jumpStartTime != 0)
            {
                jumpElapsedTime = Time.time - jumpStartTime;
                isJumping = true;
                if (jumpElapsedTime < 1) duration *= 0.5f;
                  else duration += jumpElapsedTime * 0.1f;
                //add 2 directions later
                destination = m_Transform.position +
                    m_Transform.forward * jumpElapsedTime * distance;
                //Debug.Log("Start Jumping");
            }
        }
        else
        {
            //Jumped to Pos
            if(Vector3.Distance(m_Transform.position,destination) < 0.05f)
            {
                //Debug.Log("Jump Ended");
                isJumping = false;
                jumpElapsedTime = 0;
                jumpStartTime = 0;
                m_Transform.position = destination;
                m_Transform.rotation =
                    Quaternion.Euler(0, m_Transform.rotation.y, m_Transform.rotation.z);
                destination = Vector3.zero;
                startingPos = Vector3.zero;
                duration = Duration;

                platformManager.JumpLanded();
                cameraController.MoveCamera(transform.position); 
            }
            //still going
            else
            {
                float time = (Time.time - (jumpStartTime+jumpElapsedTime));
                m_Transform.position = evaluate(startingPos, destination, time / duration);
                m_Transform.rotation *= Quaternion.Euler((Time.deltaTime / duration) * 360, 0, 0);
            }

        }

    }

    public Vector3 evaluate(Vector3 startPos, Vector3 endPos, float t)
    {
        Vector3 midPos = (endPos + startPos)/2;
        midPos.y = Vector3.Distance(startPos, endPos) * maxHeight;
        if (midPos.y < minimumHeight) midPos.y = minimumHeight;
        Vector3 ab = Vector3.Lerp(startPos, midPos, t);
        Vector3 bc = Vector3.Lerp(midPos, endPos, t);
        return Vector3.Lerp(ab, bc, t);
    }

    private void OnDrawGizmos()
    {
        if (startingPos == null || destination == null) return;
        for (int i = 0; i < 20; i++)
        {
            Gizmos.DrawWireSphere(evaluate(startingPos,destination, i / 20f), 0.1f);
        }
    }
}
