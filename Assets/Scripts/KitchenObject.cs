using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KitchenObject : MonoBehaviour
{
    [SerializeField] KitchenObjectSO kithcenObjectSO;

    public KitchenObjectSO GetKithcenObjectSO() {
        return kithcenObjectSO;
    }
}
