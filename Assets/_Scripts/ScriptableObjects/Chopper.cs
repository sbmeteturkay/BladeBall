using System;
using System.Collections.Generic;
using UnityEngine;
[CreateAssetMenu(fileName = "New Chopper", menuName = "Chopper/Chopper")]
public class Chopper : ScriptableObject
{
    public static event Action<Blade> OnBladeChange;
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
        scale = value;
        capacity =(int)scale* 10;
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
    public void Start()
    {
        SetMovementSpeed(speedData.value);
        SetEnergy(energyData.value);
        SetScale(scaleData.value);

    }
}
