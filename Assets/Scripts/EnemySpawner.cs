using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Pool;

public class EnemySpawner : MonoBehaviour
{
    [SerializeField] private Timer _timer;
    [SerializeField] private Enemy _enemyPrefab;
    [SerializeField] private List<Transform> _spawnPositions;
    [SerializeField] private Transform _target;
    [SerializeField] private int _defaultPoolCapacity = 40;
    [SerializeField] private int _maxPoolSize = 100;

    private ObjectPool<Enemy> _zombiesPool;

    private void OnEnable()
    {
        _timer.TimeChanged += _zombiesPool.Get;
    }

    private void OnDisable()
    {
        _timer.TimeChanged -= _zombiesPool.Get;
    }

    private void Awake()
    {
        _zombiesPool = new ObjectPool<Enemy>(
            createFunc: () => Instantiate(_enemyPrefab, GetRandomSpawnPosition(), Quaternion.Euler(0, 0, 0)),
            actionOnGet: (enemy) => Get(enemy),
            actionOnRelease: (enemy) => enemy.gameObject.SetActive(false),
            actionOnDestroy: (enemy) => Destroy(enemy.gameObject),
            collectionCheck: false,
            defaultCapacity: _defaultPoolCapacity,
            maxSize: _maxPoolSize
            );
    }

    private void Get(Enemy enemy)
    {
        enemy.gameObject.SetActive(true);
        enemy.transform.position = GetRandomSpawnPosition();
        enemy.SetDirection(Vector3.forward, _target);
    }

    public void ReleaseEnemy(Enemy enemy) => _zombiesPool.Release(enemy);

    private Vector3 GetRandomSpawnPosition() => _spawnPositions[Random.Range(0, _spawnPositions.Count)].position;
}
