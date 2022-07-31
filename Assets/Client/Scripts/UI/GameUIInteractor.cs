using UnityEngine;

public static class GameUIInteractor
{
    private static GameUI ui;

    private static Player playerInteractor;

    private static void Initialize()
    {
        LoadResources();
        GetInteractors();
    }
    public static void OnStart()
    {        
        InitCoinsCounterUI();
        InitBackpackUI();
        InitCoinsPool();
    }
    private static void LoadResources()
    {
        var prefab = Resources.Load("Interface/UIInterface") as GameObject;
        ui = Object.Instantiate(prefab).GetComponent<GameUI>();
    }
    private static void GetInteractors()
    {
        
    }

    // use after OnInitialize
    private static void InitCoinsCounterUI()
    {
        ui.UpMenu.Coins.ChangeCoins(Bank.Coins);
        Bank.OnCoinsChangeEvent += ui.UpMenu.Coins.ChangeCoins;
    }
    private static void InitBackpackUI()
    {
        var backpack = playerInteractor.Equipments.CurrentBackpack;
        ui.UpMenu.Backpack.ChangeBackpackValues(backpack.Capacity, backpack.ItemAmount);
        backpack.OnInventoryAddedEvent += ui.UpMenu.Backpack.ChangeBackpackValues;
        backpack.OnInventoryRemovedEvent += ui.UpMenu.Backpack.ChangeBackpackValues;
    }
    private static void InitCoinsPool()
    {
        var barns = GameObject.FindObjectsOfType<Barn>();
        foreach(Barn barn in barns)
        {
            barn.BarterEvent += ui.CoinsPool.GetCoin;
        }
    }

}
