using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DropDecoy : AbilityParent
{
    [SerializeField] private GameObject m_dummyPrefab;
    [SerializeField] private float m_invisibilityDuration = 1f;
    [Range(0f, 1f)]
    [SerializeField] private float m_spriteAlpha = 125f;
    [SerializeField] private float m_distToSpawnDecoy = 1f;
    private float m_currentTimer = 0f;
    private bool m_isInvisActive = false;
    private Dummy[] m_dummies;
    private Vector3 m_mousePos;

    private SpriteRenderer[] m_spriteRenderers;

    private void Awake()
    {
        m_spriteRenderers = GetComponentsInChildren<SpriteRenderer>();
    }

    protected override void Update()
    {
        base.Update();
        if (m_isInvisActive)
        {
            m_currentTimer -= Time.deltaTime;
            if (m_currentTimer <= 0)
            {
                m_isInvisActive = false;
                Color bufferColor;
                for (int i = 0; i < m_spriteRenderers.Length; i++)
                {
                    bufferColor = m_spriteRenderers[i].color;
                    bufferColor.a = 255;
                    m_spriteRenderers[i].color = bufferColor;
                }
                GameManager.Instance.m_player.m_isVisible = true;
            }
        }
    }

    private void Start()
    {
        InputManager.Instance.UpdateMousePos += SaveMousePos;
    }
    private void OnDestroy()
    {
        InputManager.Instance.UpdateMousePos -= SaveMousePos;
    }

    public override void InitAbility(int numCharges, int abilityIndex)
    {
        base.InitAbility(numCharges, abilityIndex);

        m_dummies = new Dummy[numCharges];
        Vector3 spawnPos = new Vector3(1000f, 1000f, 0f);
        for (int i = 0; i < numCharges; i++)
        {
            m_dummies[i] = Instantiate(m_dummyPrefab, spawnPos, Quaternion.identity).GetComponent<Dummy>();
            m_dummies[i].gameObject.SetActive(false);
        }
    }

    protected override void AbilityEffect()
    {
        Dummy dummy = m_dummies[m_numCharges - 1];
        dummy.gameObject.SetActive(true);
        dummy.transform.position = transform.position + transform.right * m_distToSpawnDecoy;
        dummy.transform.right = transform.right;
        dummy.StartWalking(m_mousePos);

        m_isInvisActive = true;
        m_currentTimer = m_invisibilityDuration;
        GameManager.Instance.m_player.m_isVisible = false;

        Color bufferColor;
        for (int i = 0; i < m_spriteRenderers.Length; i++)
        {
            bufferColor = m_spriteRenderers[i].color;
            bufferColor.a = m_spriteAlpha;
            m_spriteRenderers[i].color = bufferColor;
        }
    }
    private void SaveMousePos(Vector2 _mousePos)
    {
        m_mousePos = new Vector3(_mousePos.x, _mousePos.y, transform.position.z);
    }
}
