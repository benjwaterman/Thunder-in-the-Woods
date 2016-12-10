using UnityEngine;
using System.Collections;
using LitJson;
using System.Collections.Generic;
using System.IO;

public class ItemDatabase : MonoBehaviour
{
    public static ItemDatabase Current;

    private List<Item> database = new List<Item>();

    void Start()
    {
        Current = this;
        database = JsonMapper.ToObject<List<Item>>(File.ReadAllText(Application.dataPath + "/StreamingAssets/Items.json"));
    }

    public Item FetchItemByID(int id)
    {
        for (int i = 0; i < database.Count; i++)
            if (database[i].ID == id)
                return database[i];
        return null;
    }
}

public class Item
{
    public int ID { get; set; }
    public string Title { get; set; }
    public string Type { get; set; }
    public int Value { get; set; }
    public int Damage { get; set; }
    public int Sap { get; set; }
    public string Description { get; set; }
    public bool Stackable { get; set; }
    public string Slug { get; set; }
    public string SpritePath { get; set; }

    public Sprite Sprite { get; set; }
    public GameObject ThisGameObject { get; set; }

    public Item()
    {
        this.ID = -1;
    }
}