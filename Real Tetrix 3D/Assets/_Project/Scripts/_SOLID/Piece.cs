using UnityEngine;

namespace RafaEslava {

public abstract class Piece : MonoBehaviour
{
    [SerializeField] protected string _id;
    public string Id => _id;

    [SerializeField] protected bool _isMoveable;
    public bool IsMoveable => _isMoveable;
    
    bool IsMoveUpDownRestricted { get; }
    bool IsRotable { get; }
    bool IsRotateXRestricted { get; }
    bool IsRotateYRestricted { get; }
    bool IsRotateZRestricted { get; }
    bool IsDroppable { get; }
    bool IsAutoDropped { get; }

    public abstract void DroppedAction(); 
    
    
}

}
