using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class Player : MonoBehaviour
{
    //Controls
    [SerializeField] private InputActionMap m_controls;

    //Movement
    [SerializeField] private float m_maxSpeed = 10.f;
    [SerializeField] private float m_acceleration = 2.f;

    private void OnEnable()
    {

    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
