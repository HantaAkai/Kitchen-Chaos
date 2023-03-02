using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ClearCounter : BaseCounter
{
    [SerializeField] private KitchenObjectSO kitchenObjectSO;
    

    public override void Interact(Player player) {
        if (!HasKithcenObject()) {
            //There is no KitchenObject here

            if (player.HasKithcenObject()) { 
                //Player has something
                player.GetKitchenObject().SetKitchenObjectParent(this);
            }
        } else { 
            // There is a KitchenObject here
        }        
    }

   

}
 