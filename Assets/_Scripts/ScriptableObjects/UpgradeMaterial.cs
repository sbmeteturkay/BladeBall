using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
[CreateAssetMenu(fileName = "UpgradeMaterial", menuName = "Shopping/Upgrade")]
public class UpgradeMaterial:ScriptableObject
{
    [Header("Constant Data")]
    public int startPrice = 1;
    public float startValue = 1;
    public event Action<UpgradeMaterial> OnValueChange;
    [Space(20)]
    public string upgradeName;
    [Header("Level")]
    public int level = 1;
    public int maxLevel = 20;
    public bool infinite = false;
    [Header("In Game Price")]
    public int price = 1;
    public int priceIncreaseRate=1;
    public CollectType moneyType=CollectType.wood;
    [Header("Increase value system")]
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
        value = value + valueIncreaseRate * level;
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
        price = price +priceIncreaseRate*level;
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
            SavePlayerPref();
            OnValueChange?.Invoke(this);
            return true;
        }
        else
            return false;

    }
    public void GetPlayerPref()
    {
        if (PlayerPrefs.HasKey(upgradeName + "price"))
        {
            price = PlayerPrefs.GetInt(upgradeName + "price");
            level = PlayerPrefs.GetInt(upgradeName + "level");
            value = PlayerPrefs.GetFloat(upgradeName + "value");
            Debug.Log("scriptable awake");
            OnValueChange?.Invoke(this);
        }
        else
        {
            level = 1;
            price = startPrice;
            value = startValue;
            SavePlayerPref();
        }
    }
    void SavePlayerPref()
    {
        PlayerPrefs.SetInt(upgradeName + "level", level);
        PlayerPrefs.SetInt(upgradeName + "price", price);
        PlayerPrefs.SetFloat(upgradeName + "value", value);
        PlayerPrefs.Save();
    }
}

