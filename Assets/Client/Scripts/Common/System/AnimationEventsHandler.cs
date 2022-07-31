using UnityEngine;
using System;

public class AnimationEventsHandler : MonoBehaviour
{
    public event Action TriggerAnimationEvent;
    public event Action OnStepsEvevent;
    public event Action OnSlashBeginEvent;

    public void SlashEffect()
    {
        OnSlashBeginEvent.Invoke();
    }
    public void BeginSlash()
    {
        TriggerAnimationEvent?.Invoke();
    }
    public void EndSlash()
    {
        TriggerAnimationEvent?.Invoke();
    }
    public void OnStep()
    {
        OnStepsEvevent?.Invoke();
    }
}
