using System.Collections;
using UnityEngine;

public class BotSpawner : MonoBehaviour
{
    [SerializeField]
    private float _spawnInterval = 60f;
    [SerializeField]
    private Bot _botPrefab;
    [SerializeField]
    private float _offset;

    private Transform _player;
    private Bot _bot;

    public void Initialize(Transform player)
    {
        _player = player;
    }

    void Start()
    {
        StartCoroutine(SpawnAICar());
    }

    private IEnumerator SpawnAICar()
    {
        while (true)
        {
            yield return new WaitForSeconds(_spawnInterval);
            if (_bot == null)
            {
                var positionY = _player.transform.position.y - _offset;
                Vector3 spawnPosition = new Vector3(_player.transform.position.x, positionY, _player.transform.position.z);
                _bot = Instantiate(_botPrefab, spawnPosition, Quaternion.identity);

                _bot.Initialize(_player);
                _bot.OnBecameInvisible += Bot_OnBecameInvisible;
            }
        }
    }

    private void Bot_OnBecameInvisible()
    {
        _bot.OnBecameInvisible -= Bot_OnBecameInvisible;
        _bot = null;
    }
}
