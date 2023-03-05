using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlatesCounter : BaseCounter {

    public event EventHandler OnPlateSpawned;

    [SerializeField] private KitchenObjectSO kitchenObjectSO;

    private float spawnPlatesTimer;
    private float spawnPlatesDelay = 4f;
    private int platesSpawnedAmount;
    private int platesSpawnedAmountMax = 4;

    private void Update() {
        spawnPlatesTimer += Time.deltaTime;

        if (spawnPlatesTimer > spawnPlatesDelay) {
            spawnPlatesTimer = 0f;

            if (platesSpawnedAmount < platesSpawnedAmountMax) {
                platesSpawnedAmount++;

                OnPlateSpawned?.Invoke(this, EventArgs.Empty);
            
            }
        }
    }

}