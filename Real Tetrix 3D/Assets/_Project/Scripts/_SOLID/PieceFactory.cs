using Object = UnityEngine.Object;

namespace RafaEslava {

public class PieceFactory 
{
    PiecesConfiguration _piecesConfiguration;

    public PieceFactory(PiecesConfiguration piecesConfiguration)
    {
        _piecesConfiguration = piecesConfiguration;
    }

    public Piece Create(string id)
    {                
        var piece = _piecesConfiguration.GetPiecePrefabById(id);
        return Object.Instantiate(piece);
    }
}

}
