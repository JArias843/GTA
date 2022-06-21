using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ThrowSmokeBomb : MonoBehaviour
{
    [SerializeField] private int m_abilityID;
    [SerializeField] private SmokeBomb m_smokeBomb;
    [SerializeField] private float m_cooldown;
    private float m_currentCooldown;
    private bool m_isOnCooldown = false;

    // Start is called before the first frame update
    void Start()
    {
        InputManager.Instance.OnDefensiveSkillPressedEvent += ThrowBomb;
    }
    private void OnDestroy()
    {   
        InputManager.Instance.OnDefensiveSkillPressedEvent -= ThrowBomb;
    }
    private void Update()
    {
        if(m_isOnCooldown)
        {
            m_currentCooldown -= Time.deltaTime;
            GameCC.Instance.SetProgressBarFillAmount(m_abilityID, 1f - (m_currentCooldown / m_cooldown));
            if(m_currentCooldown < 0)
            {
                m_isOnCooldown = false;
                GameCC.Instance.ToggleRechargeBar(m_abilityID);
                GameCC.Instance.ToggleSkillActiveFilter(m_abilityID);
            }
        }
    }


    private void ThrowBomb()
    {
        if(!m_isOnCooldown)
        {
            m_currentCooldown = m_cooldown;
            m_isOnCooldown = true;
            m_smokeBomb.gameObject.SetActive(true);
            m_smokeBomb.Throw(transform.position);
            GameCC.Instance.ToggleRechargeBar(m_abilityID);
            GameCC.Instance.SetProgressBarFillAmount(m_abilityID, 0f);
            GameCC.Instance.ToggleSkillActiveFilter(m_abilityID);
        }
    }
}
