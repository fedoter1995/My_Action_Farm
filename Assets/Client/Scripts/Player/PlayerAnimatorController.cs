using UnityEngine;

public sealed class PlayerAnimatorController : AnimatorController, IPlayerAnimatorController
{
    [SerializeField] private AnimationEventsHandler _animHandler;
    [SerializeField] private ParticleSystem _dustParticles;
    [SerializeField] private ParticleSystem _slashEffect;
    private int IntMove = Animator.StringToHash("Move");
    private int IntAttack = Animator.StringToHash("Attack");

    private void Awake()
    {
        if (_animHandler == null)
            _animHandler = GetComponentInChildren<AnimationEventsHandler>();

        _animHandler.OnStepsEvevent += DustGeneration;
        _animHandler.OnSlashBeginEvent += SlashEffectPlay;

        animator = GetComponentInChildren<Animator>();
    }

    public void SetMoveFloatValue(float value)
    {
        SetHashValue(IntMove, value);
    }
    public void SetAttackTrigger()
    {
        SetTriggerValue(IntAttack);
    }

    public void DustGeneration()
    {
        _dustParticles.Play();
    }
    private void SlashEffectPlay()
    {
        _slashEffect.Play();
    }
 
}
