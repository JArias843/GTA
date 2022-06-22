using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowTarget : MonoBehaviour
{
    [SerializeField]
    Transform m_target;

    public float smoothTime;
    private Vector3 velocity = Vector3.zero;

    private void FixedUpdate()
    {
        transform.position = Vector3.SmoothDamp(transform.position, 
        m_target.position + new Vector3(0, 0, -10), ref velocity, smoothTime);
    }
}
