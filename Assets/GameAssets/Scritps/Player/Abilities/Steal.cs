using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Steal : MonoBehaviour
{
    [SerializeField] private float m_timeToSteal = 0.5f;
    [SerializeField] private int  m_maxStealPerTick = 2;

    private float m_currentTimeToSteal = 0f;
    private int m_currentMoneyStolen = 0;

    [SerializeField] private float m_stealDistance;
    [SerializeField] private LayerMask m_layer;
    private Transform m_transform;

    RaycastHit2D[] m_results;

    EnemyWallet m_enemyWallet = null;
    bool m_isStealing = false;

    Player m_cmpPlayer;

    private void Awake()
    {
        m_transform = transform;
        m_results = new RaycastHit2D[1];
        m_cmpPlayer = GetComponent<Player>();
    }
    // Start is called before the first frame update
    void Start()
    {
        InputManager.Instance.OnInteractPressedEvent += StartStealing;
        InputManager.Instance.OnInteractReleasedEvent += StopStealing;
    }
    private void OnDestroy()
    {
        InputManager.Instance.OnInteractPressedEvent -= StartStealing;
        InputManager.Instance.OnInteractReleasedEvent -= StopStealing;
    }
    // Update is called once per frame
    void Update()
    {
        if(m_isStealing)
        {
            m_currentTimeToSteal -= Time.deltaTime;
            if(m_currentTimeToSteal < 0)
            {
                if (m_enemyWallet.Player)
                {
                    m_currentMoneyStolen = m_enemyWallet.Steal(m_maxStealPerTick);
                    m_cmpPlayer.CoinsStolen += m_currentMoneyStolen;
                    m_currentTimeToSteal = m_timeToSteal;

                    if(m_currentMoneyStolen != 0)
                        MusicManager.Instance?.PlaySound("steal_money");
                    else
                    {
                        MusicManager.Instance?.PlaySound("error");
                        PoliceManager.Instance?.CallPolice();
                    }
                }
                else 
                {
                    StopStealing();
                }
                
            }
        }
    }

    private void StartStealing()
    {
        if(Physics2D.RaycastNonAlloc(m_transform.position, m_transform.right, m_results, m_stealDistance, m_layer) != 0)
        {
            m_enemyWallet = m_results[0].collider.GetComponent<EnemyWallet>();

            if (m_enemyWallet)
            {
                if (m_enemyWallet.Player)
                {
                    m_isStealing = true;
                    m_enemyWallet.ToggleProgressBar(true);
                }
            }
        }
    }
    private void StopStealing()
    {
        if(m_enemyWallet)
        {
            m_enemyWallet.ToggleProgressBar(false);
        }
        m_enemyWallet = null;
        m_isStealing = false;
    }
}
