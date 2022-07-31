using System;
using UnityEngine;

public interface ITakeDamge
{
    event Action<int> TakeDamageEvent;
    int HealthPoints { get; }
    void TakeDamage(Transform sender, int damage);
}
