using UnityEngine;

namespace RafaEslava {

public class PieceBlock : Piece
{
    public PieceBlock()
    {
    }

    public override void DroppedAction()
    {
        Debug.Log("pieceblock : abstract piece");
    }
}

}
