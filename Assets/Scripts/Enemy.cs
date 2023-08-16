using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    int _health = 100;
    int _damage = 34;
    private void OnParticleCollision(GameObject other)
    {
        _health = _health - _damage;
        Debug.Log($"Enemy has {_health}");
        if(_health <= 0)
        {
            Destroy(gameObject);
        }
    }
}
