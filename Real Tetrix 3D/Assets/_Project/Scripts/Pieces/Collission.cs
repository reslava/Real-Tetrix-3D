using UnityEngine;
using UnityEngine.Events;

namespace RafaEslava {

public class Collission : MonoBehaviour 
{
	public static bool IsBlockCollision = false;		

	void OnTriggerEnter(Collider other) 
	{
		if(tag != "Next" && Conditions.IsPlaying)				{ 			
		
			IsBlockCollision = true;											
			gameObject.GetComponentInParent<States>()?.CollisionEnter();
		}
	}

	// void OnTriggerStay(Collider other) 
	// {
	// 	if(tag != "Next") 
	// 	{
	// 		IsBlockCollision = true;	
	// 		gameObject.GetComponentInParent<BlockStates>()?.CollisionEnter();
	// 	}
	// }
			
	void OnTriggerExit(Collider other) 
	{		
		if(tag != "Next" && Conditions.IsPlaying)		
		{			
			IsBlockCollision = false;				
			gameObject.GetComponentInParent<States>()?.CollisionExit();		
		}
	}
}

}