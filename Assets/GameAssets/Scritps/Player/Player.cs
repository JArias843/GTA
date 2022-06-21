using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class Player : MonoBehaviour
{
    //Movement
    [SerializeField] private float m_maxSpeed = 10f;
    [SerializeField] private float m_acceleration = 2f;
    [SerializeField] private float m_breakAcceleration = 4f;
    Vector2 m_currentVelocity = Vector2.zero;
    Vector2 m_velocityDirection = Vector2.zero;
    


    //Components
    private Transform m_transform;
    private Rigidbody2D m_cmpRB;

    private void Awake()
    {
        m_transform = GetComponent<Transform>();
        m_cmpRB = GetComponent<Rigidbody2D>();
    }

    // Start is called before the first frame update
    void Start()
    {
        if (InputManager.Instance)
        {
            InputManager.Instance.Move += Move;
        }
    }
    private void OnDestroy()
    {
        if(InputManager.Instance)
        {
            InputManager.Instance.Move -= Move;
        }
    }
    // Update is called once per frame
    void Update()
    {
        
    }

    private void Move(Vector2 input)
    {
        m_currentVelocity = m_cmpRB.velocity;
        m_velocityDirection = m_currentVelocity.normalized;
        if (input.magnitude < 0.1f)
        {
            m_currentVelocity -= m_velocityDirection * m_breakAcceleration * Time.deltaTime;
        }
        else
        {
            input.Normalize();
            m_currentVelocity += input * m_acceleration * Time.deltaTime;
            Vector2.ClampMagnitude(m_currentVelocity, m_maxSpeed);
        }
        m_cmpRB.velocity = m_currentVelocity;
    }
}
