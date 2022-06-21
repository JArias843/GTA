using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Xml;

public class Loader : MonoBehaviour
{
    XmlDocument xmlDoc;
    XmlNode node;

    struct Level
    {
        public float currentLevel;
        public float maxMoney;
    }

    // Start is called before the first frame update
    void Start()
    {
        xmlDoc = new XmlDocument();
        TextAsset asset = (TextAsset)Resources.Load("example");
        xmlDoc.LoadXml(asset.text);

        node = xmlDoc.DocumentElement;
        Debug.Log(node.Name);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
