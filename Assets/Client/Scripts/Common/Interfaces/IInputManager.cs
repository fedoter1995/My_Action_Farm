using UnityEngine;
public interface IInputManager 
{
    Vector3 MoveVector { get; }
    float MoveFloat { get; }

    void SetActive(bool activity);
}
