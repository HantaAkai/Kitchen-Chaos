using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CuttingCounter : BaseCounter, IHasProgress
{


    public event EventHandler<IHasProgress.OnProgressChangedEventArgs> OnProgressChanged;
    public class OnProgressChangedEventArgs : EventArgs {
        public float progressNormalised;
    }

    public event EventHandler OnCut;

    [SerializeField] private CuttingRecipeSO[] cuttingRecipeSOArray;

    private int cuttingProgres;

    public override void Interact(Player player) {
        if (!HasKithcenObject()) {
            //There is no KitchenObject here

            if (player.HasKithcenObject()) {
                //Player has something
                if (HasRecipeWithInput(player.GetKitchenObject().GetKithcenObjectSO())) { 
                    //Player has something that can be cut
                    player.GetKitchenObject().SetKitchenObjectParent(this);
                    cuttingProgres = 0;

                    CuttingRecipeSO cuttingRecipeSO = GetCuttingRecipeSOWithInput(GetKitchenObject().GetKithcenObjectSO());
                    OnProgressChanged?.Invoke(this, new IHasProgress.OnProgressChangedEventArgs {
                        progressNormalised = (float)cuttingProgres / cuttingRecipeSO.cuttingProgressMax
                    }) ;
                }
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
        if (HasKithcenObject() && HasRecipeWithInput(GetKitchenObject().GetKithcenObjectSO())) {
            //There is a KitchenObject here AND it can be cut
            cuttingProgres++;

            OnCut?.Invoke(this, EventArgs.Empty);

            CuttingRecipeSO cuttingRecipeSO = GetCuttingRecipeSOWithInput(GetKitchenObject().GetKithcenObjectSO());

            OnProgressChanged?.Invoke(this, new IHasProgress.OnProgressChangedEventArgs {
                progressNormalised = (float)cuttingProgres / cuttingRecipeSO.cuttingProgressMax
            });

            if (cuttingProgres >= cuttingRecipeSO.cuttingProgressMax) {
                KitchenObjectSO outputKitchenObjectSO = GetOutputForInput(GetKitchenObject().GetKithcenObjectSO());

                GetKitchenObject().DestroySelf();

                KitchenObject.SpawnKitchenObject(outputKitchenObjectSO, this);
            }
        } 
    }

    private bool HasRecipeWithInput(KitchenObjectSO inputKitchenObjectSO) {
        CuttingRecipeSO cuttingRecipeSO = GetCuttingRecipeSOWithInput(inputKitchenObjectSO);
        return cuttingRecipeSO != null;
    }

    private KitchenObjectSO GetOutputForInput(KitchenObjectSO inputKitchenObjectSO) {
        CuttingRecipeSO cuttingRecipeSO = GetCuttingRecipeSOWithInput(inputKitchenObjectSO);
        if (cuttingRecipeSO != null) {
            return cuttingRecipeSO.output;
        } else { 
            return null;
        }    
    }

    private CuttingRecipeSO GetCuttingRecipeSOWithInput(KitchenObjectSO inputKitchenObjectSO) {
        foreach (CuttingRecipeSO cuttingRecipeSO in cuttingRecipeSOArray) {
            if (cuttingRecipeSO.input == inputKitchenObjectSO) {
                return cuttingRecipeSO;
            }

        }
        return null;
    }
}
