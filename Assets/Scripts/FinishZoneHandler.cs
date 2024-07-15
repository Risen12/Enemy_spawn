using UnityEngine;

[RequireComponent(typeof(MeshCollider))]
public class FinishZoneHandler : MonoBehaviour
{
    [SerializeField] private EnemySpawner _enemySpawner;

    private void OnCollisionEnter(Collision collision)
    {
        Debug.Log("вижу что-то");

        if (collision.gameObject.TryGetComponent(out Enemy enemy))
        {
            Debug.Log("¬ижу зомби");
            _enemySpawner.ReleaseEnemy(enemy);
        }
    }
}
