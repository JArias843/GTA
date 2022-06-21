using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AbilityIcon : MonoBehaviour
{
    [SerializeField] private GameObject m_rechargeBarGO;
    [SerializeField] private Image m_progressIMG;
    [SerializeField] private GameObject m_inactiveSkillFilterGO;

    void ToggleActiveFilter()
    {
        m_inactiveSkillFilterGO.SetActive(!m_inactiveSkillFilterGO.activeSelf);
    }
    void ToggleRechargeBar()
    {
        m_rechargeBarGO.SetActive(!m_rechargeBarGO.activeSelf);
    }
    void UpdateFillAmount(float _newFillamout)
    {
        m_progressIMG.fillAmount = _newFillamout;
    }
}
