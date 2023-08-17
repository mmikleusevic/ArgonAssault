using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    [SerializeField] GameObject _deathVFX;
    [SerializeField] GameObject _hitVFX;
    [SerializeField] int _scorePerHit = 15;
    [SerializeField] int _hitPoints = 3;
    [SerializeField] int _damage = 1;
  
    readonly static string ITEMS_TO_DESTROY = "ItemsToDestroy";

    Scoreboard _scoreboard;
    GameObject _parentGameObject;

    void Start()
    {
        _scoreboard = FindObjectOfType<Scoreboard>();

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

        ProcessVFX(_hitVFX, locationOfHit);
    }

    void KillEnemy()
    {
        ProcessVFX(_deathVFX, transform.position);
        Destroy(gameObject);
    }

    void ProcessScore()
    {
        _scoreboard.UpdateScore(_scorePerHit);
    }

    void ProcessVFX(GameObject visualEffect, Vector3 position)
    {
        GameObject vfx = Instantiate(visualEffect, position, Quaternion.identity);
        vfx.transform.parent = _parentGameObject.transform;

    }
}
