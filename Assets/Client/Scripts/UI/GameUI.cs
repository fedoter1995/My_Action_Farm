using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameUI : MonoBehaviour
{
    [SerializeField] private UpMenu _upMenu;
    [SerializeField] private CoinsPool _coinsPool;
    public UpMenu UpMenu => _upMenu;
    public CoinsPool CoinsPool => _coinsPool;

    private void Start()
    {
        Initialize();
    }
    private void Initialize()
    {
        _upMenu.Initialize();
    }
}
