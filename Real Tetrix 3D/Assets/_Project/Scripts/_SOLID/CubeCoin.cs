using UnityEngine;

namespace RafaEslava {

public class CubeCoin : Cube
{
    [SerializeField] private int _points;

    public override void PlaneDoneAction()
    {
        _gameData.Coins++;
        _gameData.CoinsPoints += _points;
    }
}

}
