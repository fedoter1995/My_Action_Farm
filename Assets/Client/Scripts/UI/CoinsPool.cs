using UnityEngine;

public class CoinsPool : MonoBehaviour
{
    [SerializeField] CoinUI _coinPrefab;
    [SerializeField] int _coinsCount;
    private CustomTools.Pool<CoinUI> coins;
    void Awake()
    {
        Initialize();
    }
    
    public void GetCoin(object sender, object seller, int price)
    {
        var coin = SpawnCoin(transform.position);
        coin.DropCoin(sender, seller, price);
    }

    private CoinUI SpawnCoin(Vector3 position)
    {
        var coin = coins.GetFreeObject();
        coin.transform.position = position;
        return coin;
    }
    private void Initialize()
    {
        coins = new CustomTools.Pool<CoinUI>(_coinPrefab, _coinsCount, transform, true);

        var barns = FindObjectsOfType<Barn>();
        foreach (Barn barn in barns)
            barn.BarterEvent += GetCoin;
    }
}
