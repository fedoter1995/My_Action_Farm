using System;
using UnityEngine;

public abstract class PlayerController : ScriptableObject
{
    protected IPlayerInput input;
    protected IInputManager userInput;

    public abstract event Action OnAttackTriggerEvent;
    public IInputManager UserInput => userInput;
    public abstract void Move(float speed, Transform transform);
    public abstract void AttackTrigger();
    public abstract void InitController(IPlayerAnimatorController player);

    public void SetActive(bool activity)
    {
        userInput.SetActive(activity);
    }
}
