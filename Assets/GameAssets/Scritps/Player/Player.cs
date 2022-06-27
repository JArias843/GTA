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
    private float m_currentSpeedMultiplier = 1f;
    private float m_currentAccMultiplier = 1f;
    private Vector2 m_currentVelocity = Vector2.zero;
    private Vector2 m_velocityDirection = Vector2.zero;
    private Vector3 mouseWorldPos;
    public bool m_isVisible;

    private int m_coinsStolen = 0;

    //References
    private Camera m_mainCamera;

    //Components
    private Transform m_transform;
    private Rigidbody2D m_cmpRB;

    public float CurrentSpeedMultiplier { 
        get => m_currentSpeedMultiplier; 
        set
        { 
            if(value < 0f)
            {
                value = 0f;
            }
            m_currentSpeedMultiplier = value; 
        }
    }
    public float CurrentAccMultiplier { 
        get => m_currentAccMultiplier; 
        set 
        {
            if (value < 0f)
            {
                value = 0f;
            }
            m_currentAccMultiplier = value;
        }  
    }
    public int CoinsStolen { get => m_coinsStolen;
        set
        {
            if(value < 0)
            {
                value = 0;
            }

            GameCC.Instance.WriteCoinsStolen(value);
            m_coinsStolen = value;
        }
    }

    private void Awake()
    {
        m_transform = GetComponent<Transform>();
        m_cmpRB = GetComponent<Rigidbody2D>();
        m_isVisible = true;
    }

    // Start is called before the first frame update
    void Start()
    {
        if (InputManager.Instance)
        {
            InputManager.Instance.Move += Move;
            InputManager.Instance.UpdateMousePos += FaceToMouse;
        }

        m_mainCamera = Camera.main;
        m_coinsStolen = 0;
        GameCC.Instance.WriteCoinsStolen(m_coinsStolen);
    }
    private void OnDestroy()
    {
        if(InputManager.Instance)
        {
            InputManager.Instance.Move -= Move;
            InputManager.Instance.UpdateMousePos -= FaceToMouse;
        }
    }
    // Update is called once per frame
    void Update()
    {
        
    }

    private void FaceToMouse(Vector2 _mousePos)
    {
        mouseWorldPos = new Vector3(_mousePos.x, _mousePos.y, m_transform.position.z);
        //mouseWorldPos = m_mainCamera.ScreenToWorldPoint(new Vector3(_mousePos.x, _mousePos.y, 0));
        //mouseWorldPos.z = m_transform.position.z;

        m_transform.right = (mouseWorldPos - m_transform.position).normalized;
    }
    private void Move(Vector2 _input)
    {
        m_currentVelocity = m_cmpRB.velocity;
        m_velocityDirection = m_currentVelocity.normalized;
        if (_input.magnitude < 0.1f)
        {
            m_currentVelocity -= m_velocityDirection * m_breakAcceleration * m_currentAccMultiplier * Time.deltaTime;
        }
        else
        {
            _input.Normalize();
            m_currentVelocity += _input * m_acceleration * m_currentAccMultiplier * Time.deltaTime;
        }
        m_currentVelocity = Vector2.ClampMagnitude(m_currentVelocity, m_maxSpeed * m_currentSpeedMultiplier);
        m_cmpRB.velocity = m_currentVelocity;
    }
}
