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
    public CameraShake m_cameraShake;

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
            if (collision.collider.GetComponentInParent<EnemyWallet>())
            {
                //Stun other
                m_cmpPlayer.CoinsStolen += 
                 collision.collider.GetComponentInParent<EnemyWallet>().Steal(m_amountToStealOnCollision);

                m_isHnRActive = false;
                m_cmpPlayer.CurrentSpeedMultiplier -= m_maxSpeedMultiplier;
                m_cmpPlayer.CurrentAccMultiplier -= m_accMultiplier;

                StartCoroutine(m_cameraShake.Shake(0.10f, 0.3f));
                MusicManager.Instance?.PlaySound("hit_and_run");
            }
        }
    }
}
