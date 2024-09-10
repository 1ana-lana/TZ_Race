using UnityEngine;

public class MagnetEffect : BonusEffect
{
    [SerializeField]
    private float _magnetRadius = 5f; 
    [SerializeField] 
    private float _attractionSpeed = 10f;
    [SerializeField]
    private LayerMask _coinLayer;
    

    void Update()
    {
        if (isBonusActive)
        {
            AttractCoins();
        }
    }

    void AttractCoins()
    {
        Collider2D[] coins = Physics2D.OverlapCircleAll(transform.position, _magnetRadius, _coinLayer);
        foreach (Collider2D coinCollider in coins)
        {
            Transform coinTransform = coinCollider.transform;
            Vector3 direction = (transform.position - coinTransform.position).normalized;
            coinTransform.position += direction * _attractionSpeed * Time.deltaTime;

            if (Vector3.Distance(transform.position, coinTransform.position) < 0.5f)
            {
                CollectCoin(coinCollider.gameObject);
            }
        }
    }

    private void CollectCoin(GameObject coin)
    {
        Destroy(coin);
    }
}
