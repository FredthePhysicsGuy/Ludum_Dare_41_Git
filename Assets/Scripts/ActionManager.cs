using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class ActionManager : SingletonManager<ActionManager>
{

    private bool axeHitOn;
    private bool eatOn;

	void Start () 
	{
        axeHitOn = true;
        eatOn = false;
	}

	void Update () 
	{
        if(!Head_Camera.Instance.pauseMovementandCursorLock)
        {
            if (Input.GetKeyDown(KeyCode.Mouse0))
            {
                HitWithAxe.Instance.AxeAttackObj();
            }
        }

	}
}
