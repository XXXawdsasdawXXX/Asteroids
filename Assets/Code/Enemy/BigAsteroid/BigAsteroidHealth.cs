﻿using System;
using Code.Data;
using Code.Stats;
using UnityEngine;
using Zenject;

namespace Code.Enemy
{
    public class BigAsteroidHealth : MonoBehaviour, IHealth
    {
        [SerializeField] private BigAsteroid _asteroid;
        private byte _currentHP;
        private byte _maxHP;

        public byte Current => _currentHP;
        public byte Max => _maxHP;

        public event Action OnStatChanged;
        public Action<BigAsteroid> OnDeath;

        [Inject]
        private void Construct(GameConfig config)
        {
            _maxHP = config.bigAsteroidMaxHP;
            _currentHP = _maxHP;
        }

        public void TakeDamage()
        {
            if (_currentHP <= 0)
                return;
            
            _currentHP--;
            OnStatChanged?.Invoke();

            if (Current > 0) 
                return;

            OnDeath?.Invoke(_asteroid);
            Debug.Log("Despawn " + gameObject.name);
            _asteroid.Despawn();
        }
    }
}