using TMPro;
using UnityEngine;

public sealed class CounterCoinsUI : MonoBehaviour
{
    [SerializeField] private Animation _shakeAnimation;
    [SerializeField] public TextMeshProUGUI text;

    public void Initialize()
    {
        Bank.OnCoinsChangeEvent += ChangeCoins;
        ChangeCoins(Bank.Coins);
    }
    public void ChangeCoins(int coins)
    {
        _shakeAnimation.Stop();
        _shakeAnimation.Play();
        text.text = coins.ToString();
    }
}
