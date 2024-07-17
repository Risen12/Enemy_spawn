using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Pool;

public class EnemySpawner : MonoBehaviour
{
    [SerializeField] private Timer _timer;
    [SerializeField] private Enemy _enemyPrefab;
    [SerializeField] private List<Transform> _spawnPositions;
    [SerializeField] private Transform _finishZone;
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
            createFunc: () => CreateEnemy(),
            actionOnGet: (enemy) => Spawn(enemy),
            actionOnRelease: (enemy) => enemy.gameObject.SetActive(false),
            actionOnDestroy: (enemy) => Destroy(enemy.gameObject),
            collectionCheck: false,
            defaultCapacity: _defaultPoolCapacity,
            maxSize: _maxPoolSize
            );
    }

    public void ReleaseEnemy(Enemy enemy) => _zombiesPool.Release(enemy);

    private void Spawn(Enemy enemy)
    {
        enemy.gameObject.SetActive(true);
        enemy.transform.position = GetRandomSpawnPosition();
        enemy.SetDirection((_finishZone.position - enemy.transform.position).normalized);
    }

    private Enemy CreateEnemy()
    {
        Vector3 spawnPosition = GetRandomSpawnPosition();

        return Instantiate(_enemyPrefab, spawnPosition, Quaternion.identity);
    }

    private Vector3 GetRandomSpawnPosition() => _spawnPositions[Random.Range(0, _spawnPositions.Count)].position;
}
