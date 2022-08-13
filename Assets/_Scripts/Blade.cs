using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName ="New Blade",menuName ="Chopper/Blade")]
public class Blade : ScriptableObject
{
    public float damage=1;
    public float speed=1;
    public float energy=10;
    public Blades bladeModelIndex;
}
