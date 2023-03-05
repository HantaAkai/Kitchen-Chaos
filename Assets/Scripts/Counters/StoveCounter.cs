using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;

public class StoveCounter : BaseCounter {

    private enum State { 
        Idle,
        Frying,
        Fried,
        Burned,
    }
    
    
    [SerializeField] private FryingRecipeSO[] fryingRecipeSOArray;
    [SerializeField] private BurningRecipeSO[] burningRecipeSOArray;


    private State state;

    private float fryingTimer;
    private FryingRecipeSO fryingRecipeSO;
    private float burningTimer;
    private BurningRecipeSO burningRecipeSO;

    private void Start() {
        state = State.Idle;
    }

    private void Update() {
        if (HasKithcenObject()) {
            switch (state) { 
            case State.Idle:
                break;
            case State.Frying:
                fryingTimer += Time.deltaTime;

                if (fryingTimer > fryingRecipeSO.fryingRimerMax) {
                    //Fried

                    GetKitchenObject().DestroySelf();
                    KitchenObject.SpawnKitchenObject(fryingRecipeSO.output, this);


                    state = State.Fried;
                    burningTimer = 0f;
                        burningRecipeSO = GetBuringRecipeSOWithInput(GetKitchenObject().GetKithcenObjectSO());
                }
                break;
            case State.Fried:
                    burningTimer += Time.deltaTime;

                    if (burningTimer > burningRecipeSO.burningTimerMax) {
                        //Fried

                        GetKitchenObject().DestroySelf();
                        KitchenObject.SpawnKitchenObject(burningRecipeSO.output, this);

                        state = State.Burned;
                    }
                        break;
            case State.Burned:
                break;
        }

        }
    }



    public override void Interact(Player player) {
        if (!HasKithcenObject()) {
            //There is no KitchenObject here

            if (player.HasKithcenObject()) {
                //Player has something
                if (HasRecipeWithInput(player.GetKitchenObject().GetKithcenObjectSO())) {
                    //Player has something that can be fried
                    player.GetKitchenObject().SetKitchenObjectParent(this);
                    fryingRecipeSO = GetFryingRecipeSOWithInput(GetKitchenObject().GetKithcenObjectSO());
                    state = State.Frying;
                    fryingTimer = 0f;
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
                state = State.Idle;
            }
        }
    }


    private bool HasRecipeWithInput(KitchenObjectSO inputKitchenObjectSO) {
        FryingRecipeSO fryingRecipeSO = GetFryingRecipeSOWithInput(inputKitchenObjectSO);
        return fryingRecipeSO != null;
    }

    private KitchenObjectSO GetOutputForInput(KitchenObjectSO inputKitchenObjectSO) {
        FryingRecipeSO fryingRecipeSO = GetFryingRecipeSOWithInput(inputKitchenObjectSO);
        if (fryingRecipeSO != null) {
            return fryingRecipeSO.output;
        } else {
            return null;
        }
    }

    private FryingRecipeSO GetFryingRecipeSOWithInput(KitchenObjectSO inputKitchenObjectSO) {
        foreach (FryingRecipeSO fryingRecipeSO in fryingRecipeSOArray) {
            if (fryingRecipeSO.input == inputKitchenObjectSO) {
                return fryingRecipeSO;
            }

        }
        return null;
    }

    private BurningRecipeSO GetBuringRecipeSOWithInput(KitchenObjectSO inputKitchenObjectSO) {
        foreach (BurningRecipeSO burningRecipeSO in burningRecipeSOArray) {
            if (burningRecipeSO.input == inputKitchenObjectSO) {
                return burningRecipeSO;
            }

        }
        return null;
    }

}
