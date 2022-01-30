using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace RafaEslava {

public class Pieces : MonoBehaviour
{    
    public GameObject Player;
    public GameObject Next;

    public GameObject PlayerParent;		
	  public GameObject NextParent;	
	  public GameObject DroppedParent;

    private void Awake() 
    {       		        
      PlayerParent = gameObject.transform.GetChild(0).gameObject;				
      NextParent = gameObject.transform.GetChild(1).gameObject;		
      DroppedParent = gameObject.transform.GetChild(2).gameObject;	        						
    }		
	
}

}
