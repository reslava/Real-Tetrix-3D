using UnityEngine;

namespace RafaEslava {

public class PieceHourglass : Piece
{
    public PieceHourglass()
    {
    }

    public override void DroppedAction()
    {
        Debug.Log("PieceHourglass : abstract piece");
    }
}

}
