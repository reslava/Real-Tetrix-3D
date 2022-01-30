using UnityEngine;
using UnityEngine.Events;

namespace RafaEslava {

public enum BlockState {Normal, Helper, HelperNext, Menu};

public class States : MonoBehaviour
{        
    [SerializeField] public int Index;  
    [SerializeField] public Color Color;    

    [SerializeField]
    private BlockState state;
    
    public BlockState State
    {
        get { return state; }
        set 
        {
            state = value;
            switch(state)
            {
                case BlockState.Helper:
                    HelperInitialze();
                break;
                case BlockState.HelperNext:
                    HelperNextInitialze();
                break;
            }
        }
    }      

    public void CollisionEnter()
    {
        Color c = Color;
        c.r -= 0.35f;
		c.g -= 0.35f;
		c.b -= 0.35f;
        foreach(Renderer render in GetComponentsInChildren<Renderer>()) 
        {
            render.material.color = c;
        }
    }
    
    public void CollisionExit()
    {
        RestoreColor();
    }

    private void SetColor(Color c)
    {        
        foreach(Renderer render in GetComponentsInChildren<Renderer>())              
            render.material.color = c;                           
    }

    private void RestoreColor() => SetColor(Color);        

    public void Copy(GameObject block)     
    {    
        gameObject.transform.rotation = block.gameObject.transform.rotation;     
    }    

    private void HelperInitialze()
    {        
        gameObject.gameObject.transform.localScale = new Vector3(1.002f, 1.002f, 1.002f);

        Tools.MoveToLayer(gameObject.transform, LayerMask.NameToLayer("Ignore Raycast"));        
    }

    private void HelperNextInitialze()
    {
        float size_x2;
            float size_y2;
            switch (Index)
            {
                case 1:
                case 2:
                case 5:
                case 10:
                    size_x2 = 0f;
                    break;
                default:
                    size_x2 = 0.1f;
                    break;
            }
            switch (Index)
            {
                case 1:
                    size_y2 = 0f;
                    break;
                case 2:
                case 3:
                case 4:
                    size_y2 = 0.1f;
                    break;
                case 5:
                case 6:
                case 7:
                case 8:
                case 9:
                    size_y2 = 0.2f;
                    break;
                default:
                    size_y2 = 0.3f;
                    break;
            }

            float pos_x = -300f - size_x2;
            float pos_y = 0f - size_y2;
            
            gameObject.transform.position = new Vector3(pos_x, pos_y, 1);

            foreach (Transform transform in gameObject.transform)
                transform.tag = "Next";

            Tools.MoveToLayer(gameObject.transform, LayerMask.NameToLayer("Ignore Raycast"));
    }    

    private void Update() 
    {
        if(State == BlockState.HelperNext)
            transform.Rotate(0, 180 * Time.deltaTime, 0);    
        if(State == BlockState.Menu)
            transform.Rotate(0, 20 * Time.unscaledDeltaTime, 0);    
    }       
}

}