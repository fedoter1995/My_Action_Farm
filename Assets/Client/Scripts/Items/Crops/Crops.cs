using System;
using System.Collections;
using UnityEngine;


public abstract class Crops : MonoBehaviour, ITakeDamge
{
    #region SerializeFields
    [SerializeField] protected CropsInfo _info;
    [SerializeField] protected CropsStackPool _cropsStackPool;
    [SerializeField] protected Collider _collider;
    [SerializeField] protected CropsMeshController _cropsMeshController;
    [SerializeField] protected int _cropsStackCount = 1;
    #endregion

    private Coroutine growingCoroutine;

    public event Action<int> TakeDamageEvent;

    public CropsInfo Info => _info;

    public int HealthPoints { get; private set; } = 10;

    #region Events

    #endregion
    private void Awake()
    {
        Initialize();
    }

    protected virtual void Initialize()
    {
        _cropsStackPool.InitCropsStackPool(transform);
        HealthPoints = Info.HealthPoints;
    }
    public void Harvest()
    {
        _cropsMeshController.SetGrassSize(0.03f);
         growingCoroutine = StartCoroutine(GrowingRoutine(Info.GrowingTime));

        for(int i = 0; i< _cropsStackCount; i++)
            DropCropsStack();
    }
    public IEnumerator GrowingRoutine(float growingTime)
    {
        
        _collider.enabled = false;

        float time = 0.03f;

        while (time < growingTime)
        {
            time += Time.deltaTime;
            float progress = time / growingTime;
            _cropsMeshController.SetGrassSize(progress);
            yield return null;
        }

        HealthPoints = Info.HealthPoints;
        growingCoroutine = null;

        _collider.enabled = true;
        yield break;
    }
    private void DropCropsStack()
    {
        _cropsStackPool.GetCropsStack(transform.position);
    }
    public void TakeDamage(Transform sender, int damage)
    {
        HealthPoints -= damage;
        OnTakeDamage(sender);
        if (growingCoroutine == null && HealthPoints <= 0 )
            Harvest();
    }
    private void OnTakeDamage(Transform _transform)
    {
        _cropsMeshController.DropLeaves();
    }

}
