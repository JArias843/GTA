using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ThrowSmokeBomb : AbilityParent
{
    [SerializeField] private GameObject m_smokeBombPrefab;
    private SmokeBomb[] m_smokeBombs;

    public override void InitAbility(int numCharges, int abilityIndex)
    {
        base.InitAbility(numCharges, abilityIndex);

        m_smokeBombs = new SmokeBomb[numCharges];
        Vector3 spawnPos = new Vector3(1000f, 1000f, 0f);
        for (int i = 0; i < numCharges; i++)
        {
            m_smokeBombs[i] = Instantiate(m_smokeBombPrefab, spawnPos, Quaternion.identity).GetComponent<SmokeBomb>();
            m_smokeBombs[i].gameObject.SetActive(false);
        }
    }

    protected override void AbilityEffect()
    {
        m_smokeBombs[m_numCharges - 1].gameObject.SetActive(true);
        m_smokeBombs[m_numCharges - 1].Throw(transform.position);
    }
}
