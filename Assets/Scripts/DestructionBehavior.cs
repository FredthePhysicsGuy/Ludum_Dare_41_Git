using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class DestructionBehavior : MonoBehaviour 
{

    public GameObject[] arrayOfReplacingObjs;
    public GameObject[] arrayOfParticles;
    public GameObject[] arrayOfsoundObjs;

    private float axeAnimationTime;

	void Start () 
	{
        axeAnimationTime = 0.2f;
    }

    public void ActivateDestruction()
    {
        IEnumerator startWait  = WaitForActivation(axeAnimationTime);
        StartCoroutine(startWait);
    }


    private IEnumerator WaitForActivation(float waitTime)
    {
        
        yield return new WaitForSecondsRealtime(waitTime);
        foreach (GameObject item in arrayOfReplacingObjs)
        {
            if(item != null)
            {
                item.SetActive(true);
            }   
        }
        foreach (GameObject partObj in arrayOfParticles)
        {
            if(partObj != null)
            {
                partObj.GetComponent<ParticleSystem>().Play();
            }
            
        }
        foreach (GameObject soundObj in arrayOfsoundObjs)
        {
            if(soundObj != null)
            {
                soundObj.GetComponent<AudioSource>().Play();
            }     
        }
        gameObject.layer = 8;
    }
}
