using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Pathfinding;
using UnityEngine.SceneManagement;

public enum PoliceState
{
    Zero,
    One,
    Two,
    RangeUp,
    SpeedUp,
    Three
}

public class PoliceManager : Utils.TemporalSingleton<PoliceManager>
{
    public GameObject prefabRef;

    public PoliceState m_policeState;
    public List<GameObject> m_list;
    public List<Transform> m_patrolPoints;
    private int index;
    private const int MAX_ENEMIES = 3;
    public Transform m_emergencyCall;
    private bool m_isGoingToCall;

    // Start is called before the first frame update
    void Start()
    {
        m_emergencyCall = Instantiate(m_emergencyCall.gameObject, new Vector3(10000, 10000, 10000), Quaternion.identity).transform;
        m_policeState = PoliceState.Zero;
        index = -1;

        for (int i = 0; i < MAX_ENEMIES; i++)
        {
            GameObject enemy = Instantiate(prefabRef);
            enemy.SetActive(false);
            m_list.Add(enemy);
        }
    }

    private void Update()
    {
        if (SceneManager.GetActiveScene().name != "Main_Menu" && 
        GameManager.Instance.LevelData.m_timer <= 25)
        {
            HandleSpeedUp();
        }

        if (Vector2.Distance(m_list[0].transform.position, m_emergencyCall.transform.position) <= 2)
        {
            if (m_isGoingToCall)
            {
                m_list[0].GetComponent<Police>().m_destinationSetter.target = null;
                m_list[0].GetComponent<Police>().Patrol();
                m_isGoingToCall = false;
            }
        }
    }

    public void HandleZero()
    {
        index = -1;
        for (int i = 0; i < MAX_ENEMIES; i++)
        {
            m_list[i].SetActive(false);
        }
    }
    
    public void HandleOne()
    {
        index = 0;
        m_list[index].SetActive(!m_list[index].activeInHierarchy);
        // TODO: Ajustar posicion fuera de la camara
        m_list[index].transform.position = new Vector3(-20, -20, 0);
    }
    
    public void HandleTwo()
    {
        index = 1;
        m_list[index].SetActive(!m_list[index].activeInHierarchy);
        // TODO: Ajustar posicion fuera de la camara
        m_list[index].transform.position = new Vector3(10, 5, 0);
    }

    public void HandleRangeUp()
    {
        for (int i = 0; i < m_list.Count; i++)
        {
            if (m_list[i].GetComponent<CircleCollider2D>())
            {
                // TODO: Ajustar rango
                m_list[i].GetComponent<CircleCollider2D>().radius = 6;
            }
        }
    }
    
    public void HandleSpeedUp()
    {
        for (int i = 0; i < m_list.Count; i++)
        {
            if (m_list[i].GetComponent<AIPath>())
            {
                AIPath path = m_list[i].GetComponent<AIPath>();
                path.maxSpeed = 5;
            }
        }
    }
    
    public void HandleThree()
    {
        index = 2;
        m_list[index].SetActive(!m_list[index].activeInHierarchy);
    }

    public void CallPolice()
    {
        m_isGoingToCall = true;
        m_emergencyCall.transform.position = GameManager.Instance.m_player.transform.position;

        AIDestinationSetter destination = m_list[0].GetComponent<AIDestinationSetter>();
        destination.target = m_emergencyCall;
    }

    public void UpdatePoliceState(PoliceState _state)
    {
        m_policeState = _state;

        switch (m_policeState)
        {
            case PoliceState.Zero:
                HandleZero();
                break;
            case PoliceState.One:
                HandleOne();
                break;
            case PoliceState.Two:
                HandleTwo();
                break;
            case PoliceState.RangeUp:
                HandleRangeUp();
                break;
            case PoliceState.SpeedUp:
                HandleSpeedUp();
                break;
            case PoliceState.Three:
                HandleThree();
                break;
            default:
                break;
        }
    }
}
