using System.Collections;
using UnityEngine;
using System.Xml;
using System.Xml.Serialization;

[System.Serializable] 
public class items
{
    [XmlAttribute("name")]
    public string name;

    [XmlElement("red")]
    public float red;

    [XmlElement("green")]
    public float green;

    [XmlElement("blue")]
    public float blue;
 
}
