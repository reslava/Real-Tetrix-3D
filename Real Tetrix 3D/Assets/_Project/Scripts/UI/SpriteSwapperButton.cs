using UnityEngine;
using UnityEngine.UI;
 
public class SpriteSwapperButton : Button
{
   private Sprite normalSprite;
 
   protected override void Awake()
   {
     base.Awake();
     normalSprite = image.sprite;
   }
 
   protected override void DoStateTransition (Selectable.SelectionState state, bool instant)
   {
     base.DoStateTransition(state, instant);
 
     Sprite newSprite = null;
 
     switch (state)
     {
       case Selectable.SelectionState.Normal:
         newSprite = normalSprite;
         break;
       case Selectable.SelectionState.Highlighted:
         newSprite = this.spriteState.highlightedSprite;
         break;
       case Selectable.SelectionState.Pressed:
         newSprite = this.spriteState.pressedSprite;
         break;
       case Selectable.SelectionState.Disabled:
         newSprite = this.spriteState.disabledSprite;
         break;
     }
 
     if (newSprite != null) {
       image.sprite = newSprite;
     }
   }
}