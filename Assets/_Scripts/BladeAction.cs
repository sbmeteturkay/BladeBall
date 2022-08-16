using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BladeAction : StaticInstance<BladeAction>
{
    [SerializeField] Animator bladeAnimation;
    Blade blade;
    BladeState state;
    void Start()
    {
        blade = PlayerUnit.Instance.chopper.blade;
        PlayerState.OnEventChange += PlayerState_OnEventChange;
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

    public void DoDamage(Tree tree)
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
                }
                break;
            default:
                break;
        }

    }
    private void OnTriggerStay(Collider other)
    {
        if(other.tag=="Tree" && PlayerState.Instance.State != BladeState.BladeOpen)
            PlayerState.Instance.ChangeState(BladeState.BladeOpen);
    }
    private void OnTriggerExit(Collider other)
    {
        if(other.tag == "Tree" && PlayerState.Instance.State == BladeState.BladeOpen)
            PlayerState.Instance.ChangeState(BladeState.BladeOff);
    }
}

public enum Blades
{
    grey,
    fire,
    old
}
enum Storage
{
    skull,
    baloon
}