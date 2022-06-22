using UnityEngine;
using System.Xml;

public class MapLoader : MonoBehaviour
{
    public static void LoadMap(int index)
    {
        XmlDocument xmlDoc = new XmlDocument();
        TextAsset asset = (TextAsset)Resources.Load("GameData");
        xmlDoc.LoadXml(asset.text);

        XmlNode root = xmlDoc.DocumentElement;
        XmlNodeList list = root.SelectNodes("Levels");
        XmlNodeList level = list[0].ChildNodes;

        if (index > level.Count)
            return;

        XmlNode node = level[index];

        /*Set data*/
        GameManager.Instance.m_levelData.m_score =
        int.Parse(node.SelectSingleNode("score").InnerText);

        GameManager.Instance.m_levelData.m_time =
        float.Parse(node.SelectSingleNode("time").InnerText);

        GameManager.Instance.m_levelData.m_levelID = 
        int.Parse(node.SelectSingleNode("scene").InnerText);
    }
}
