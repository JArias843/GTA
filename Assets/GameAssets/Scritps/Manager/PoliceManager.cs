using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Pathfinding;
using UnityEngine.SceneManagement;

public class PoliceManager : Utils.TemporalSingleton<PoliceManager>
{
    public GameObject prefabRef;

    public List<GameObject> m_list;
    public List<Transform> m_patrolPoints;
    private int index;
    private const int MAX_ENEMIES = 3;
    public Transform m_emergencyCall;
    private bool m_isGoingToCall;

    // Start is called before the first frame update
    void Start()
    {
        m_emergencyCall = Instantiate(m_emergencyCall.gameObject, 
        new Vector3(10000, 10000, 10000), Quaternion.identity).transform;
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
}
