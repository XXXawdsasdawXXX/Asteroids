﻿using System;
using Code.Data;
using Code.Enemy.Aliens;
using Code.Enemy.BigAsteroids;
using Code.Enemy.SmallAsteroids;
using UnityEngine;
using Zenject;

namespace Code.Infrastructure.Factory
{
    public class EnemiesFactory : ITickable
    {
        private readonly BigAsteroid.Pool _bigAsteroidPool;
        private readonly SmallAsteroid.Pool _smallAsteroidPool;
        private readonly Alien.Pool _aliensPool;
        
        private readonly float _asteroidsSpawnCooldown;
        private float _currentAsteroidsCooldown;
        private readonly int _createSmallAsteroid;
        
        private readonly float _aliensSpawnCooldown;
        private float _currentAliensCooldown;

        public Action OnDeathAliens;
        public Action<float> OnUpdateAliensCooldown;

        private EnemiesFactory(BigAsteroid.Pool bigAsteroidPool,
            SmallAsteroid.Pool smallAsteroidPool,
            Alien.Pool aliensPool,
            GameConfig config) 
        {
            _bigAsteroidPool = bigAsteroidPool;
            _smallAsteroidPool = smallAsteroidPool;
            _aliensPool = aliensPool;
            
            _asteroidsSpawnCooldown = config.asteroidsSpawnCooldown;
            _currentAsteroidsCooldown = _asteroidsSpawnCooldown;
            _createSmallAsteroid = config.createSmallAsteroid;

            _aliensSpawnCooldown = config.aliensSpawnCooldown;
            _currentAliensCooldown = _aliensSpawnCooldown;
            
        }

        public void Tick()
        {
            AsteroidsSpawnCycle();
            AliensSpawnCycle();
        }
        

        private void AsteroidsSpawnCycle()
        {
            if (CooldownIsUp(_currentAsteroidsCooldown))
            {
                SpawnBigAsteroid();
                _currentAsteroidsCooldown = _asteroidsSpawnCooldown;
            }
            else
                UpdateCooldown(ref _currentAsteroidsCooldown);
        }

        private void AliensSpawnCycle()
        {
            if (CooldownIsUp(_currentAliensCooldown))
            {
                SpawnAliens();
                _currentAliensCooldown = _aliensSpawnCooldown;
            }
            else
            {
                UpdateCooldown(ref _currentAliensCooldown);
                OnUpdateAliensCooldown?.Invoke(_currentAliensCooldown);
            }
        }
        
        
        private void UpdateCooldown(ref  float time) =>
            time -= Time.deltaTime;

        private bool CooldownIsUp(float time) =>
            time <= 0;

        private void SpawnBigAsteroid()
        {
            var enemy = _bigAsteroidPool.Spawn();
            enemy.hp.SetActionOnDeath(SpawnSmallAsteroids);
        }

        private void SpawnSmallAsteroids(Transform bigAsteroid)
        {
            for (byte i = 0; i < _createSmallAsteroid; i++)
            {
                _smallAsteroidPool.Spawn(bigAsteroid.position);
            }
        }

        private void SpawnAliens()
        {
            _aliensPool.Spawn();
        }

        public void DeSpawnAlien(Alien alien)
        {
            _aliensPool.Despawn(alien);
            OnDeathAliens?.Invoke();
        }
    }
}