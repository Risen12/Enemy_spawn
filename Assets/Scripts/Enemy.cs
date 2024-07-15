using UnityEngine;

public class Enemy : MonoBehaviour
{
    [SerializeField] private float _speed = 3;

    private Vector3 _direction = Vector3.zero;

    public void SetDirection(Vector3 direction) => _direction = direction;

    public void SetTarget(Transform target) =>  transform.LookAt(target);

    private void Update()
    {
        if(_direction != Vector3.zero)
            transform.Translate(_direction * _speed * Time.deltaTime);
    }
}
