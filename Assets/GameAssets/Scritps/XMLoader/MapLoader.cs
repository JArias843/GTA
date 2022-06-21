using UnityEngine;
using System.Xml;

public class MapLoader : MonoBehaviour
{
    static void LoadMap(int index)
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
        Debug.Log(node.SelectSingleNode("min_score").InnerText);
        Debug.Log(node.SelectSingleNode("background").InnerText);
        Debug.Log(node.SelectSingleNode("time").InnerText);
    }
}
