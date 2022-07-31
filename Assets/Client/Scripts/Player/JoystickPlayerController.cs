using System;
using UnityEngine;
using UnityEngine.UI;
[CreateAssetMenu(menuName = "PlayerController/JoystickPlayerController")]
public class JoystickPlayerController : PlayerController
{
    [SerializeField] private JoystickInterface _joystickInterfacePrefab;

    private JoystickInterface joystickInterface;
    private Button attackButton;
    private Joystick joystick;

    public override event Action OnAttackTriggerEvent;

    public override void InitController(IPlayerAnimatorController anim)
    {
        joystickInterface = Instantiate(_joystickInterfacePrefab);
        joystick = joystickInterface.Joystick;
        attackButton = joystickInterface.AttackButton;
        userInput = new JoyStickInputManager(joystick);
        OnAttackTriggerEvent += anim.SetAttackTrigger;
        attackButton.onClick.AddListener(AttackTrigger);
    }
    public override void Move(float speed, Transform transform)
    {
        transform.Translate(userInput.MoveVector * Time.fixedDeltaTime * speed, Space.World);

        if (userInput.MoveVector != Vector3.zero)
        {
            transform.forward = userInput.MoveVector;
        }
    }
    public override void AttackTrigger()
    {
        OnAttackTriggerEvent?.Invoke();
    }
}
