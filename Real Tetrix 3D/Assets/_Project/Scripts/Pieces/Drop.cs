using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace RafaEslava {

public class Drop : MonoBehaviour
{
	public GameData gameData;	
	public GameObject BlockHelper;

    private MainCameraController mainCameraController;
	private Spawner blockSpawner;
	private Planes planesManager;		
	
	public Vector3 Position 
	{ 
		get 
		{
			if(BlockHelper != null)
				return BlockHelper.transform.position;
			return new Vector3(0, 0, 0);
		}
		set => BlockHelper.transform.position = value;
	}
	public Vector3 PositionNext;
    
    private Vector3 positionStart = new Vector3(0,0,0);   				
	[SerializeField] 
	[Range (0.1f, 0.5f)]
	private float timeToStepOnDropping = 0.3f;	
	private float timeDroping;
    public bool OnStep;  
    private const float Step = 0.2f;
    private const float Gap = 0.01f;  

	private void OnEnable() 
	{
		Events.OnBlockHelperDroppingTick.AddListener(OnBlockHelperDropTick);	
		Events.OnBlockHelperDroppingExit.AddListener(OnBlockHelperDropExit);
	}

	private void OnDisable() 
	{
		Events.OnBlockHelperDroppingTick.RemoveListener(OnBlockHelperDropTick);	
		Events.OnBlockHelperDroppingExit.RemoveListener(OnBlockHelperDropExit);
	} 

    private void Awake()      
	{	                        
		mainCameraController = GameObject.FindObjectOfType<MainCameraController>();		
		planesManager = GameObject.FindObjectOfType<Planes>();		
		blockSpawner = GameObject.FindObjectOfType<Spawner>();

		BlockHelper = gameObject;
    }    	

	/// <sumary>
	/// Drop the block until is grounded and then change state to PlanesCheck
	/// Add cubes to a list
	/// <sumary>
	private void OnBlockHelperDropTick()
	{
		if(BlockHelper.GetComponent<States>().State == BlockState.Helper)
		{
			if(!IsDroppedFinished()) 
				return;				
			
			Conditions.IsPlanesChecking = true;
		}
	}

	private void OnBlockHelperDropExit()
	{
		//gameData.Score += gameData.iBlock * 2;		
		blockSpawner.BlockCreate();                                
        blockSpawner.CubesDroppedAdd();
	}	

    public bool IsGrounded 
    {
        get
        {
			//ver si alguno de los cubos del bloque esta apoyado
			Tools.MoveToLayer(BlockHelper.transform, LayerMask.NameToLayer("Ignore Raycast"));
			foreach (Transform transform in BlockHelper.transform) {
				//Debug.DrawRay(transform.position, -Vector3.up * Step, Color.yellow, 10f, false);
				if(Physics.Raycast(transform.position, -Vector3.up, Step, Tools.LayerIgnoreRaycast)) {
					Tools.MoveToLayer(BlockHelper.transform, LayerMask.NameToLayer("Default"));
					return true;
				}
			}
			Tools.MoveToLayer(BlockHelper.transform, LayerMask.NameToLayer("Default"));
			return false;
        }
	}
	
	/// <summary>
    /// Drop the Block step by step until is grounded and return true 
    /// </summary> 	
    public bool IsDroppedFinished()
	{				
		if (!IsGrounded || !OnStep) 
        {
            if(OnStep) 
            {
                timeDroping = 0f;
                positionStart = Position;
                PositionNext =  new Vector3(Position.x, Position.y - Step, Position.z);
                OnStep = false;	
            } 
            timeDroping += Time.deltaTime;			
			float t = timeDroping / timeToStepOnDropping;
			t = Tools.SmoothStep(t); 
            //Position = Vector3.Lerp (positionStart, PositionNext, timeDroping / timeToStep);										
			Position = Vector3.Lerp (positionStart, PositionNext, t);										
            //if(Position.y == PositionNext.y) 						
			if(timeDroping >= timeToStepOnDropping)
			{
				Position = PositionNext;
                OnStep = true;
				Events.OnBlockHelperOnStep?.Invoke();
			}
			//mainCameraController.LookAtBlockHelper();
            return false;
		}
        Events.OnBlockHelperOnStep?.Invoke();
		BlockHelper.GetComponent<Piece>().DroppedAction();
		//Events.OnDropped?.Invoke();
        return true;		
	}    

}

}
