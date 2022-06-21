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
    public NoParam OnInteractPressedEvent;
    public NoParam OnInteractReleasedEvent;
    public NoParam OnDefensiveSkillPressedEvent;


    private void Awake()
    {
        if(m_controlsAsset == null)
        {
            m_controlsAsset = new Controls();
            m_controlsAsset.Enable();
            m_controlsAsset.Player.Interact.performed += OnInteractPressed;
            m_controlsAsset.Player.Interact.canceled += OnInteractReleased;
            m_controlsAsset.Player.DefensiveSkill.performed += OnDefensiveSkillPressed;
        }
    }
    private void OnEnable()
    {
        if (m_controlsAsset == null)
        {
            m_controlsAsset = new Controls();
        }
        m_controlsAsset.Enable();
        m_controlsAsset.Player.Interact.performed += OnInteractPressed;
        m_controlsAsset.Player.Interact.canceled += OnInteractReleased;
        m_controlsAsset.Player.DefensiveSkill.performed += OnDefensiveSkillPressed;
    }
    private void OnDisable()
    {
        if (m_controlsAsset != null)
        {
            m_controlsAsset.Disable();
            m_controlsAsset.Player.Interact.performed -= OnInteractPressed;
            m_controlsAsset.Player.Interact.canceled -= OnInteractReleased;
            m_controlsAsset.Player.DefensiveSkill.performed -= OnDefensiveSkillPressed;
        }
    }

    // Update is called once per frame
    void Update()
    {
        UpdateMousePos(m_controlsAsset.Player.Mouse.ReadValue<Vector2>());
        Move(m_controlsAsset.Player.Move.ReadValue<Vector2>());
    }

    private void OnInteractPressed(InputAction.CallbackContext ctx)
    {
        OnInteractPressedEvent();
    }
    private void OnInteractReleased(InputAction.CallbackContext ctx)
    {
        OnInteractReleasedEvent();
    }
    private void OnDefensiveSkillPressed(InputAction.CallbackContext ctx)
    {
        OnDefensiveSkillPressedEvent();
    }
}
