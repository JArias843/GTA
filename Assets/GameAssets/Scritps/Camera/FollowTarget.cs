using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowTarget : MonoBehaviour
{
    private Transform m_target;
    private float smoothTime = 0.3f;
    private Vector3 velocity = Vector3.zero;

    CameraShake m_cameraShake;

    void Start()
    {
        m_target = GameManager.Instance ?
        GameManager.Instance.m_player.transform : null;

        if(GetComponent<Camera>())
            GetComponent<Camera>().orthographicSize = 7;

        m_cameraShake = GetComponent<CameraShake>() ? 
        GetComponent<CameraShake>() : null;
    }
    private void FixedUpdate()
    {
        if (m_target && !m_cameraShake.IsExecuting)
        {
            transform.position = Vector3.SmoothDamp(transform.position,
            m_target.position + new Vector3(0, 0, -10), ref velocity, smoothTime);
        }
    }
}
