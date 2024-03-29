﻿using Code.Data;
using Code.Interfaces;
using UnityEngine;
using Zenject;

namespace Code.Enemy.SmallAsteroids
{
    public class SmallAsteroid : MonoBehaviour,IEnemy, IDeSpawner
    {
        [SerializeField] private Rigidbody2D _rb;
        private float _speed;
        private Pool _pool;

        [Inject]
        public void Construct(Pool pool, GameConfig config)
        {
            _pool = pool;
            _speed = config.smallAsteroidSpeed;
        }

        public void DeSpawn() => 
            _pool.Despawn(this);

        public void SetPosition(Vector3 position) => 
            transform.position = position;

        public void Move()
        {
            Vector3 randomVector = new Vector3(Random.Range(-_speed, _speed),Random.Range(-_speed, _speed),0);
            _rb.AddForce(randomVector , ForceMode2D.Impulse);
        }

        public class Pool : MonoMemoryPool<Vector3,SmallAsteroid>
        {
            protected override void Reinitialize(Vector3 position,SmallAsteroid asteroid)
            {
                asteroid.SetPosition(position);
                asteroid.Move();
            }
        }
    }
}