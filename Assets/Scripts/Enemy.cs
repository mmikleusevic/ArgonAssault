using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering.HighDefinition;

public class Enemy : MonoBehaviour
{
    [SerializeField] GameObject _deathFX;
    [SerializeField] GameObject _hitVFX;
    [SerializeField] int _scorePerHit = 15;
    [SerializeField] int _hitPoints = 3;
    [SerializeField] int _damage = 1;
  
    Scoreboard _scoreboard;
    GameObject _parentGameObject;
    Effects _effects;

    readonly static string ITEMS_TO_DESTROY = "ItemsToDestroy";

    void Start()
    {
        _scoreboard = FindObjectOfType<Scoreboard>();
        _effects = Effects.Initialize();

        Rigidbody rigidBody = gameObject.AddComponent<Rigidbody>();
        rigidBody.useGravity = false;

        _parentGameObject = GameObject.FindWithTag(ITEMS_TO_DESTROY);
    }

    void OnParticleCollision(GameObject other)
    {
        ProcessHits(other);
    }

    private void OnCollisionEnter(Collision collision)
    {
        KillEnemy();
    }

    void ProcessHits(GameObject other)
    {
        _hitPoints = _hitPoints - _damage;

        ProcessEnemyHit(other);

        if (_hitPoints <= 0)
        {
            ProcessScore();
            KillEnemy();
        }
    }

    void ProcessEnemyHit(GameObject other)
    {
        Vector3 locationOfHit = transform.position;

        if (other.gameObject.GetComponent<ParticleSystem>() != null)
        {
            ParticleSystem laserParticles = other.gameObject.GetComponent<ParticleSystem>();

            List<ParticleCollisionEvent> particleCollisionEvents = new List<ParticleCollisionEvent>();

            int nNumParticleCollisions = laserParticles.GetCollisionEvents(this.gameObject, particleCollisionEvents);

            if(nNumParticleCollisions > 0)
            {
                locationOfHit = particleCollisionEvents[0].intersection;
            }
        }

        _effects.ProcessFX(_hitVFX, locationOfHit, _parentGameObject);
    }

    void KillEnemy()
    {
        _effects.ProcessFX(_deathFX, transform.position, _parentGameObject);
        Destroy(gameObject);
    }

    void ProcessScore()
    {
        _scoreboard.UpdateScore(_scorePerHit);
    }
}
