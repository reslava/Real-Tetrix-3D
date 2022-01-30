using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace RafaEslava {

public static class  TetrixTools 
{
    public static Color GetColorByIndex(int index)
    {
        Color c;
		switch (index) {
			case 1:
			case 4:
				//c = Color.yellow;				
				c = new Color32(255, 213, 0, 1);
				break;		
			case 2:
			case 5:
			case 10:
				//c = Color.cyan; RED
				c = new Color32(255, 50, 19, 1);
				break;
			case 3:
			case 6:
			case 11:
				//c = Color.blue;
				c = new Color32(3, 65, 174, 1);
				break;
			case 8:
			case 13:
				//c = Color.green;
				c = new Color32(114, 203, 59, 1);
				break;
			case 7:
			case 12:
				//c = Color.red; ORANGE				
				c = new Color32(255, 151, 28, 1);
				break;
			case 9:
			case 14:
				//c = Color.magenta;
				//c = new Color32(244, 154, 194, 1);
				c = new Color32(178, 132, 190, 1);
				break;
			default:
				Debug.Log(">>>CHUNK");
				c = Color.white;
				break;
		}
		return c;  
	}	
}

}