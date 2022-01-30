using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace RafaEslava {

public class CubeBlock : Cube
{
    [SerializeField] private int _points;

    public override void PlaneDoneAction()
    {
        _gameData.Blocks++;
        _gameData.BlocksPoints += _points;
    }
}

}
