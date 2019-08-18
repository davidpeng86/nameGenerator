using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemLoader : MonoBehaviour
{
    public const string path = "items";
    // Start is called before the first frame update
    void Start()
    {
        ItemContainer ic = ItemContainer.Load(path);

        foreach (items item in ic.items) {
            print(item.name);
        }
    }


}
