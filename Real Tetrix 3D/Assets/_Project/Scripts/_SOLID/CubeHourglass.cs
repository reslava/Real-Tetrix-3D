using UnityEngine;

namespace RafaEslava {

public class CubeHourglass : Cube
{
    [SerializeField] private int _planeSecondsExtra;
    [SerializeField] private int _levelUpSecondsExtratime;


    public override void PlaneDoneAction()
    {
        _gameData.PlaneTimeTo += _planeSecondsExtra;
        _gameData.LevelUpTimeTo += _levelUpSecondsExtratime;
    }
}

}
