using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameCC : Utils.TemporalSingleton<GameCC>
{
    [SerializeField] private Text m_coinsText;
    [SerializeField] private Transform m_abilityBar;
    private List<AbilityIcon> m_abilities;

    public override void Awake()
    {
        base.Awake();
        m_abilities = new List<AbilityIcon>(0);
    }

    public void SetProgressBarFillAmount(int _abilityID, float _percentage)
    {
        m_abilities[_abilityID].UpdateFillAmount(_percentage);
    }
    public void ToggleSkillActiveFilter(int _abilityID)
    {
        m_abilities[_abilityID].ToggleActiveFilter();
    }
    public void ToggleRechargeBar(int _abilityID)
    {
        m_abilities[_abilityID].ToggleRechargeBar();
    }

    public void WriteCoinsStolen(int coins)
    {
        m_coinsText.text = coins.ToString();
    }
    
    public int InitAbility(GameObject iconPrefab, int numCharges)
    {
        m_abilities.Add(Instantiate(iconPrefab, m_abilityBar).GetComponent<AbilityIcon>());    
        return (m_abilities.Count - 1);
    }

    public void SetNumCharges(int numNumCharges)
    {

    }
}
