using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class InGameUI : MonoBehaviour
{
    [SerializeField] TMP_Text moneyValueText;

    public void UpdateCurrentMoney(int value)
    {
        moneyValueText.text = value.ToString();
    }
}
