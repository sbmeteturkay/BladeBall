using System;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName ="New Blade",menuName ="Chopper/Blade")]
public class Blade : ScriptableObject
{
    
    [Header("In Game Stats")]
    public float damage=1;
    public float speed=1;
    [Header("Object properitys")]
    public Blades bladeModelIndex;
    public AudioClip hitSound;
    [Header("Market")]
    public Sprite image;
    public int price;
    public bool isPurchased;

    public enum Blades
    {
        grey,
        hammer,
        trumpet,
        saw,
        fire,
    }
}
