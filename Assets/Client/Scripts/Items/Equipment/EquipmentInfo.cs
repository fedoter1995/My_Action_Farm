using UnityEngine;

public abstract class EquipmentInfo : ScriptableObject, IEquipmentStats
{
    [SerializeField] private string _id;
    [SerializeField] private Sprite _icon;
    [SerializeField] private string _title;
    [SerializeField] private int _price;
    public string ID => _id;
    public Sprite Icon => _icon;
    public string Title => _title;
    public int Price => _price;
}
