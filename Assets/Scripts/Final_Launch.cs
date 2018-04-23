using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Final_Launch : SingletonManager<Final_Launch>
{
    public GameObject doorBlocker;
    public GameObject failScreen;
    public GameObject endScreen;

    [HideInInspector]
    public bool currentlyLaunching;

    private void Start()
    {
        currentlyLaunching = false;
    }

    private void FixedUpdate()
    {
        if(currentlyLaunching)
        {
            gameObject.GetComponent<CharacterController>().Move(Vector3.up * 3.0f * Time.fixedDeltaTime);
            Head_Camera.Instance.movingVec.y += 3.0f * Time.fixedDeltaTime;
        }
    }

    public void CheckForFinalLaunch()
    {
        GameObject[] listEngine = GameObject.FindGameObjectsWithTag("WorkingEngine");
        if (listEngine.Length >= 1)
        {
            LaunchingSpaceShip();
        }
        else
        {
            FailureScreen();
        }
    }

    private void LaunchingSpaceShip()
    {
        doorBlocker.SetActive(true);
        IEnumerator launchCo = LaunchingTimer();
        StartCoroutine(launchCo);
        gameObject.GetComponent<AudioSource>().Play();
    }

    private void FailureScreen()
    {
        failScreen.SetActive(true);
        Head_Camera.Instance.pauseMovementandCursorLock = true;
    }

    private void DisplayEndScreen()
    {
        endScreen.SetActive(true);
        Head_Camera.Instance.pauseMovementandCursorLock = true;
    }

    private IEnumerator LaunchingTimer()
    {
        currentlyLaunching = true;
        yield return new WaitForSecondsRealtime(30.0f);
        currentlyLaunching = false;

        DisplayEndScreen();
        gameObject.GetComponent<AudioSource>().Stop();
    }
}
