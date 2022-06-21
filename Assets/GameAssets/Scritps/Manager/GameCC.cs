using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameCC : Utils.TemporalSingleton<GameCC>
{
    [SerializeField] private AbilityIcon[] m_abilityProgressBar;
    [SerializeField] private Text m_coinsText;

    public void SetProgressBarFillAmount(int _abilityID, float _percentage)
    {
        m_abilityProgressBar[_abilityID].UpdateFillAmount(_percentage);
    }
    public void ToggleSkillActiveFilter(int _abilityID)
    {
        m_abilityProgressBar[_abilityID].ToggleActiveFilter();
    }
    public void ToggleRechargeBar(int _abilityID)
    {
        m_abilityProgressBar[_abilityID].ToggleRechargeBar();
    }

    public void WriteCoinsStolen(int coins)
    {
        m_coinsText.text = coins.ToString();
    }
}
