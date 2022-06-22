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
    private Vector2 m_currentVelocity = Vector2.zero;
    private Vector2 m_velocityDirection = Vector2.zero;
    private Vector3 mouseWorldPos;
    public bool m_isVisible;

    //References
    private Camera m_mainCamera;

    //Components
    private Transform m_transform;
    private Rigidbody2D m_cmpRB;

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
        mouseWorldPos = m_mainCamera.ScreenToWorldPoint(new Vector3(_mousePos.x, _mousePos.y, 0));
        mouseWorldPos.z = m_transform.position.z;
        
        m_transform.right = (mouseWorldPos - m_transform.position).normalized;
    }
    private void Move(Vector2 _input)
    {
        m_currentVelocity = m_cmpRB.velocity;
        m_velocityDirection = m_currentVelocity.normalized;
        if (_input.magnitude < 0.1f)
        {
            m_currentVelocity -= m_velocityDirection * m_breakAcceleration * Time.deltaTime;
        }
        else
        {
            _input.Normalize();
            m_currentVelocity += _input * m_acceleration * Time.deltaTime;
        }
        m_currentVelocity = Vector2.ClampMagnitude(m_currentVelocity, m_maxSpeed);
        m_cmpRB.velocity = m_currentVelocity;
    }
}
