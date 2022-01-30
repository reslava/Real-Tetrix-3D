using System;
using System.Collections.Generic;
using UnityEngine;

namespace RafaEslava {

[CreateAssetMenu (menuName ="Tetrix/Pieces Configuration")]
public class PiecesConfiguration : ScriptableObject
{
    [SerializeField] private Piece[] _pieces;
    private readonly Dictionary<string, Piece> _piecesDictionary;

    public PiecesConfiguration()
    {
        _piecesDictionary = new Dictionary<string, Piece>();
        foreach(var piece in _pieces)
            _piecesDictionary.Add(piece.Id, piece);
    }

    public Piece GetPiecePrefabById(string id)
    {
        if(!_piecesDictionary.TryGetValue(id, out var piece))
            throw new Exception($"Piece with id <{id}> not found in the Factory"); 

        return piece;
    }
}

}
