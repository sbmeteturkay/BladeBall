using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerState : StaticInstance<PlayerState>
{
    public static event Action<BladeState> OnEventChange;
    public BladeState State { get; private set; }

    void Start() => ChangeState(BladeState.BladeOpen);
    public void ChangeState(BladeState bladeState)
    {
        State = bladeState;
        
        // fast switch with enum sw(tab)(tab)enumVariableName(tab)(downArrow)

        switch (bladeState)
        {
            case BladeState.Standing:
                break;
            case BladeState.BladeOff:
                break;
            case BladeState.BladeOpen:
                break;
            case BladeState.Full:
                break;
            default:
                break;
        }
        OnEventChange?.Invoke(bladeState);
    }
    private void Update()
    {
        if (PlayerUnit.Instance.CheckCapacity() == 0)
        {
            if(State != BladeState.Full)
                ChangeState(BladeState.Full);
        }
        else {
            if(State!=BladeState.BladeOpen)
                ChangeState(BladeState.BladeOpen); 
        }
    }

}
[Serializable]
public enum BladeState
{
    Standing = 0,
    BladeOff=1,
    BladeOpen = 2,
    Full=3
}
