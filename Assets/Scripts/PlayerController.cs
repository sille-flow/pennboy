using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [SerializeField] float speed = 0.02f;
    [SerializeField] float maxHeight = 2f;
    [SerializeField] float distance = 2.5f;
    public Transform m_Transform;

    float jumpStartTime = 0;
    float jumpElapsedTime = 0;
    bool isJumping = false;
    Vector3 startingPos = Vector3.zero;
    Vector3 destination = Vector3.zero;
    // Start is called before the first frame update
    void Start()
    {
        m_Transform = gameObject.GetComponent<Transform>();
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
                //add 2 directions later
                destination = m_Transform.position + new Vector3(-jumpElapsedTime, 0, 0) * distance;
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
                m_Transform.rotation = Quaternion.identity;
                destination = Vector3.zero;
                startingPos = Vector3.zero;
            }
            //still going
            else
            {
                Debug.Log("Jump Happening");
                float time = (Time.time - (jumpStartTime+jumpElapsedTime));
                m_Transform.position = evaluate(startingPos, destination, time);
                m_Transform.forward =
                    evaluate(startingPos, destination, time + 0.01f) * 0.01f* speed - transform.position;
            }
            
        }
       
    }

    public Vector3 evaluate(Vector3 startPos, Vector3 endPos, float t)
    {
        Vector3 midPos = new Vector3(
                startingPos.x + (endPos.x - startPos.x) / 2,
                Vector3.Distance(startPos, endPos)*maxHeight,
                endPos.z
            );
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
