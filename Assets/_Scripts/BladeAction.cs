using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BladeAction : StaticInstance<BladeAction>
{
    [SerializeField] Animator bladeAnimation;
    Blade blade;
    BladeState state=BladeState.BladeOpen;
    void Start()
    {
        blade = PlayerUnit.Instance.chopper.blade;
        PlayerState.OnEventChange += PlayerState_OnEventChange;
        PlayerUnit.Instance.chopper.OnBladeChange += Chopper_OnBladeChange;
    }

    private void Chopper_OnBladeChange(Blade obj)
    {
        blade = obj;
    }

    private void PlayerState_OnEventChange(BladeState obj)
    {
        state = obj;
        switch (state)
        {
            case BladeState.Standing:
                break;
            case BladeState.BladeOff:
                bladeAnimation.SetBool("full", false);
                bladeAnimation.SetBool("bladeOpen", false);
                break;
            case BladeState.BladeOpen:
                bladeAnimation.SetBool("full", false);
                bladeAnimation.SetBool("bladeOpen", true);
                break;
            case BladeState.Full:
                bladeAnimation.SetBool("full", true);
                bladeAnimation.SetBool("bladeOpen", false);
                break;
            default:
                break;
        }
    }

    public void DoDamage(TreeUnit.TreeInstance tree)
    {
        switch (state)
        {
            case BladeState.Standing:
                break;
            case BladeState.BladeOff:
                break;
            case BladeState.BladeOpen:
                if (tree.currentHealth > 0)
                {
                    tree.currentHealth -= blade.damage;
                    SoundManager.Instance.Play(blade.hitSound, false,true);
                }
                break;
            default:
                break;
        }

    }
    /*private void OnTriggerStay(Collider other)
    {
        if(other.tag=="Tree" && PlayerState.Instance.State != BladeState.BladeOpen)
            PlayerState.Instance.ChangeState(BladeState.BladeOpen);
    }
    private void OnTriggerExit(Collider other)
    {
        if(other.tag == "Tree" && PlayerState.Instance.State == BladeState.BladeOpen)
            PlayerState.Instance.ChangeState(BladeState.BladeOff);
    }*/
}

enum Storage
{
    skull,
    baloon
}