using UnityEngine;

public class UpMenu : MonoBehaviour
{
    [SerializeField] private CounterCoinsUI _coins;
    [SerializeField] private BackpackUI _backpack;
    public CounterCoinsUI Coins => _coins;
    public BackpackUI Backpack => _backpack;

    public void Initialize()
    {
        _coins.Initialize();
        _backpack.Initialize();
    }
}
