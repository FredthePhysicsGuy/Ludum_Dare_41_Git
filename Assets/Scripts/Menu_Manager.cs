using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class Menu_Manager : SingletonManager<Menu_Manager>
{

    public GameObject cursorImage;

    private bool escapeMode;

    override public void Awake()
    {
        base.Awake();
        escapeMode = false;
    }

    void Start () 
	{
		
	}

	void Update () 
	{
		if(Input.GetKeyDown(KeyCode.Escape))
        {
            if(!Final_Launch.Instance.currentlyLaunching)
            {
                if (escapeMode)
                {
                    escapeMode = false;
                    Head_Camera.Instance.pauseMovementandCursorLock = false;
                    cursorImage.SetActive(true);
                }
                else
                {
                    escapeMode = true;
                    Head_Camera.Instance.pauseMovementandCursorLock = true;
                    cursorImage.SetActive(false);
                }
            }
            else
            {
                if (escapeMode)
                {
                    escapeMode = false;
                    Cursor.lockState = CursorLockMode.Locked;
                    cursorImage.SetActive(true);
                }
                else
                {
                    escapeMode = true;
                    Cursor.lockState = CursorLockMode.None;
                    Cursor.visible = true;
                    cursorImage.SetActive(false);
                }
            }


        }
	}
}
