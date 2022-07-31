using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gardenbed: MonoBehaviour
{
    [SerializeField] private Transform _gardenBed;
    [SerializeField] private Crops _crops;


    public Crops CurrentCrops;
    private Player currentPlayer;

    private Coroutine harvestingRoutine;
    private Coroutine growingRoutine;

    private void Planting(Crops prefab)
    {

    }
    private Crops CreateCrops(Crops prefab,Vector3 position, Transform parent)
    {
        var  crop = Instantiate(prefab, parent);
        crop.transform.localPosition = position;
        return crop;
    }

}
