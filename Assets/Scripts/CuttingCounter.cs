using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CuttingCounter : BaseCounter
{

    [SerializeField] private KitchenObjectSO cutKitchenObjectSO;
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
            } else {
                //Player has nothing

                GetKitchenObject().SetKitchenObjectParent(player);
            }
        }
    }

    public override void InteractAlternative(Player player) {
        if (HasKithcenObject()) { 
            //There is a KitchenObject here
            GetKitchenObject().DestroySelf();

            KitchenObject.SpawnKitchenObject(cutKitchenObjectSO, this);
        } 
    }
}
