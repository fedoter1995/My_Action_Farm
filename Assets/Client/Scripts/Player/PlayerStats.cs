using UnityEngine;

[CreateAssetMenu(menuName = "Stats/Player")]
[System.Serializable]
public class PlayerStats : ScriptableObject
{
    [SerializeField] private float _speed;
    public float Speed { get => _speed; }
}
