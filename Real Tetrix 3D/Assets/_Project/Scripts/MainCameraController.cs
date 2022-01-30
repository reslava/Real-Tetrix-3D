using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace RafaEslava {

public class MainCameraController : MonoBehaviour
{    
	public GameData gameData;
	private Pieces blocks;
    private Camera mainCamera;  

    private int rotationAngle = 270, rotationAngleFinal = 270;
    public enum CameraPositionsEnum {front, right, behind, left};
	public CameraPositionsEnum CameraPosition = CameraPositionsEnum.front;
    private Vector3 cameraPositionStart, cameraPositionFinal;
    private float cameraY = 4f;
	private float cameraDistance = 3f;

    private float t;

    private void Awake() 
    {        						
        mainCamera = gameObject.GetComponent<Camera>();            
		blocks = GameObject.FindObjectOfType<Pieces>();            

		gameObject.transform.position = CameraFront;
    }

	private void Update() 
	{
		//***********************************************
		// Camera always look to Block Helper		
		LookAtBlockHelper();	
	}

    public void CameraMove()
	{
//		if (state == stateEnum.movingCamera || state == stateEnum.prepareTetris) {
			t += Time.deltaTime * 5f;

			if(rotationAngle != rotationAngleFinal) {
				float angle = Mathf.Lerp(rotationAngle, rotationAngleFinal, t);
				Vector3 position = Vector3.zero;
				float radius;

				radius = Vector2.Distance(new Vector2(cameraPositionStart.x, cameraPositionStart.z), Vector2.zero);

				position.y = cameraY;
				position.x = Mathf.Cos(Mathf.Deg2Rad * angle) * radius;
				position.z = Mathf.Sin(Mathf.Deg2Rad * angle) * radius;
				mainCamera.transform.position = position;
				if(angle == rotationAngleFinal) {
					rotationAngle = rotationAngleFinal;
					if(rotationAngle == 360)
						rotationAngle = rotationAngleFinal = 0;
					if(rotationAngle == -90)
						rotationAngle = rotationAngleFinal = 270;
					//state = stateEnum.idle;
					Conditions.IsCameraMoving = false;
				}
			}
			else {
				mainCamera.transform.position = Vector3.Lerp (cameraPositionStart, cameraPositionFinal, t);
				if(mainCamera.transform.position == cameraPositionFinal) {
					cameraY = cameraPositionFinal.y;
					//!TODO
					//if (state == stateEnum.prepareTetris)
					//	state = stateEnum.menu;
					//else
					//{
						//state = stateEnum.idle;
						Conditions.IsCameraMoving = false;
					//}
				}
			}

			mainCamera.transform.LookAt(blocks.Player.transform.position);
			//gameController.BlockHelperNext_SetOnCorner();
			//return;
		//}        
	}
    public void Prepare() 
    {
        CameraPosition = CameraPositionsEnum.front;
		cameraY = 4f;
		cameraDistance = 3f;
		rotationAngle = 270;
		rotationAngleFinal = 270;

		t = 0f;
		cameraPositionStart = mainCamera.transform.position;
		cameraPositionFinal = CameraFront;
    }

    public void PrepareCameraMoving() 
    {
		t = 0f;
		
		cameraPositionStart = mainCamera.transform.position;
		switch (CameraPosition) {		
		case CameraPositionsEnum.front:
			cameraPositionFinal = CameraFront;
			break;
		case CameraPositionsEnum.behind:
			cameraPositionFinal = cameraBehind;
			break;
		case CameraPositionsEnum.left:
			cameraPositionFinal = cameraLeft;
			break;
		case CameraPositionsEnum.right:
			cameraPositionFinal = cameraRight;
			break;
		}
		//DELETEcameraPositionFinal.y = cameraY;
		//state = stateEnum.movingCamera;
		Conditions.IsCameraMoving = true;
	}

    public Vector3 CameraFront {
		get {
			return new Vector3 (0, cameraY, -cameraDistance);
		}
	}
	private Vector3 cameraBehind {
		get {
			return new Vector3 (0, cameraY, cameraDistance);
		}
	}
	private Vector3 cameraLeft {
		get {
			return new Vector3 (-cameraDistance, cameraY, 0);
		}
	}
	private Vector3 cameraRight {
		get {
			return  new Vector3 (cameraDistance, cameraY, 0);
		}
	}	

	private Vector3 cameraPositionVector {
		get {
			Vector3 v = Vector3.zero;
			switch(CameraPosition) {
			case CameraPositionsEnum.front:
				v = CameraFront;
				break;
			case CameraPositionsEnum.left:
				v = cameraLeft;
				break;
			case CameraPositionsEnum.behind:
				v = cameraBehind;
				break;
			case CameraPositionsEnum.right:
				v = cameraRight;
				break;
			}
			return v;
		}
	}	
	private float cameraAngle(CameraPositionsEnum CameraPosition) {
		float a = 0f;
		switch(CameraPosition) {
		case CameraPositionsEnum.right:
			a = Mathf.Deg2Rad * 0;
			break;
		case CameraPositionsEnum.behind:
			a = Mathf.Deg2Rad * 90;
			break;
		case CameraPositionsEnum.left:
			a = Mathf.Deg2Rad * 180;
			break;
		case CameraPositionsEnum.front:
			a = Mathf.Deg2Rad * 270;
			break;
		}
		return a;
	}    
    //******************************************************************************************
	// Camera Zoom in and out
	//******************************************************************************************
	public void ZoomIn() {
		//if (gameController.GameStateMachine.CurrentState.GetType() == typeof(StatePlaying) && cameraDistance > 0) 
		if (cameraDistance > 0) 
		{
			t = 0f;
			Vector3 p;
			p = mainCamera.transform.position;
			cameraPositionStart = p;
			cameraDistance--;
			cameraPositionFinal = cameraPositionVector;
			//state = stateEnum.movingCamera;
			Conditions.IsCameraMoving = true;
		}
	}
	public void ZoomOut() {
		if (cameraDistance < 10) 
		{
			t = 0f;
			Vector3 p;
			p = mainCamera.transform.position;
			cameraPositionStart = p;
			cameraDistance++; 
			cameraPositionFinal = cameraPositionVector;
			//state = stateEnum.movingCamera;
			Conditions.IsCameraMoving = true;
		}
	}

	//******************************************************************************************
	// Camera Up and Down
	//******************************************************************************************
	public void MoveUp() 
	{
		//if (state == stateEnum.idle && cameraY < 10) 
		if (cameraY < 10) 
		{
			t = 0f;
			Vector3 p;
			p = mainCamera.transform.position;
			cameraPositionStart = p;
			cameraPositionFinal = new Vector3 (p.x, p.y + gameData.Step * 5, p.z);
			//state = stateEnum.movingCamera;
			Conditions.IsCameraMoving = true;
		}
	}

	public void MoveDown() 
	{
		//if (state == stateEnum.idle && cameraY > 0.5) {
		if (cameraY > 0.5) 
		{	
			t = 0f;
			Vector3 p;
			p = mainCamera.transform.position;
			cameraPositionStart = p;                        
			cameraPositionFinal = new Vector3 (p.x, p.y - gameData.Step * 5, p.z);
			//state = stateEnum.movingCamera;
			Conditions.IsCameraMoving = true;
		}
	}

	//******************************************************************************************
	// Camera Left and Right
	//******************************************************************************************
	public void MoveLeft() 
	{
		//if (gameController.GameStateMachine.CurrentState.GetType() == typeof(StatePlaying))
		{
			CameraPosition--;
			if (CameraPosition < CameraPositionsEnum.front)
				CameraPosition = CameraPositionsEnum.left;
			rotationAngleFinal = rotationAngle - 90;
			//Debug.Log ((rotationAngle).ToString () + " - " + (rotationAngleFinal).ToString ());
			PrepareCameraMoving ();
		}
	}

	public void MoveRigth() 
	{
		//if (gameController.GameStateMachine.CurrentState.GetType() == typeof(StatePlaying))
		{
			CameraPosition++;
			if (CameraPosition > CameraPositionsEnum.left)
				CameraPosition = CameraPositionsEnum.front;
			rotationAngleFinal = rotationAngle + 90;
			//if(rotationAngleFinal) < 5) rotationAngleFinal = 0;
			//if(Mathf.Abs(Mathf.Rad2Deg * rotationAngleFinal - 360) < 5) rotationAngleFinal = 0;
			//Debug.Log ((rotationAngle).ToString () + " - " + (rotationAngleFinal).ToString ());
			PrepareCameraMoving ();
		}
	}


	public void LookAtBlockHelper()
	{
		//Vector3 relativePos = blockHelperController.Position - mainCamera.gameObject.transform.position;
		if(blocks.Player) 
		{
			Vector3 relativePos = blocks.Player.transform.position - mainCamera.gameObject.transform.position;
			Quaternion rotation = Quaternion.LookRotation(relativePos);
			mainCamera.gameObject.transform.rotation = Quaternion.Slerp (mainCamera.transform.rotation, rotation, Time.deltaTime * 2);
		}
	}

}

}