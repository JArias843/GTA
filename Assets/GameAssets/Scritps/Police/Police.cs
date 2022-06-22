using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Pathfinding;

public class Police : MonoBehaviour
{
    public AIDestinationSetter m_target;
    public List<Vector2> m_patrolPoints;
    private bool m_isFollowing;

    public void Awake()
    {
        m_target = GetComponent<AIDestinationSetter>();
        m_target.target = null;
    }

    public void Start()
    {
        Patrol();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.CompareTag("Player"))
        {
            m_isFollowing = true;
            m_target.target = collision.gameObject.transform;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        m_isFollowing = false;
        m_target.target = null;
        Patrol();
    }

    private void Patrol()
    {
        if (!m_isFollowing)
        {
            int randomPatrolIndex = Random.Range(0, m_patrolPoints.Count);
            m_target.target.position = m_patrolPoints[randomPatrolIndex];
        }
    }
}
