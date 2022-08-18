using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
[CreateAssetMenu(fileName = "UpgradeMaterial", menuName = "Shopping/Upgrade")]
public class UpgradeMaterial:ScriptableObject
{
    [Header("Constant Data")]
    public int startPrice = 1;
    public float startValue = 1;    public event Action<UpgradeMaterial> OnValueChange;
    [Space(20)]
    public string upgradeName;
    [Header("Level")]
    public int level = 1;
    public int maxLevel = 20;
    public bool infinite = false;
    [Header("In Game Price")]
    public int price = 1;
    public float priceIncreaseRate=1;
    public CollectType moneyType=CollectType.wood;
    [Header("Increaser of system")]
    public float value = 1;
    public float valueIncreaseRate = 1;
    [Space(20)]
    public Sprite referenceImage;
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

    public void SetImage(Sprite _image)
    {
        referenceImage = _image;
    }
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

