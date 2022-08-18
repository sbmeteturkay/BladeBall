using System;
using System.Collections.Generic;
using UnityEngine;
[CreateAssetMenu(fileName = "UpgradeMaterial", menuName = "Shopping/Upgrade")]
public class UpgradeMaterial:ScriptableObject
{
    public event Action<UpgradeMaterial> OnValueChange;
    public string upgradeName;
    [Header("Level")]
    public int level = 1;
    public int maxLevel = 20;
    public bool infinite = false;
    [Header("In Game Price")]
    public int price = 1;
    public float priceIncreaseRate;
    public CollectType moneyType=CollectType.wood;
    [Header("Increaser of system")]
    public float value = 1;
    public float valueIncreaseRate = 1;

    #region Value
    public void SetValue(int _value)
    {
        value = _value;
    }
    public void UpgradeValue()
    {
        value = valueIncreaseRate * value;
    }
    public void LimitedValueMultipler(MonoBehaviour mono,float multipler,float seconds)
    {
        float _value = value;
        value = value * multipler;
        Helpers.Wait(mono, seconds, () => { value = _value; });
        
    }
    #endregion
    #region Level
    public bool CanUpdateLevel()
    {
        if (!infinite && level >= maxLevel)
            return false;
        else
            return true;
    }
    public void UpdateLevel()
    {
        level++;
    }
    #endregion
    #region Price
    public void UpdatePrice()
    {
        price = price * price;
    }
    public float GetPrice()
    {
        return price;
    }
    #endregion

    public bool UpdateAction()
    {
        if (GameDataManager.CanSpendCoins(price, moneyType) && CanUpdateLevel())
        {
            GameDataManager.SpendCoins(price, moneyType);
            UpdatePrice();
            UpgradeValue();
            UpdateLevel();
            OnValueChange?.Invoke(this);
            return true;
        }
        else
            return false;

    }

}

