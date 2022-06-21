using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class InputManager : PersistentSingleton<InputManager>
{
    //Controls
    private Controls m_controlsAsset;

    //Delegates 
    public delegate void ParamVector2D(Vector2 vec2);
    public ParamVector2D UpdateMousePos;
    public ParamVector2D Move;

    public delegate void NoParam();
    public NoParam OnInteractPressed;

    private void Awake()
    {
        if(m_controlsAsset == null)
        {
            m_controlsAsset = new Controls();
            m_controlsAsset.Enable();
            //m_controlsAsset.Player.Interact.performed += 
        }
    }
    private void OnEnable()
    {
        if (m_controlsAsset == null)
        {
            m_controlsAsset = new Controls();
        }
        m_controlsAsset.Enable();
    }
    private void OnDisable()
    {
        if (m_controlsAsset != null)
        {
            m_controlsAsset.Disable();
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        UpdateMousePos(m_controlsAsset.Player.Mouse.ReadValue<Vector2>());
        Move(m_controlsAsset.Player.Move.ReadValue<Vector2>());
    }
}
