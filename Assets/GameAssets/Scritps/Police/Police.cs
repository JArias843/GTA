using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Pathfinding;

public class Police : MonoBehaviour
{
    public AIDestinationSetter m_target;

    public void Awake()
    {
        m_target = GetComponent<AIDestinationSetter>();
        m_target.target = null;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.CompareTag("Player"))
        {
            m_target.target = collision.gameObject.transform;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        m_target.target = null;
    }
}
