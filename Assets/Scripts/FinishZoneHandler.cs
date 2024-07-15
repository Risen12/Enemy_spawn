using UnityEngine;

[RequireComponent(typeof(MeshCollider))]
public class FinishZoneHandler : MonoBehaviour
{
    [SerializeField] private EnemySpawner _enemySpawner;

    private void OnCollisionEnter(Collision collision)
    {
        Debug.Log("���� ���-��");

        if (collision.gameObject.TryGetComponent(out Enemy enemy))
        {
            Debug.Log("���� �����");
            _enemySpawner.ReleaseEnemy(enemy);
        }
    }
}
