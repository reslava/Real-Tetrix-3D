namespace RafaEslava {

public class CubeLife : Cube
{    
    public override void PlaneDoneAction()
    {
        _gameData.Lives++;        
    }
}

}
