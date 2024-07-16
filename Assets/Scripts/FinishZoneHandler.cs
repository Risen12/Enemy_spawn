using UnityEngine;

[RequireComponent(typeof(MeshCollider))]
public class FinishZoneHandler : MonoBehaviour
{
    [SerializeField] private EnemySpawner _enemySpawner;

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.TryGetComponent(out Enemy enemy))
        {
            _enemySpawner.ReleaseEnemy(enemy);
        }
    }
}
