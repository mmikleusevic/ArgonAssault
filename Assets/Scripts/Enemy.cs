using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    [SerializeField] GameObject _deathVFX;
    [SerializeField] Transform _parent;
    [SerializeField] int _points = 15;
    Scoreboard _scoreboard;

    int _health = 100;
    int _damage = 34;

    private void Start()
    {
        _scoreboard = FindObjectOfType<Scoreboard>();
    }

    private void OnParticleCollision(GameObject other)
    {
        _health = _health - _damage;
        Debug.Log($"Enemy has {_health}");
        if(_health <= 0)
        {
            GameObject vfx = Instantiate(_deathVFX, transform.position, Quaternion.identity);
            vfx.transform.parent = _parent;
            _scoreboard.UpdateScore(_points);
            Destroy(gameObject);
        }
    }
}
