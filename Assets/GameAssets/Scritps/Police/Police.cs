using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Pathfinding;

public class Police : MonoBehaviour
{
    [SerializeField] public AIDestinationSetter m_destinationSetter;
    public List<Transform> m_patrolPoints;
    private int m_currentIndex;
    private bool m_followPlayer;
    private bool m_followDummy;

    [SerializeField]
    float m_killRange;

    public void Awake()
    {
        m_destinationSetter = GetComponent<AIDestinationSetter>();
        m_destinationSetter.target = null;
    }

    public void Start()
    {
        m_patrolPoints = PoliceManager.Instance.m_patrolPoints;
        Patrol();
    }

    public void Update()
    {
        if (m_followPlayer && !GameManager.Instance.m_player.m_isVisible && !m_followDummy)
        {
            m_followPlayer = false;
            m_destinationSetter.target = null;
            Patrol();
        }

        if (Vector2.Distance(m_patrolPoints[m_currentIndex].position, transform.position) <= 0.5)
        {
            if (!m_followPlayer)
            {
                Patrol();
            }
        }

        if (m_followPlayer && 
            Vector3.Distance(transform.position, GameManager.Instance.m_player.transform.position) <= m_killRange &&
            !GameManager.Instance.IsFinish)
        {
            GetComponent<AudioSource>()?.Play();
            GameManager.Instance.UpdateGameState(GameState.Lose);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.GetComponent<Dummy>())
        {
            m_followPlayer = false;
            m_followDummy = true;
            m_destinationSetter.target = collision.gameObject.transform;
        }
        else if (collision.GetComponent<Player>() && GameManager.Instance.m_player.m_isVisible && m_destinationSetter != null)
        {
            m_followPlayer = true;
            m_followDummy = false;
            m_destinationSetter.target = collision.gameObject.transform;
        }
        else if (collision as CircleCollider2D && collision.GetComponent<Police>())
        {
            Patrol();
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.GetComponent<Dummy>() && m_destinationSetter.target == collision.gameObject.transform)
        {
            m_followDummy = false;
            m_destinationSetter.target = null;
            Patrol();
        }

        if (collision.GetComponent<Player>() && m_destinationSetter.target == collision.gameObject.transform)
        {
            m_followPlayer = false;
            m_destinationSetter.target = null;
            Patrol();
        }
    }

    public void Patrol()
    {
        if (!m_followPlayer && !m_followDummy)
        {
            int tempSeed = (int)System.DateTime.Now.Ticks;
            Random.InitState(tempSeed);
            m_currentIndex = Random.Range(0, m_patrolPoints.Count);
            m_destinationSetter.target = m_patrolPoints[m_currentIndex];
        }
    }
}
