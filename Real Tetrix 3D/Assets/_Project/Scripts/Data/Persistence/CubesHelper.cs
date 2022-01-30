using System.Collections.Generic;
using UnityEngine;

namespace RafaEslava {
    
public class CubesHelper : Singleton<CubesHelper>
{
    public GameObject Cube;	
    public GameData gameData;	

    private Pieces blocks;		

    private void Start()
	{								
		blocks = GameObject.FindObjectOfType<Pieces>();
    }		

    public void CubesToList()
    {       
        gameData.CubesDroppedList = new List<CubeDropped>();        	
        foreach(Transform transform in blocks.DroppedParent.transform)		
        {
			GameObject cube = transform.gameObject;
            CubeDropped cubeDropped = new CubeDropped();

            cubeDropped.x = transform.position.x;
            cubeDropped.y = transform.position.y;
            cubeDropped.z = transform.position.z;

            cubeDropped.Id = cube.GetComponent<Cube>().Id;
            //cubeDropped.index = int.Parse(cube.name.Substring(4));
            
            gameData.CubesDroppedList.Add(cubeDropped);
        }
    }

    public void CubesFromList()
    {
        foreach(CubeDropped cube in gameData.CubesDroppedList)		
        {			            
            Vector3 position = new Vector3(cube.x, cube.y, cube.z);            
            string Id = cube.Id;                        
            Cube cubePrefab;
            if(gameData.IdToCubesAvailables.TryGetValue(Id, out cubePrefab))            
                cubePrefab = Instantiate (cubePrefab, position, Quaternion.identity, blocks.DroppedParent.transform);
            else
                Debug.LogError("Cube Id not in Dictionary of CubesAvailables");
                
            
            // cubeDropped.name = "Cube" + index.ToString();
            
            // Renderer render = cubeDropped.GetComponent<Renderer>();                       
            // render.material.color = TetrixTools.GetColorByIndex(index);            


            
			// old GameObject cubeDropped  = (GameObject)Instantiate (gameData.CubesBlocks[index - 1], position, Quaternion.identity, blocks.DroppedParent.transform);
            //IdGameObject cubeDropped  = (GameObject)Instantiate (gameData.CubesAvailablesList(Id), position, Quaternion.identity, blocks.DroppedParent.transform);
            // cubeDropped.name = "Cube" + index.ToString();
            
            // Renderer render = cubeDropped.GetComponent<Renderer>();                       
            // render.material.color = TetrixTools.GetColorByIndex(index);            
        }        
    }	    
}

}
