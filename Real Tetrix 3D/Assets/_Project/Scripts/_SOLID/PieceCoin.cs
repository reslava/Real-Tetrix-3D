using UnityEngine;

namespace RafaEslava {

public class PieceCoin : Piece
{
    public PieceCoin()
    {
    }

    public override void DroppedAction()
    {
        Debug.Log("pieceblock : abstract piece");
    }
}

}
