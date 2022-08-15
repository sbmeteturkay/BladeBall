using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName ="New Blade",menuName ="Chopper/Blade")]
public class Blade : ScriptableObject
{
    [Header("In Game Stats")]
    public float damage=1;
    public float speed=1;
    public Blades bladeModelIndex;

    [Header("Market")]
    public int price;
    public bool isPurchased;
}
