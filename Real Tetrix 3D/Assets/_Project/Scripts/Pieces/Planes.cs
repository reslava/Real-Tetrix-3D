using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace RafaEslava {

public class Planes : MonoBehaviour
{   
	public GameData gameData;

    private Spawner blockSpawner;	
	private Pieces blocks;	

    private void Start() 
    {		
        blocks = GetComponent<Pieces>();
		blockSpawner = GetComponent<Spawner>();
    }    

    public void PlanesCheck()	
	{		
		StartCoroutine (PlanesCheckCoroutine ());	
	}

	public int PlaneGetFirstEmpty() 
	{
		for (int i = 0; i < gameData.maxYStart; i++)   
			if (PlaneIsEmpty (i)) 
				return i;			
		
		return gameData.maxYStart;
	}

	private bool PlaneIsFull(int level) 
	{		
		RaycastHit[] hits = new RaycastHit[0];
		int hitsCounter = 0;
		int layerMask;

		layerMask = 1 << LayerMask.NameToLayer ("Ignore Raycast");
		layerMask = ~layerMask;

		for(int x = -gameData.Size / 2; x <= gameData.Size / 2 - gameData.xMax; x++) 
		{
			hits = Physics.RaycastAll(new Vector3(x * gameData.Step + gameData.xEven, level * gameData.Step + 0.1f, -10), Vector3.forward, 20, layerMask);
			hitsCounter += hits.Length;
			//Debug.DrawRay(new Vector3(x * Step + xEven, level * Step + 0.1f, -10), Vector3.forward * 20f, Color.yellow, 10f, false);
		}		
		
		//Debug.Log(hitsCounter + " - " + size);		
		var isPlaneFull = hitsCounter == gameData.Size * gameData.Size;
		if(isPlaneFull)		
		{
			gameData.NewScoreInitialize();
			Debug.Log("PREFAB PLANE DONE");
			foreach(var cube in CubesInLevel(level))
				cube.GetComponent<Cube>().PlaneDoneAction();
				//cube.GetComponent<ICube>().PlaneDoneAction();
				//hit.transform.gameObject.GetComponent<ICube>().PlaneDoneAction();
		}

		return isPlaneFull;
	}

	private bool PlaneIsEmpty(int level) 
	{
		RaycastHit[] hits;
		int hitsCounter = 1;
		int layerMask;
		
		layerMask = 1 << LayerMask.NameToLayer ("Ignore Raycast");
		layerMask = ~layerMask;
		
		for(int x = -gameData.Size / 2; x <= gameData.Size / 2 - gameData.xMax; x++) 
		{
			hits = Physics.RaycastAll(new Vector3(x * gameData.Step + gameData.xEven, level *gameData.Step + 0.1f, -10), Vector3.forward, 20, layerMask);
			hitsCounter += hits.Length;
			//Debug.DrawRay(new Vector3(x * STEP + xEven, level * STEP + 0.1f, -10), Vector3.forward * 20f, Color.yellow, 10f, false);
		}
		return hitsCounter == 1;
	}

	private IEnumerator ScaleCubes(List<GameObject> cubes)
	{
		float t;
		float rate;		
		Vector3 from, to;		

		// escalar cubos del nivel
		t = 0.0f;
		rate = 1 / 0.8f;		
		from = new Vector3(0.2f, 0.2f, 0.2f);
		to = new Vector3(0.05f, 0.05f, 0.05f);
		while (t < 1.0f) {
			t += Time.deltaTime * rate;
			foreach(GameObject cube in cubes) 
				cube.transform.localScale = Vector3.Lerp(from, to, t);
			yield return new WaitForEndOfFrame ();
		} 

		t = 0.0f;
		rate = 1 / 0.8f;		
		while (t < 1.0f) {
			t += Time.deltaTime * rate;						
			foreach(GameObject cube in cubes) 
				cube.transform.localScale = Vector3.Lerp(to, from, t);
			yield return new WaitForEndOfFrame ();
		}

		foreach (GameObject cube in cubes) 
			cube.transform.localScale = new Vector3 (0.2f, 0.2f, 0.2f);
	}

	private List<GameObject> CubesInLevel(int level)
	{
		List<GameObject> cubesInLevel = new List<GameObject>(); 
		RaycastHit[] hits;

		// Guardar una lista con los cubos del nivel
		for(int x = -gameData.Size / 2; x <= gameData.Size / 2 - gameData.xMax; x++) {
			hits = Physics.RaycastAll(new Vector3(x * gameData.Step + gameData.xEven, level * gameData.Step + 0.1f, -10), Vector3.forward, 20, Tools.LayerIgnoreRaycast);
			foreach(RaycastHit raycastHit in  hits) {
				cubesInLevel.Add(raycastHit.collider.gameObject);
			}
		}

		return cubesInLevel;
	}
	private IEnumerator PlaneClean(int level) 
	{
		List<GameObject> cubesInLevel = CubesInLevel(level);

		yield return StartCoroutine(ScaleCubes(cubesInLevel));
		
		foreach(GameObject cube in cubesInLevel)
			Destroy(cube);			
		cubesInLevel.Clear ();
		cubesInLevel = null;
		yield return new WaitForEndOfFrame ();

		//recorrer cubos superiores al nivel y bajarlos 		
		foreach (Transform transform in blocks.DroppedParent.transform) 
			//!TODO antes era if (cube.transform.position.y >= level * STEP + 0.1f) {
			if (transform.position.y >= level * gameData.Step + 0.15f) {
				transform.position = new Vector3 (transform.position.x, transform.position.y - gameData.Step, transform.position.z);				
			}
		yield return new WaitForEndOfFrame ();
		
		Events.OnPlaneDoneAndCleaned?.Invoke();
	}

    private IEnumerator PlanesCheckCoroutine() 
	{
		gameData.planesDoneAtOnce = 0;
		yield return new WaitForEndOfFrame();													
		
		for (int i = 0; i < PlaneGetFirstEmpty(); i++) 
		{  			
			if (PlaneIsFull (i)) 
			{								
				//var uiPlay = FindObjectOfType<UiPlay>();
				//Events.OnNewScore?.Invoke();
				gameData.OnPlaneDone();				
												
				yield return StartCoroutine( PlaneClean (i) );	

				//Conditions.IsPlaneDone = true;						
				
				// antes de pone i-- habia q hacer break y empezar topo ***yield break;  
				yield return null;

				// The plane done has been cleaned and the rest of cubes lower doen so we start again same i
				i--;				
			}			
		}					        
        blockSpawner.BlockHelperCreate ();
        
        Conditions.IsPlanesChecking = false;    
	}

	public float PlaneGetY(int plane) 
	{
		return plane * gameData.Step + gameData.Step / 2;
	}
}
}