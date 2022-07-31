using UnityEngine;
using CustomTools;
[CreateAssetMenu(menuName = "Pools/CropsStackPool")]
public class CropsStackPool : ScriptableObject
{
    [SerializeField] private CropsStack _cropsStackPrefab;
    [SerializeField] private int _count = 1;
    [SerializeField] private bool _autoExpand = true;

    private Pool<CropsStack> cropsPool;

    public void InitCropsStackPool(Transform parent)
    {
        cropsPool = new Pool<CropsStack>(_cropsStackPrefab, _count, parent, _autoExpand);
    }
    public CropsStack GetCropsStack(Vector3 position)
    {
        var go = cropsPool.GetFreeObject();
        go.transform.position = position;
        go.transform.rotation = Quaternion.identity;
        return go;
    }
}
