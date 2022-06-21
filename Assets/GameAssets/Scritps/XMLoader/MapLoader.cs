using UnityEngine;
using UnityEngine.SceneManagement;
using System.Xml;

public class MapLoader
{
    public static void LoadMap(int index)
    {
        XmlDocument xmlDoc = new XmlDocument();
        TextAsset asset = (TextAsset)Resources.Load("LevelData");
        xmlDoc.LoadXml(asset.text);

        XmlNode root = xmlDoc.DocumentElement;
        XmlNodeList list = root.SelectNodes("Levels");
        XmlNodeList level = list[0].ChildNodes;

        if (index > level.Count)
            return;

        XmlNode node = level[index];

        GameManager.Instance.m_currentLevel.m_minScore =
        int.Parse(node.SelectSingleNode("min_score").InnerText);

        GameManager.Instance.m_currentLevel.m_time =
        float.Parse(node.SelectSingleNode("time").InnerText);

        SceneManager.LoadScene(int.Parse(node.SelectSingleNode("scene").InnerText));
    }
}
