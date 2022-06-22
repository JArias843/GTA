using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Pathfinding;

public class Police : MonoBehaviour
{
    public AIDestinationSetter m_target;
    public List<Transform> m_patrolPoints;
    private int m_currentIndex;
    private bool m_isFollowing;

    public void Awake()
    {
        m_target = GetComponent<AIDestinationSetter>();
        m_target.target = null;
    }

    public void Start()
    {
        m_patrolPoints = PoliceManager.Instance.m_patrolPoints;
        Patrol();
    }

    public void Update()
    {
        if (m_isFollowing && !GameManager.Instance.m_player.m_isVisible)
        {
            Debug.Log("NoVisible");
            m_isFollowing = false;
            m_target.target = null;
            Patrol();
        }

        if (Vector2.Distance(m_patrolPoints[m_currentIndex].position, transform.position) <= 0.5)
        {
            if (!m_isFollowing)
            {
                Patrol();
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.GetComponent<Dummy>())
        {
            m_isFollowing = true;
            m_target.target = collision.gameObject.transform;
        }
        else if(collision.GetComponent<Player>() && GameManager.Instance.m_player.m_isVisible && m_target != null)
        {
            m_isFollowing = true;
            m_target.target = collision.gameObject.transform;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.GetComponent<Dummy>() && m_target.GetComponent<Dummy>())
        {
            m_isFollowing = false;
            m_target.target = null;
            Patrol();
        }

        if (collision.GetComponent<Player>() && m_target.GetComponent<Player>())
        {
            m_isFollowing = false;
            m_target.target = null;
            Patrol();
        }
    }

    private void Patrol()
    {
        if (!m_isFollowing)
        {
            int tempSeed = (int)System.DateTime.Now.Ticks;
            Random.InitState(tempSeed);
            m_currentIndex = Random.Range(0, m_patrolPoints.Count);
            m_target.target = m_patrolPoints[m_currentIndex];
        }
    }
}
