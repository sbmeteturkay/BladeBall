using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerUnit : StaticInstance<PlayerUnit>
{
    public Chopper chopper;
    [SerializeField] Animator playerAnimation;
    [SerializeField] GameObject BladeParent;
    [SerializeField] GameObject ScaleObject;
    Vector3 scale = new Vector3(1, 1, 1);
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
    public void SetScale()
    {
        scale.Set(chopper.scale, chopper.scale, chopper.scale);
        ScaleObject.transform.localScale = scale;
        dg_simpleCamFollow.scaleFactor.Set(0, scale.y, -scale.z);
    }
    public void UpdateBladeModel(int i)
    {
        Debug.Log((int)chopper.blade.bladeModelIndex);
        BladeParent.transform.GetChild((int)chopper.blade.bladeModelIndex).gameObject.SetActive(false);
        BladeParent.transform.GetChild(i).gameObject.SetActive(true);
    }
    private void Start()
    {
        chopper.OnValueChange += Chopper_OnValueChange;

        chopper.Start();
    }

    private void Chopper_OnValueChange(Chopper obj)
    {
        SetScale();
    }
}
