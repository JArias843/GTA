using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public struct LevelData
{
    public int m_minScore;
    public float m_time;
    public string m_levelName;
}

public class GameManager : PersistentSingleton<GameManager>
{
    public Player m_player;
    public LevelData m_currentLevel;

    // Start is called before the first frame update
    void Start()
    {
        MapLoader.LoadMap(0);
    }

    // Update is called once per frame
    void Update()
    {
                    
    }

}
