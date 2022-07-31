using UnityEngine;
using UnityEngine.UI;

public class JoystickInterface : MonoBehaviour
{
    [SerializeField] private Button _attackButton;
    [SerializeField] private Joystick _joystick;

    public Button AttackButton => _attackButton;
    public Joystick Joystick => _joystick;
}
