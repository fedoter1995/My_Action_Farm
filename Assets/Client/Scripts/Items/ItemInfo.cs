using System;
using UnityEngine;

public class ItemInfo : ScriptableObject
{

    [SerializeField] private string _id;
    [SerializeField] private string _title;
    [SerializeField] private int _price;

    public string ID  => _id;
    public string Title => _title;
    public int Price => _price;
}

