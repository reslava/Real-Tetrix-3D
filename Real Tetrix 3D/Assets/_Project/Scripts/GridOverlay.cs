using UnityEngine;
using System.Collections.Generic;

namespace RafaEslava {

[RequireComponent(typeof(LineRenderer))]
public class GridOverlay : MonoBehaviour
{	

	public int Size = 2;
	
	public bool showMain = true;
	public bool showSub = false;
	
	private float gridSizeX;
	//public float gridSizeY;
	private float gridSizeZ;
	
	//private float smallStep = 0.1f;
	private float largeStep = 0.2f;
	
	private float startX;
	//public float startY;
	private float startZ;
	
	private float offsetY = 0;
	
	//private Material lineMaterial;
	//public Material lineMaterial;
	
	//!private Color mainColor = new Color(1f,1f,0f,1f);
	//!private Color subColor = new Color(0f,0.5f,0f,1f);

	private const float STEP = 0.2f;
	private const float GAP = 0.01f;

	private List<Vector3> pos = new List<Vector3>();	
	private LineRenderer l;

	
	void Start () 
	{
		//l = gameObject.AddComponent<LineRenderer>();  
		l = gameObject.GetComponent<LineRenderer>();  
		l.startWidth = 0.020f;
		l.endWidth = 0.020f;
		l.useWorldSpace = true;
		Size = 2;
		Draw();		
	}

	public void Draw() 
	{
		if (Size < 2)
			Size = 2;
		if (Size > 16)
			Size = 16;

		gridSizeX = Size * STEP + GAP;
		gridSizeZ = Size * STEP + GAP;
		startX = -gridSizeX / 2;
		startZ = -gridSizeZ / 2;

		l.positionCount = 0;
		pos.Clear();

		if(showMain)
		{					
			//Debug.Log("GRID" + gridSizeX + " - " + gridSizeY + " - " +  startX + " - " +  startZ);							
			//for(float j = 0; j <= gridSizeY; j += largeStep)
			//{
				float j = 0;
				//X axis lines
				for(float i = 0; i <= gridSizeZ; i += largeStep)
				{									      					
					pos.Add(new Vector3(startX, j + offsetY, startZ + i));
					pos.Add(new Vector3(startX +  gridSizeX, j + offsetY, startZ + i));														
					pos.Add(new Vector3(startX, j + offsetY, startZ + i));
				}
				//Z axis lines
				for(float i = 0; i <= gridSizeX; i += largeStep)
				{
					pos.Add(new Vector3(startX + i, j + offsetY, startZ));					
					pos.Add(new Vector3(startX + i, j + offsetY, startZ + gridSizeZ));
					pos.Add(new Vector3(startX + i, j + offsetY, startZ));					
				}
			//}					
		}
		l.positionCount = pos.Count;
		l.SetPositions(pos.ToArray());		
	}

	// // // public static void BakeLineDebuger(GameObject lineObj)
	// // // {
	// // // 	var lineRenderer = lineObj.GetComponent<LineRenderer>();
	// // // 	var meshFilter = lineObj.AddComponent<MeshFilter>();
	// // // 	Mesh mesh = new Mesh();
	// // // 	lineRenderer.BakeMesh(mesh);
	// // // 	meshFilter.sharedMesh = mesh;

	// // // 	var meshRenderer = lineObj.AddComponent<MeshRenderer>();
	// // // 	meshRenderer.sharedMaterial = s_matDebug;

	// // // 	GameObject.Destroy(lineRenderer);
	// // // }

    /*
	void CreateLineMaterial() 
	{
		
		if( !lineMaterial ) {
            lineMaterial = new Material( "Shader \"Lines/Colored Blended\" {" +
			                            "SubShader { Pass { " +
			                            "    Blend SrcAlpha OneMinusSrcAlpha " +
			                            "    ZWrite Off Cull Off Fog { Mode Off } " +
			                            "    BindChannels {" +
			                            "      Bind \"vertex\", vertex Bind \"color\", color }" +
			                            "} } }" );            

            lineMaterial.hideFlags = HideFlags.HideAndDontSave;
			lineMaterial.shader.hideFlags = HideFlags.HideAndDontSave;            
         }
	}
	*/

	// // // /// <sumary>
	// // // /// This must be in same GameObject as Camera
	// // // /// <sumary>
	// // // void OnPostRender() 
	// // // {        
	// // // 	//CreateLineMaterial();
	// // // 	// set the current material
	// // // 	//lineMaterial.SetPass( 0 );
	// // // 	lineMaterial.SetPass( 0 );
		
	// // // 	GL.Begin( GL.LINES );
		
	// // // 	if(showSub)
	// // // 	{
	// // // 		//!GL.Color(subColor);
			
	// // // 		//Layers
	// // // 		for(float j = 0; j <= gridSizeY; j += smallStep)
	// // // 		{
	// // // 			//X axis lines
	// // // 			for(float i = 0; i <= gridSizeZ; i += smallStep)
	// // // 			{
	// // // 				GL.Vertex3( startX, j + offsetY, startZ + i);
	// // // 				//GL.Vertex3( gridSizeX, j + offsetY, startZ + i);
	// // // 				GL.Vertex3( startX + gridSizeX, j + offsetY, startZ + i);
	// // // 			}
				
	// // // 			//Z axis lines
	// // // 			for(float i = 0; i <= gridSizeX; i += smallStep)
	// // // 			{
	// // // 				GL.Vertex3( startX + i, j + offsetY, startZ);
	// // // 				//GL.Vertex3( startX + i, j + offsetY, gridSizeZ);
	// // // 				GL.Vertex3( startX + i, j + offsetY, startZ + gridSizeZ);
	// // // 			}
	// // // 		}
			
	// // // 		//Y axis lines
	// // // 		for(float i = 0; i <= gridSizeZ; i += smallStep)
	// // // 		{
	// // // 			for(float k = 0; k <= gridSizeX; k += smallStep)
	// // // 			{
	// // // 				GL.Vertex3( startX + k, startY + offsetY, startZ + i);
	// // // 				//GL.Vertex3( startX + k, gridSizeY + offsetY, startZ + i);
	// // // 				GL.Vertex3( startX + k, startY + gridSizeY + offsetY, startZ + i);
	// // // 			}
	// // // 		}
	// // // 	}
		
	// // // 	if(showMain)
	// // // 	{
	// // // 		//!GL.Color(mainColor);
			
	// // // 		//Layers
	// // // 		for(float j = 0; j <= gridSizeY; j += largeStep)
	// // // 		{
	// // // 			//X axis lines
	// // // 			for(float i = 0; i <= gridSizeZ; i += largeStep)
	// // // 			{
	// // // 				GL.Vertex3( startX, j + offsetY, startZ + i);
	// // // 				//GL.Vertex3( gridSizeX, j + offsetY, startZ + i);
	// // // 				GL.Vertex3( startX +  gridSizeX, j + offsetY, startZ + i);					
	// // // 			}
				
	// // // 			//Z axis lines
	// // // 			for(float i = 0; i <= gridSizeX; i += largeStep)
	// // // 			{
	// // // 				GL.Vertex3( startX + i, j + offsetY, startZ);
	// // // 				//GL.Vertex3( startX + i, j + offsetY, gridSizeZ);
	// // // 				GL.Vertex3( startX + i, j + offsetY, startZ + gridSizeZ);
	// // // 			}
	// // // 		}
			
	// // // 		//Y axis lines
	// // // 		for(float i = 0; i <= gridSizeZ; i += largeStep)
	// // // 		{
	// // // 			for(float k = 0; k <= gridSizeX; k += largeStep)
	// // // 			{
	// // // 				GL.Vertex3( startX + k, startY + offsetY, startZ + i);
	// // // 				//GL.Vertex3( startX + k, gridSizeY + offsetY, startZ + i);
	// // // 				GL.Vertex3( startX + k, startY + gridSizeY + offsetY, startZ + i);
	// // // 			}
	// // // 		}
	// // // 	}		
		
	// // // 	GL.End();		
	// // // }

}

}