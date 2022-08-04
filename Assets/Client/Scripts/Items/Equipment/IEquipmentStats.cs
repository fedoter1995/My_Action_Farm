using UnityEngine;
public interface IEquipmentStats
{
    string ID { get; }
    Sprite Icon { get; }
    string Title { get; }
    int Price { get; }
}
