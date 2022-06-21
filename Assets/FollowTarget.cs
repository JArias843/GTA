using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowTarget : MonoBehaviour
{
    private Transform m_target;
    public float smoothTime;
    private Vector3 velocity = Vector3.zero;

    void Start()
    {
        m_target = GameManager.Instance.m_player.transform;
    }
    private void FixedUpdate()
    {
        transform.position = Vector3.SmoothDamp(transform.position, 
        m_target.position + new Vector3(0, 0, -10), ref velocity, smoothTime);
    }
}
