using UnityEngine;

namespace RafaEslava {

public class PieceLife : Piece
{
    public PieceLife()
    {
    }

    public override void DroppedAction()
    {
        Debug.Log("piece life : abstract piece");
    }
}

}
