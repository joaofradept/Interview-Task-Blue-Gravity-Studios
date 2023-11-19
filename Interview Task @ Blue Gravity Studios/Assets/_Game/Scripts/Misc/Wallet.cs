using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Wallet : MonoBehaviour
{
    public Action<int> onMoneyChange;

    [SerializeField] int initialMoney;

    int currentMoney;

    int CurrentMoney
    {
        get => currentMoney;

        set
        {
            currentMoney = value;

            if (onMoneyChange != null)
                onMoneyChange(currentMoney);
        }
    }

    private void Start()
    {
        AddMoney(initialMoney);
    }

    public bool TryWidthdraw(int amount)
    {
        if (CurrentMoney >= amount)
        {
            CurrentMoney -= amount;

            return true;
        }

        return false;
    }

    public void AddMoney(int amount)
    {
        CurrentMoney += amount;
    }
}
