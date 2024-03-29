﻿using UnityEngine;

namespace Code.Bullets
{
    public class Bullet : MonoBehaviour
    {
        [SerializeField] private Rigidbody2D _rb;
        protected float speed;
        
        protected void SpawnBullet(Vector3 position, Vector3 forward)
        {
            transform.position = position;
            _rb.velocity = forward * speed;
        }

        public void MoveTo(Vector3 to)
        {

        }
    }
}