using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Pathfinding;

public class Dummy : MonoBehaviour
{
    [SerializeField] private float m_lifetime = 5f;
    private float m_currentLifetime;
    public AIDestinationSetter m_target;
    public Transform m_waypoint;

    // Start is called before the first frame update
    void Start()
    {
        
    }
    private void OnEnable()
    {
        m_target = GetComponent<AIDestinationSetter>();
        m_waypoint = Instantiate(m_waypoint.gameObject, new Vector3(10000, 10000, 10000), Quaternion.identity).transform;
    }

    // Update is called once per frame
    void Update()
    {
        m_currentLifetime -= Time.deltaTime;
        if (m_currentLifetime <= 0f)
        {
            Destroy(this.gameObject);
        }
    }

    public void StartWalking(Vector3 _target)
    {
        m_currentLifetime = m_lifetime;
        m_waypoint.transform.position = _target;
        m_target.target = m_waypoint;
    }
}
