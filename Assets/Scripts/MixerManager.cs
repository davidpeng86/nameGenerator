using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class MixerManager : MonoBehaviour
{
    public const string path = "items";
    public Text text;

    [SerializeField]
    private ItemContainer ic;

    int obj_count = 0;
    NameMaker makeName = new NameMaker();

    string input_name;
    public GameObject sphere;

    Color col = new Color(0,0,0,0);
    // Start is called before the first frame update
    void Start()
    {
        ic = ItemContainer.Load(path);
        makeName.train();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void btn01_click(){
        obj_count++;
        input_name += ic.items[0].name;
        col += new Color(ic.items[0].red, ic.items[0].green, ic.items[0].blue,1);
    }
    public void btn02_click(){
        obj_count++;
        input_name += ic.items[1].name;
        col += new Color(ic.items[1].red, ic.items[1].green, ic.items[1].blue,1);
    }
    public void btn03_click(){
        obj_count++;
        input_name += ic.items[2].name;
        col += new Color(ic.items[2].red, ic.items[2].green, ic.items[2].blue,1);
    }
    
    public void merge_click(){
        if(obj_count > 1){
            col /= obj_count;
            Renderer rend = sphere.GetComponent<Renderer> ();
            rend.material = new Material(Shader.Find("Specular"));
            rend.material.color = col;

            items new_item = new items();
            //new_item.name = gen_name();
            new_item.name = makeName.newString();
            new_item.red = col.r;
            new_item.green = col.g;
            new_item.blue = col.b;

            ic.Add(new_item);
            ic.SaveItems();
            text.text = new_item.name;
            print(new_item.name);
            obj_count=0;
        }
        else{
            print("You have to pick colors!!!");
        }
    }

    public string gen_name(){
        string newString = "";
        string vowels = "";
        
        for(int i = 0; i < input_name.Length; i++){
            //find out those vowels
            if("aeiou".Contains(input_name[i].ToString())){
                vowels += input_name[i];
            }

        }
        
        for (int i = 0; i <= Random.Range(4,9); i++)
        {
            int r = Random.Range(0, input_name.Length -1);
            int v = Random.Range(0, vowels.Length -1);
            // if two continuous letters aren't vowels
            // the next letter must be a vowel
            if(i > 1 && 
                "aeiou".Contains(newString[i-2].ToString()) == false &&
                "aeiou".Contains(newString[i-1].ToString()) == false )
            {
                newString += vowels[v];
            }
            else
            {
                newString += input_name[r];
            }
            input_name.Remove(r);
        }

        input_name = "";
        return newString;
    }
}
