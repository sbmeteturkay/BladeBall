using System;
using System.Collections.Generic;
using UnityEngine;
[CreateAssetMenu(fileName = "New Chopper", menuName = "Chopper/Chopper")]
public class Chopper : ScriptableObject
{
    public event Action<Blade> OnBladeChange;
    public event Action<Chopper> OnValueChange;
    public Blade blade;
    public float movementSpeed=1;
    public float scale=1;
    public float energy = 1;
    public float currentEnergy = 1;
    public int capacity = 10;

    [Header("Upgrade Values")]
    public UpgradeMaterial speedData;
    public UpgradeMaterial scaleData;
    public UpgradeMaterial energyData;
    public void SetMovementSpeed(float value)
    {
        movementSpeed = value;
    }
    public void SetScale(float value)
    {
        capacity = (int)value*2;
        scale = value * 2 / 10;
    }
    public void SetEnergy(float value)
    {
        energy = value;
        currentEnergy = value;
    }
    public void SetBlade(Blade _blade)
    {
        Debug.Log("blade change");
        blade = _blade;
        OnBladeChange?.Invoke(blade);
    }
    public void ReloadValues()
    {
        SetMovementSpeed(speedData.value);
        SetEnergy(energyData.value);
        SetScale(scaleData.value);
        OnValueChange?.Invoke(this);
    }
    public void Start()
    {
        ReloadValues();
        scaleData.OnValueChange += ScaleData_OnValueChange;
        speedData.OnValueChange += SpeedData_OnValueChange;
        energyData.OnValueChange += EnergyData_OnValueChange;
    }

    private void EnergyData_OnValueChange(UpgradeMaterial obj)
    {
        ReloadValues();
    }

    private void SpeedData_OnValueChange(UpgradeMaterial obj)
    {
        ReloadValues();
    }

    private void ScaleData_OnValueChange(UpgradeMaterial obj)
    {
        ReloadValues();
    }
}
