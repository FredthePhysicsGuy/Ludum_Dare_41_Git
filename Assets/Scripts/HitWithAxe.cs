using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class HitWithAxe : SingletonManager<HitWithAxe>
{
    private float axeRange;
    private Animator axeAnim;

	void Start () 
	{
        axeRange = 5.0f;
        axeAnim = gameObject.GetComponent<Animator>();
    }

    public void AxeAttackObj()
    {
        axeAnim.SetTrigger("StartAttack");

        RaycastHit hit;
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        if (Physics.Raycast(ray, out hit, axeRange))
        {
            if(hit.transform != null)
            {
                gameObject.GetComponent<AudioSource>().Play();
                if (hit.transform.gameObject.layer != 8)
                {
                    if (hit.transform.gameObject.CompareTag("Breakable"))
                    {
                        hit.transform.gameObject.GetComponent<DestructionBehavior>().ActivateDestruction();
                    }
                    if (hit.transform.gameObject.CompareTag("Interactable"))
                    {
                        hit.transform.gameObject.GetComponent<ActionBehavior>().StartInteraction();
                    }
                    if (hit.transform.gameObject.CompareTag("Final_Button"))
                    {
                        hit.transform.gameObject.GetComponent<DestructionBehavior>().ActivateDestruction();
                        Final_Launch.Instance.CheckForFinalLaunch();
                    }
                }
                else
                {
                    print("hitting something you are not supposed to");
                }

            }
        }
    }
}
