using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerUnit : StaticInstance<PlayerUnit>
{
    public Chopper chopper;
    [SerializeField] Animator playerAnimation;
    [SerializeField] GameObject BladeParent;
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
        UpdateBladeModel((int)blade.bladeModelIndex);
        chopper.SetBlade(blade);

    }
    public void UpdateBladeModel(int i)
    {
        Debug.Log((int)chopper.blade.bladeModelIndex);
        BladeParent.transform.GetChild((int)chopper.blade.bladeModelIndex).gameObject.SetActive(false);
        BladeParent.transform.GetChild(i).gameObject.SetActive(true);
    }
}
