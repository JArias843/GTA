using UnityEngine;
using System.Xml;

public class MapLoader : MonoBehaviour
{
    public static SData LoadMap(int index)
    {
        SData data = new SData();

        XmlDocument xmlDoc = new XmlDocument();
        TextAsset asset = (TextAsset)Resources.Load("data");
        xmlDoc.LoadXml(asset.text);

        XmlNode root = xmlDoc.DocumentElement;
        XmlNodeList list = root.SelectNodes("Levels");
        XmlNodeList level = list[0].ChildNodes;

        if (index > level.Count)
            return data;

        XmlNode node = level[index];

        /*Set data*/
        data.m_score =
        int.Parse(node.SelectSingleNode("score").InnerText);

        data.m_timer = 
        float.Parse(node.SelectSingleNode("timer").InnerText);

        data.m_levelID = 
        int.Parse(node.SelectSingleNode("scene").InnerText);
        
        data.m_numSmokeBombs = 
        int.Parse(node.SelectSingleNode("smoke_bomb").InnerText);
        
        data.m_numHnR= 
        int.Parse(node.SelectSingleNode("speed_up").InnerText);
        
        data.m_numDecoys= 
        int.Parse(node.SelectSingleNode("decoy").InnerText);


        return data;
    }
}
