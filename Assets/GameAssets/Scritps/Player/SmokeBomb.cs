using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SmokeBomb : MonoBehaviour
{
    [SerializeField] private float m_smokeBombEffectDuration;
    private float m_currentDuration;
    bool m_isCausingEffect = false;
    
    private Transform m_transform;
    private ParticleSystem m_particles;

    private Player m_player;

    private void Awake()
    {
        m_transform = transform;
        m_particles = GetComponent<ParticleSystem>();
    }

    // Update is called once per frame
    void Update()
    {
        if(m_isCausingEffect)
        {
            m_currentDuration -= Time.deltaTime;
            if (m_currentDuration < 0)
            {
                m_isCausingEffect = false;
                if(m_player)
                {
                    //Set is not hidden 
                }
            }
        }
        else
        {
            if(!m_particles.isPlaying)
            {
                gameObject.SetActive(false);
            }
        }
        
    }

    public void Throw(Vector3 _position)
    {
        m_isCausingEffect = true;
        m_transform.position = _position;
        m_currentDuration = m_smokeBombEffectDuration;
        m_particles.Play();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        m_player = collision.GetComponent<Player>();
        if (m_player != null)
        {
            if(!m_isCausingEffect)
            { 
                //Set is hidden
            }
        }
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        Player bufferPlayer = collision.GetComponent<Player>();
        if (bufferPlayer != null && m_player != null && bufferPlayer == m_player)
        {
            //Set is not hidden
        }
    }
}