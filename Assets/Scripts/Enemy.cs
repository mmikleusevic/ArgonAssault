using UnityEngine;

public class Enemy : MonoBehaviour
{
    [SerializeField] GameObject _deathVFX;
    [SerializeField] Transform _parent;
    [SerializeField] int _points = 15;
    [SerializeField] int _health = 100;
    [SerializeField] int _damage = 34;

    Scoreboard _scoreboard;


    private void Start()
    {
        _scoreboard = FindObjectOfType<Scoreboard>();
    }

    private void OnParticleCollision(GameObject other)
    {
        ProcessHits();
    }

    private void ProcessHits()
    {
        _health = _health - _damage;
        Debug.Log($"Enemy has {_health}");
        if (_health <= 0)
        {
            KillEnemy();
            ProcessDeath();
        }
    }

    private void KillEnemy()
    {
        GameObject vfx = Instantiate(_deathVFX, transform.position, Quaternion.identity);
        vfx.transform.parent = _parent;
        Destroy(gameObject);
    }

    private void ProcessDeath()
    {
        _scoreboard.UpdateScore(_points);
    }
}
