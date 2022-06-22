using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HitAndRun : AbilityParent
{
    
    [SerializeField] private float m_effectDuration;
    [SerializeField] private float m_maxSpeedMultiplier = 1.5f;
    [SerializeField] private float m_accMultiplier = 1.5f;
    [SerializeField] private int m_amountToStealOnCollision;
    bool m_isHnRActive = false;
    private float m_currentTimer = 0f;

    private Player m_cmpPlayer;
    private void Awake()
    {
        m_cmpPlayer = GetComponent<Player>();
    }
    private void Start()
    {
        
    }
    protected override void Update()
    {
        base.Update();
        if(m_isHnRActive)
        {
            m_currentTimer -= Time.deltaTime;
            if(m_currentTimer <= 0)
            {
                m_isHnRActive = false;
                m_cmpPlayer.CurrentSpeedMultiplier -= m_maxSpeedMultiplier;
                m_cmpPlayer.CurrentAccMultiplier -= m_accMultiplier;
            }
        }
    }

    protected override void AbilityEffect()
    {
        m_isHnRActive = true;
        m_currentTimer = m_effectDuration;
        m_cmpPlayer.CurrentSpeedMultiplier += m_maxSpeedMultiplier;
        m_cmpPlayer.CurrentAccMultiplier += m_accMultiplier;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(m_isHnRActive)
        {
            Debug.Log(collision.collider.name);
            if (collision.collider.GetComponentInParent<EnemyWallet>())
            {
                Debug.Log("b");
                //Stun other
                m_cmpPlayer.CoinsStolen += 
                    collision.collider.GetComponentInParent<EnemyWallet>().Steal(m_amountToStealOnCollision);
                m_isHnRActive = false;
                m_cmpPlayer.CurrentSpeedMultiplier -= m_maxSpeedMultiplier;
                m_cmpPlayer.CurrentAccMultiplier -= m_accMultiplier;
            }
        }
    }
}