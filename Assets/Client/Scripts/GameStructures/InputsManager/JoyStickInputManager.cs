
using UnityEngine;

public class JoyStickInputManager : IInputManager
{
    private Joystick joyStick;
    private bool active = true;
    public JoyStickInputManager(Joystick joyStick)
    {
        this.joyStick = joyStick;
    }

    public Vector3 MoveVector
    {
        get
        {
            if (active)
                return new Vector3(joyStick.Horizontal, 0, joyStick.Vertical).normalized;
            else
                return Vector3.zero;
        }
    }

    public float MoveFloat
    {
        get
        {
            if (active)
                return Mathf.Abs(MoveVector.x) + Mathf.Abs(MoveVector.z);
            else 
                return 0;
        }
    }

    public void SetActive(bool activity)
    {
        active = activity;
    }
}
