using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class EnemyWallet : MonoBehaviour
{
    [SerializeField] private GameObject m_progressBarGO;
    [SerializeField] private Image m_progressBar;
    [SerializeField] private int m_walletValue = 10;
    private int m_currentWalletValue = 10;
    Player m_player = null;

    public Player Player { get => m_player; set => m_player = value; }

    // Start is called before the first frame update
    void Start()
    {
        ResetWallet();
    }

    private void ResetWallet()
    {
        m_currentWalletValue = m_walletValue;
        m_progressBar.fillAmount = 0f;
    }

    public void ToggleProgressBar(bool _setActive)
    {
        m_progressBarGO.SetActive(_setActive);
    }

    public int Steal(int amount)
    {
        m_currentWalletValue -= amount;
        if(m_currentWalletValue < 0)
        {
            amount += m_currentWalletValue;
            //SCREAM
            m_currentWalletValue = 0;
        }
        m_progressBar.fillAmount = 1f - ((float)m_currentWalletValue / (float)m_walletValue);
        return amount;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(m_player == null)
        {
            m_player = collision.GetComponent<Player>();
        }
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        if(m_player != null)
        {
            if(collision.GetComponent<Player>() != null)
            {
                m_player = null;
            }
        }
    }
}
