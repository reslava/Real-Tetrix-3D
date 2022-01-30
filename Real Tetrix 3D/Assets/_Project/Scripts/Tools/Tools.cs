using UnityEngine;

namespace RafaEslava {

public static class Tools
{
	public static readonly float STEP = 0.2f;

	private static bool isPaused;

	public static bool IsPaused 
	{
		get => isPaused;
		set
		{
			isPaused = value;
			if(isPaused)
			{
				Time.timeScale = 0;
				Events.OnPauseEnter?.Invoke();
			}
			else
			{
			    Time.timeScale = 1;
				Events.OnPauseExit?.Invoke();
			}
		}
	}
	
	public static float EasyOut(float t) => Mathf.Sin(t * Mathf.PI * 0.5f);
	public static float EasyIn(float t) => 1f - Mathf.Cos(t * Mathf.PI * 0.5f);
	public static float Exponential(float t) => t*t;
	public static float SmoothStep(float t) => t*t * (3f - 2f*t);

    public static void MoveToLayer(Transform root, int layer) 
    {
		root.gameObject.layer = layer;
		foreach(Transform child in root)
			MoveToLayer(child, layer);
	}      

    public static int LayerIgnoreRaycast
    {
		get 
        {
			int layer;
		
			layer = 1 << LayerMask.NameToLayer ("Ignore Raycast");
			layer = ~layer;
			return layer;
		}
	}	
}

}