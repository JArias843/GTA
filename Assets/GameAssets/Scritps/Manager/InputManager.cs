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


    private void Awake()
    {
        if(m_controlsAsset == null)
        {
            m_controlsAsset = new Controls();
            m_controlsAsset.Enable();
            m_controlsAsset.Player.Interact.performed += OnInteractPressed;
            m_controlsAsset.Player.Interact.canceled += OnInteractReleased;
            m_controlsAsset.Player.DefensiveSkill.performed += OnDefensiveSkillPressed;
            m_controlsAsset.Player.Ability1.performed += OnOnAbility1Pressed;
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
        m_controlsAsset.Player.Ability1.performed += OnOnAbility1Pressed;
    }
    private void OnDisable()
    {
        if (m_controlsAsset != null)
        {
            m_controlsAsset.Disable();
            m_controlsAsset.Player.Interact.performed -= OnInteractPressed;
            m_controlsAsset.Player.Interact.canceled -= OnInteractReleased;
            m_controlsAsset.Player.DefensiveSkill.performed -= OnDefensiveSkillPressed;
            m_controlsAsset.Player.Ability1.performed -= OnOnAbility1Pressed;
        }
    }

    // Update is called once per frame
    void Update()
    {
        UpdateMousePos?.Invoke(m_controlsAsset.Player.Mouse.ReadValue<Vector2>());
        Move?.Invoke(m_controlsAsset.Player.Move.ReadValue<Vector2>());
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

}
