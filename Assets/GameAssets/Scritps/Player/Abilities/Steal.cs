using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Steal : MonoBehaviour
{
    [SerializeField] private float m_timeToSteal = 0.5f;
    [SerializeField] private int  m_maxStealPerTick = 2;

    private float m_currentTimeToSteal = 0f;

    [SerializeField] private float m_stealDistance;
    [SerializeField] private LayerMask m_layer;
    private Transform m_transform;

    RaycastHit2D[] m_results;

    EnemyWallet m_enemyWallet = null;
    bool m_isStealing = false;

    private int m_coinsStolen = 0;

    private void Awake()
    {
        m_transform = transform;
        m_results = new RaycastHit2D[1];
        m_coinsStolen = 0;
    }
    // Start is called before the first frame update
    void Start()
    {
        InputManager.Instance.OnInteractPressedEvent += StartStealing;
        InputManager.Instance.OnInteractReleasedEvent += StopStealing;
        GameCC.Instance.WriteCoinsStolen(m_coinsStolen);
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
                    m_coinsStolen += m_enemyWallet.Steal(m_maxStealPerTick);
                    m_currentTimeToSteal = m_timeToSteal;
                    GameCC.Instance.WriteCoinsStolen(m_coinsStolen);
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
                if(m_enemyWallet.Player)
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
