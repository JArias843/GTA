using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Pathfinding;

public enum PoliceState
{
    Zero,
    One,
    Two,
    RangeUp,
    SpeedUp,
    Three
}

public class PoliceManager : MonoBehaviour
{
    public GameObject prefabRef;

    public PoliceState m_policeState;
    public List<GameObject> m_list;
    private int index;
    private const int MAX_ENEMIES = 3;

    // Start is called before the first frame update
    void Start()
    {
        m_policeState = PoliceState.Zero;
        index = -1;

        for (int i = 0; i < MAX_ENEMIES; i++)
        {
            GameObject enemy = Instantiate(prefabRef);
            enemy.SetActive(false);
            m_list.Add(enemy);
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
    }
    
    public void HandleTwo()
    {
        index = 1;
        m_list[index].SetActive(!m_list[index].activeInHierarchy);
    }
    
    public void HandleRangeUp()
    {
        for (int i = 0; i < MAX_ENEMIES; i++)
        {
            if (m_list[i].GetComponent<CircleCollider2D>())
            {
                m_list[i].GetComponent<CircleCollider2D>().radius = 6;
            }
        }
    }
    
    public void HandleSpeedUp()
    {
        for (int i = 0; i < MAX_ENEMIES; i++)
        {
            if (m_list[i].GetComponent<AIPath>())
            {
                AIPath path = m_list[i].GetComponent<AIPath>();
                path.maxSpeed = 3;
            }
        }
    }
    
    public void HandleThree()
    {
        index = 2;
        m_list[index].SetActive(!m_list[index].activeInHierarchy);
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
