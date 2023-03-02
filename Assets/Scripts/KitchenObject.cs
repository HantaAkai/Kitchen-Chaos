using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KitchenObject : MonoBehaviour
{
    [SerializeField] KitchenObjectSO kithcenObjectSO;

    private ClearCounter clearCounter;

    public KitchenObjectSO GetKithcenObjectSO() {
        return kithcenObjectSO;
    }

    public void SetClearCounter(ClearCounter clearCounter) {
        if (this.clearCounter != null) { 
            this.clearCounter.ClearKitchenObject();
        }
        this.clearCounter = clearCounter;

        if (clearCounter.HasKithcenObject()) {
            Debug.LogError("This counter already has a kithcen object!");
        }

        clearCounter.SetKitchenObject(this);

        transform.parent = clearCounter.GetKitchenObjectFollowTransform();
        transform.localPosition = Vector3.zero; 
    }

    public ClearCounter GetClearCounter() {
        return clearCounter;
    }
}
