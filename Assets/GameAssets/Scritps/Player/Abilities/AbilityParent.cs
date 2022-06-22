using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class AbilityParent : MonoBehaviour
{
    [SerializeField] protected GameObject m_iconPrefab;
    protected int m_abilityID;

    [SerializeField] protected float m_cooldown;
    protected float m_currentCooldown;
    protected bool m_isOnCooldown = false;
    protected int m_numCharges;
    protected int m_abilityIndex;

    // Update is called once per frame
    protected virtual void Update()
    {
        if (m_isOnCooldown)
        {
            m_currentCooldown -= Time.deltaTime;
            GameCC.Instance.SetProgressBarFillAmount(m_abilityID, 1f - (m_currentCooldown / m_cooldown));
            if (m_currentCooldown <= 0)
            {
                m_isOnCooldown = false;
                GameCC.Instance.ToggleRechargeBar(m_abilityID);
                if(m_numCharges > 0)
                {
                    GameCC.Instance.ToggleSkillActiveFilter(m_abilityID);
                }
            }
        }
    }

    public virtual void InitAbility(int numCharges, int abilityIndex)
    {
        m_abilityID = GameCC.Instance.InitAbility(m_iconPrefab, numCharges);
        m_numCharges = numCharges;
        m_abilityIndex = abilityIndex;

        switch (abilityIndex)
        {
            case 0:
                {
                    InputManager.Instance.OnAbility1PressedEvent += UseAbility;
                    break;
                }
            case 1:
                {
                    InputManager.Instance.OnAbility2PressedEvent += UseAbility;
                    break;
                }
            case 2:
                {
                    InputManager.Instance.OnAbility3PressedEvent += UseAbility;
                    break;
                }
        }
    }
    protected void OnDestroy()
    {
        switch (m_abilityIndex)
        {
            case 0:
                {
                    InputManager.Instance.OnAbility1PressedEvent -= UseAbility;
                    break;
                }
            case 1:
                {
                    InputManager.Instance.OnAbility2PressedEvent -= UseAbility;
                    break;
                }
            case 2:
                {
                    InputManager.Instance.OnAbility3PressedEvent -= UseAbility;
                    break;
                }
        }
    }

    protected void UseAbility()
    {
        if (!m_isOnCooldown && m_numCharges > 0)
        {
            m_currentCooldown = m_cooldown;
            m_isOnCooldown = true;

            AbilityEffect();

            GameCC.Instance.ToggleRechargeBar(m_abilityID);
            GameCC.Instance.SetProgressBarFillAmount(m_abilityID, 0f);
            GameCC.Instance.ToggleSkillActiveFilter(m_abilityID);

            --m_numCharges;
            GameCC.Instance.SetNumCharges(m_numCharges);        
        }
    }
    protected abstract void AbilityEffect();
}
