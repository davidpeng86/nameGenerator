using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Xml.Serialization;
using System.IO;

[System.Serializable] 
[XmlRoot("ItemCollection")]
public class ItemContainer
{
     [XmlArray("Items"),XmlArrayItem("Item")]

    
    public List<items> items = new List<items>();

    public static ItemContainer Load(string path) {
        TextAsset _xml = Resources.Load<TextAsset>(path);

        XmlSerializer serializer = new XmlSerializer(typeof(ItemContainer));

        StringReader reader = new StringReader(_xml.text);

        ItemContainer items = serializer.Deserialize(reader) as ItemContainer;
         
        reader.Close();

        return items;
    }

    public void Add(items item){
        items.Add(item);
    }
    public void SaveItems(){
        XmlSerializer serializer = new XmlSerializer(typeof(ItemContainer));
        FileStream stream = new FileStream(Application.dataPath + "/Resources/items.xml",FileMode.Create);
        serializer.Serialize(stream,this);
        stream.Close();
    }
}
