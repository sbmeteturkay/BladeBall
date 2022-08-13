using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerState : StaticInstance<PlayerState>
{
    public static event Action<BladeState> OnEventChange;
    public BladeState State { get; private set; }

    void Start() => ChangeState(BladeState.Standing);
    public void ChangeState(BladeState bladeState)
    {
        State = bladeState;
        // fast switch with enum sw(tab)(tab)enumVariableName(tab)(downArrow)
        switch (bladeState)
        {
            case BladeState.Standing:
                break;
            case BladeState.BladeOpen:
                break;
            default:
                break;
        }
        OnEventChange?.Invoke(bladeState);
    }

}
[Serializable]
public enum BladeState
{
    Standing = 0,
    BladeOff=1,
    BladeOpen = 2,
}
