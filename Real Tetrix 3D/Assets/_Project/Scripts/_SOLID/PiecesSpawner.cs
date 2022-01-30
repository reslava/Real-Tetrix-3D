using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace RafaEslava {

public class PiecesSpawner : MonoBehaviour
{
    [SerializeField] PiecesConfiguration _piecesConfiguration;

    private PieceFactory _pieceFactory;

    private void Awake() 
    {
        _pieceFactory = new PieceFactory(_piecesConfiguration);
    }

    private void Update() 
    {
        _pieceFactory.Create("Block");
        _pieceFactory.Create("Coin");    
    }
}

}
