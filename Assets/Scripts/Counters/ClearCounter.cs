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

                if (player.GetKitchenObject().TryGetPlate(out PlateKitchenObject plateKitchenObject)) {
                    //Player has a plate

                    if (plateKitchenObject.TryAddIngredient(GetKitchenObject().GetKithcenObjectSO())) {
                        GetKitchenObject().DestroySelf();
                    }
                } else {
                    //Player is holding something, but it's not a plate
                    if (GetKitchenObject().TryGetPlate(out plateKitchenObject)) {
                        //Counter has a plate
                        if (plateKitchenObject.TryAddIngredient(player.GetKitchenObject().GetKithcenObjectSO())) {
                            player.GetKitchenObject().DestroySelf();
                        }
                    }
                }


            } else {
                //Player has nothing

                GetKitchenObject().SetKitchenObjectParent(player);
            }
        }        
    }

   

}
 