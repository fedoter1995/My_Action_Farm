using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class Bank
{
    public static int Coins = 0;
    public static event Action<int> OnCoinsChangeEvent;

    public static bool IsEnougthCoins(int value)
    {
        if (value > Coins)
            return false;

        return true;
    }
    public static void AddCoins(int value)
    {
        Coins += value;
        OnCoinsChangeEvent?.Invoke(Coins);
    }
    public static bool TryToSpendCoins(int value)
    {
        if(!IsEnougthCoins(value))
            return false;
                  
        SpendCoins(value);
        return true;     
    }
    private static void SpendCoins(int value)
    {
        if (IsEnougthCoins(value))
        {
            Coins -= value;
            OnCoinsChangeEvent?.Invoke(Coins);
        }
    }
}
