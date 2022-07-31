using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CropsMeshController : MonoBehaviour
{
    

    [SerializeField] private ParticleSystem _leavesEffects;

    public void SetGrassSize(float sizeY)
    {
        transform.localScale = new Vector3(1, sizeY, 1);
    }
    public void DropLeaves()
    {
        _leavesEffects.Play();
    }
}
