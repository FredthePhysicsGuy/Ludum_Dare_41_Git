using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class ActionBehavior : MonoBehaviour
{

    public GameObject soundObj;
    private Animator objAnim;


    void Start()
    {
        objAnim = gameObject.GetComponent<Animator>();
    }

    public void StartInteraction()
    {
        IEnumerator startWait = WaitForActivation(0.2f);
        StartCoroutine(startWait);
    }

    private IEnumerator WaitForActivation(float waitTime)
    {
        yield return new WaitForSecondsRealtime(waitTime);
        objAnim.SetTrigger("StartInteraction");
        soundObj.GetComponent<AudioSource>().Play();
    }
}

