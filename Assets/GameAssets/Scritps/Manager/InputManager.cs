using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
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
    public NoParam OnAbility1PressedEvent;
    public NoParam OnAbility2PressedEvent;
    public NoParam OnAbility3PressedEvent;
    public NoParam OnEscapePressedEvent;

    bool m_isRegistered = false;

    public override void Awake()
    {
        base.Awake();
        if (m_controlsAsset == null)
        {
            m_controlsAsset = new Controls();
        }
        m_controlsAsset.Enable();
        if (!m_isRegistered)
        {
            m_isRegistered = true;
            m_controlsAsset.Player.Interact.performed += OnInteractPressed;
            m_controlsAsset.Player.Interact.canceled += OnInteractReleased;
            m_controlsAsset.Player.DefensiveSkill.performed += OnDefensiveSkillPressed;
            m_controlsAsset.Player.Ability1.performed += OnOnAbility1Pressed;
            m_controlsAsset.Player.Ability2.performed += OnOnAbility2Pressed;
            m_controlsAsset.Player.Ability3.performed += OnOnAbility3Pressed;
            m_controlsAsset.Player.Escape.performed += OnEscapePressed;
        }
    }
    private void OnEnable()
    {
        if (m_controlsAsset == null)
        {
            m_controlsAsset = new Controls();
        }
        m_controlsAsset.Enable();
        if (!m_isRegistered)
        {
            m_isRegistered = true;
            m_controlsAsset.Player.Interact.performed += OnInteractPressed;
            m_controlsAsset.Player.Interact.canceled += OnInteractReleased;
            m_controlsAsset.Player.DefensiveSkill.performed += OnDefensiveSkillPressed;
            m_controlsAsset.Player.Ability1.performed += OnOnAbility1Pressed;
            m_controlsAsset.Player.Ability2.performed += OnOnAbility2Pressed;
            m_controlsAsset.Player.Ability3.performed += OnOnAbility3Pressed;
            m_controlsAsset.Player.Escape.performed += OnEscapePressed;
        }
    }
    private void OnDisable()
    {
        if (m_controlsAsset != null)
        {
            m_controlsAsset.Disable();
            if (m_isRegistered)
            {
                m_isRegistered = false;
                m_controlsAsset.Player.Interact.performed -= OnInteractPressed;
                m_controlsAsset.Player.Interact.canceled -= OnInteractReleased;
                m_controlsAsset.Player.DefensiveSkill.performed -= OnDefensiveSkillPressed;
                m_controlsAsset.Player.Ability1.performed -= OnOnAbility1Pressed;
                m_controlsAsset.Player.Ability2.performed -= OnOnAbility2Pressed;
                m_controlsAsset.Player.Ability3.performed -= OnOnAbility3Pressed;
                m_controlsAsset.Player.Escape.performed -= OnEscapePressed;
            }
        }
    }
    private void OnDestroy()
    {
        if (m_controlsAsset != null)
        {
            m_controlsAsset.Disable();
            if (m_isRegistered)
            {
                m_isRegistered = false;
                m_controlsAsset.Player.Interact.performed -= OnInteractPressed;
                m_controlsAsset.Player.Interact.canceled -= OnInteractReleased;
                m_controlsAsset.Player.DefensiveSkill.performed -= OnDefensiveSkillPressed;
                m_controlsAsset.Player.Ability1.performed -= OnOnAbility1Pressed;
                m_controlsAsset.Player.Ability2.performed -= OnOnAbility2Pressed;
                m_controlsAsset.Player.Ability3.performed -= OnOnAbility3Pressed;
                m_controlsAsset.Player.Escape.performed -= OnEscapePressed;
            }
        }
    }

    // Update is called once per frame
    void Update()
    {
        if(Time.timeScale == 1)
        {
            Vector3 worldPos = Camera.main.ScreenToWorldPoint(m_controlsAsset.Player.Mouse.ReadValue<Vector2>());
            UpdateMousePos?.Invoke(new Vector2(worldPos.x, worldPos.y));
        
            Move?.Invoke(m_controlsAsset.Player.Move.ReadValue<Vector2>());
        }
    }

    private void OnInteractPressed(InputAction.CallbackContext ctx)
    {
        OnInteractPressedEvent?.Invoke();
    }
    private void OnInteractReleased(InputAction.CallbackContext ctx)
    {
        OnInteractReleasedEvent?.Invoke();
    }
    private void OnDefensiveSkillPressed(InputAction.CallbackContext ctx)
    {
        OnDefensiveSkillPressedEvent?.Invoke();
    }

    private void OnOnAbility1Pressed(InputAction.CallbackContext ctx)
    {
        OnAbility1PressedEvent?.Invoke();
    }
    private void OnOnAbility2Pressed(InputAction.CallbackContext ctx)
    {
        OnAbility2PressedEvent?.Invoke();
    }
    private void OnOnAbility3Pressed(InputAction.CallbackContext ctx)
    {
        OnAbility3PressedEvent?.Invoke();
    }
    private void OnEscapePressed(InputAction.CallbackContext ctx)
    {
        OnEscapePressedEvent?.Invoke();
    }

}
