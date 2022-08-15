using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerUnit : StaticInstance<PlayerUnit>
{
    public Chopper chopper;
    public int CheckCapacity()
    {
        return chopper.capacity - GameDataManager.GetWood();
    }
}
