using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerUnit : StaticInstance<PlayerUnit>
{
    public Chopper chopper;
    [SerializeField] Animator playerAnimation;
    public int CheckCapacity()
    {
        return chopper.capacity - GameDataManager.GetWood();
    }
    public void UpdateCapacityAnimation()
    {
        playerAnimation.SetLayerWeight(1, GameDataManager.WoodFillAmount());
    }
    public void SetChopperBlade(Blade blade)
    {
        chopper.blade = blade;
        chopper.SetBlade(blade);
    }
}
