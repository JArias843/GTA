using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ThrowSmokeBomb : MonoBehaviour
{
    [SerializeField] private GameObject m_smokeBombPrefab;
    [SerializeField] private GameObject m_iconPrefab;
    [SerializeField] private float m_cooldown;
    private int m_abilityID;
    private float m_currentCooldown;
    private bool m_isOnCooldown = false;
    private int m_numSmokeBombs = 0;
    private SmokeBomb[] m_smokeBombs;

    public int NumSmokeBombs { get => m_numSmokeBombs; set => m_numSmokeBombs = value; }

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

    public void InitSmokeBombs(int numSmokeBombs)
    {
        m_smokeBombs = new SmokeBomb[numSmokeBombs];
        Vector3 spawnPos = new Vector3(1000f, 1000f, 0f);

        for (int i = 0; i < numSmokeBombs ; i++)
        {
            m_smokeBombs[i] = Instantiate(m_smokeBombPrefab, spawnPos, Quaternion.identity).GetComponent<SmokeBomb>();
        }
        m_abilityID = GameCC.Instance.InitAbility(m_iconPrefab, numSmokeBombs);
        m_numSmokeBombs = numSmokeBombs;
    }


    private void ThrowBomb()
    {
        if(!m_isOnCooldown && m_numSmokeBombs > 0)
        {
            m_currentCooldown = m_cooldown;
            m_isOnCooldown = true;
            m_smokeBombs[m_numSmokeBombs - 1].gameObject.SetActive(true);
            m_smokeBombs[m_numSmokeBombs - 1].Throw(transform.position);
            GameCC.Instance.ToggleRechargeBar(m_abilityID);
            GameCC.Instance.SetProgressBarFillAmount(m_abilityID, 0f);
            GameCC.Instance.ToggleSkillActiveFilter(m_abilityID);
            
            --m_numSmokeBombs;
            GameCC.Instance.SetNumCharges(m_numSmokeBombs);
        }
    }
}
