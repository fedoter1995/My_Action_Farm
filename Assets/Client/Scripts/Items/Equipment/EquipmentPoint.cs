using System;
using UnityEngine;

public abstract class EquipmentPoint : MonoBehaviour
{
    private Equipment currentEquipment;
    private AnimationEventsHandler handler;

    public Equipment CurrentWeapon => currentEquipment;
    public AnimationEventsHandler Handler => handler;


    public void ChangeCurrentEquipment(Equipment newItem)
    {        
        currentEquipment = newItem;
    }

    protected void Init()
    {
        handler = GetComponentInParent<AnimationEventsHandler>();
        handler.TriggerAnimationEvent += AttackActivity;
    }

    private void AttackActivity()
    {
        currentEquipment.ChangeActivity();
    }
}

