using UnityEngine;
[CreateAssetMenu(menuName = "Stats/Crops")]
public class CropsInfo : ItemInfo
{

    [SerializeField] private int _growingTime = 1;
    [SerializeField] private int _healthPoints = 1;

    public int GrowingTime { get => _growingTime; }
    public int HealthPoints { get => _healthPoints; }
}
