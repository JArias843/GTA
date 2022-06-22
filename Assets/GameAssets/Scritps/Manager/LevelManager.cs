using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelManager : PersistentSingleton<LevelManager>
{
    private int m_levelID;
    public int LevelID { get => m_levelID; set => m_levelID = value; }
}
