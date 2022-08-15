using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[CreateAssetMenu(fileName = "New Chopper", menuName = "Chopper/Chopper")]
public class Chopper : ScriptableObject
{
    public Blade blade;
    public float movementSpeed=1;
    public float scale=1;
    public float energy = 1;
    public float currentEnergy = 1;
    public int capacity = 10;
    public void SetMovementSpeed(int value)
    {
        movementSpeed = value;
    }
    public void SetScale(int value)
    {
        scale = value;
        capacity =(int)scale* 10;
    }
    public void SetEnergy(int value)
    {
        currentEnergy = value;
    }
    public void SetBlade(Blade blade)
    {
        this.blade = blade;
    }
}
