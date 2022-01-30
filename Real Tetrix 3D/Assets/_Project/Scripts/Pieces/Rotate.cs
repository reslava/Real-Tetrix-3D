using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace RafaEslava {

public class Rotate : MonoBehaviour
{
    private MainCameraController mainCameraController;
	private Planes planesManager;
	public GameData gameData;	
    public GameObject BlockHelper;

    [SerializeField] 
	[Range (0.1f, 0.5f)]
	private float timeToRotate = 0.3f;
    private float timeRotating = 0f;
	
	//private int rotationAngle = 270, rotationAngleFinal = 270;
	private Quaternion rotationFrom, rotationTo;

    private void OnEnable() 
	{	
		Events.OnBlockHelperRotatingTick.AddListener(OnBlockHelperRotatingTick);	
		Events.OnBlockHelperRotatingEnter.AddListener(OnBlockHelperRotatingEnter);
	}

	private void OnDisable() 
	{	
		Events.OnBlockHelperRotatingTick.RemoveListener(OnBlockHelperRotatingTick);	
		Events.OnBlockHelperRotatingEnter.RemoveListener(OnBlockHelperRotatingEnter);
	}     

    private void Awake()      
	{	                        
		mainCameraController = GameObject.FindObjectOfType<MainCameraController>();		
		planesManager = GameObject.FindObjectOfType<Planes>();

		BlockHelper = gameObject;
    }

    
    //***********************************************************************************
	// Rotate Block different axes depending of camera position
	//***********************************************************************************
	public void RotateUp(float degrees)//, ref Quaternion rotationFrom, ref Quaternion rotationTo) {
	{
		// GameController.Instance.BlockHelper.GetComponent<AudioSource> ().clip = Block_Rotate;
		// GameController.Instance.BlockHelper.GetComponent<AudioSource> ().Play();
		timeRotating = 0f;
		Vector3 rotateDegrees = GetRotationUpAxe() * degrees;
		rotationFrom = BlockHelper.gameObject.transform.rotation;
		// el orden have que sean ejes locales o globales
		rotationTo = Quaternion.Euler (rotateDegrees) * rotationFrom; 					
		//state = stateEnum.rotateBlock;					
		Conditions.IsBlockHelperRotating = true;
	}

	public void RotateRight(float degrees)//, ref Quaternion rotationFrom, ref Quaternion rotationTo) {
	{
		// GameController.Instance.BlockHelper.GetComponent<AudioSource> ().clip = Block_Rotate;
		// GameController.Instance.BlockHelper.GetComponent<AudioSource> ().Play();
		timeRotating = 0f;
		Vector3 rotateDegrees = GetRotationRightAxe() * degrees;
		rotationFrom = BlockHelper.gameObject.transform.rotation;
		// el orden have que sean ejes locales o globales
		rotationTo = Quaternion.Euler (rotateDegrees) * rotationFrom; 					
		//state = stateEnum.rotateBlock;					
		Conditions.IsBlockHelperRotating = true;
	}

	public void RotateForward(float degrees)//, ref Quaternion rotationFrom, ref Quaternion rotationTo) {
	{
		// GameController.Instance.BlockHelper.GetComponent<AudioSource> ().clip = Block_Rotate;
		// GameController.Instance.BlockHelper.GetComponent<AudioSource> ().Play();
		timeRotating = 0f;
		Vector3 rotateDegrees = GetRotationForwardAxe() * degrees;
		rotationFrom = BlockHelper.gameObject.transform.rotation;
		// el orden have que sean ejes locales o globales
		rotationTo = Quaternion.Euler (rotateDegrees) * rotationFrom; 					
		//state = stateEnum.rotateBlock;					
		Conditions.IsBlockHelperRotating = true;
	}

	private Vector3 GetRotationUpAxe(){
		Vector3 axe = Vector3.up;
		switch (mainCameraController.CameraPosition) {			
		case MainCameraController.CameraPositionsEnum.front:
			axe = Vector3.up;
			break;
		case MainCameraController.CameraPositionsEnum.behind:
			axe = -Vector3.down;
			break;
		case MainCameraController.CameraPositionsEnum.left:
			axe = Vector3.up;
			break;
		case MainCameraController.CameraPositionsEnum.right:
			axe = -Vector3.down;
			break;
		}
		return axe;
	}
	private Vector3 GetRotationRightAxe(){
		Vector3 axe = Vector3.right;
		switch (mainCameraController.CameraPosition) {			
		case MainCameraController.CameraPositionsEnum.front:
			axe = Vector3.right;
			break;
		case MainCameraController.CameraPositionsEnum.behind:
			axe = Vector3.left;
			break;
		case MainCameraController.CameraPositionsEnum.left:
			axe = Vector3.back;
			break;
		case MainCameraController.CameraPositionsEnum.right:
			axe = Vector3.forward;
			break;
		}
		return axe;
	}
	private Vector3 GetRotationForwardAxe(){
		Vector3 axe = Vector3.forward;
		switch (mainCameraController.CameraPosition) {			
		case MainCameraController.CameraPositionsEnum.front:
			axe = Vector3.back;
			break;
		case MainCameraController.CameraPositionsEnum.behind:
			axe = Vector3.forward;
			break;
		case MainCameraController.CameraPositionsEnum.left:
			axe = Vector3.left;
			break;
		case MainCameraController.CameraPositionsEnum.right:
			axe = Vector3.right;
			break;
		}
		return axe;
	}

	
	public void OnBlockHelperRotatingTick()
	{
		if(timeRotating < timeToRotate) {
            timeRotating += Time.deltaTime;// * rotateRate;    
			float t = timeRotating / timeToRotate;
			t = Tools.SmoothStep(t);                     
            BlockHelper.transform.rotation = Quaternion.Lerp (rotationFrom, rotationTo, t);		                           
        }

        if(timeRotating >= timeToRotate) {
			timeRotating = 0f;
           BlockHelper.transform.rotation = rotationTo;        
           Conditions.IsBlockHelperRotating = false;		   
        }
	}

	public void OnBlockHelperRotatingEnter()
	{
		timeRotating = 0f;		
	}     
}

}
