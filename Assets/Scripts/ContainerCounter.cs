using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class ContainerCounter : BaseCounter
{

    public event EventHandler OnPlayerGrabbedObject;

    [SerializeField] private KitchenObjectSO kitchenObjectSO;

    public override void Interact(Player player) {
        if (!HasKithcenObject()) {
            if (!player.HasKithcenObject()) {
                //Player has nothing
                KitchenObject.SpawnKitchenObject(kitchenObjectSO, player);

                OnPlayerGrabbedObject?.Invoke(this, EventArgs.Empty);
            }
            
        }


    }

    
}
