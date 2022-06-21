using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameCC : Utils.TemporalSingleton<GameCC>
{
    [SerializeField] private AbilityIcon[] m_abilityProgressBar;

    public void SetProgressBarFillAmount(int _abilityID, float _percentage)
    {

    }
    public void ToggleSkillActiveFilter(int _abilityID)
    {

    }
    public void ToggleRechargeBar(int _abilityID)
    {

    }
}
