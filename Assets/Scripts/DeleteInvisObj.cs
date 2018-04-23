using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class DeleteInvisObj : MonoBehaviour 
{

	void FixedUpdate () 
	{
		if(gameObject.layer == 8)
        {
            gameObject.SetActive(false);
        }
	}
}
