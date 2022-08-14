using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BladeAction : StaticInstance<BladeAction>
{
    [SerializeField] Animator bladeAnimation;
    [SerializeField] Blade blade;
    BladeState state;
    void Start()
    {
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
                bladeAnimation.SetBool("bladeOpen", false);
                break;
            case BladeState.BladeOpen:
                bladeAnimation.SetBool("bladeOpen", true);
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
        Debug.Log(tree.treeState);
        Debug.Log(tree.currentHealth);

    }
    private void OnTriggerStay(Collider other)
    {
        if(other.tag=="Tree")
            PlayerState.Instance.ChangeState(BladeState.BladeOpen);
    }
    private void OnTriggerExit(Collider other)
    {
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