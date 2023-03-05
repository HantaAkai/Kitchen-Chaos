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
            } else { 
                //Player has nothing
            }
        } else {
            // There is a KitchenObject here

            if (player.HasKithcenObject()) {
                //Player has something

                if (player.GetKitchenObject() is PlateKitchenObject) {
                    //Player has a plate

                    PlateKitchenObject plateKitchenObject = player.GetKitchenObject() as PlateKitchenObject;
                    if (plateKitchenObject.TryAddIngredient(GetKitchenObject().GetKithcenObjectSO())) {
                        GetKitchenObject().DestroySelf();
                    }
                }

            } else {
                //Player has nothing

                GetKitchenObject().SetKitchenObjectParent(player);
            }
        }        
    }

   

}
 