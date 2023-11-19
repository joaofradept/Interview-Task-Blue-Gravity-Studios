using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    [SerializeField] InGameUI inGameUI;

    public Wallet PlayerWallet { get; private set; }
    public PlayerBelongings Belongings { get; private set; }

    private void Awake()
    {
        PlayerWallet = GetComponent<Wallet>();
        Belongings = GetComponent<PlayerBelongings>();
    }

    private void OnEnable()
    {
        // UI shall be updated when
        // player's money changes to a different value
        PlayerWallet.onMoneyChange
            += inGameUI.UpdateCurrentMoney;
    }

    private void OnDisable()
    {
        PlayerWallet.onMoneyChange
            -= inGameUI.UpdateCurrentMoney;
    }
}
