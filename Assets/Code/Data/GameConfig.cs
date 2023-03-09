﻿using UnityEngine;

namespace Code.Data
{
    [CreateAssetMenu(fileName = "GameConfig", menuName = "ScriptableObjects/Data/GameConfig")]
    public class GameConfig : ScriptableObject
    {
        [Header("Player")]
        [Range(0.5f, 5)] public float playerSpeed = 1;
        [Range(1, 10)] public float playerMaxSpeed = 2.8f;
        [Range(1, 5)] public byte playerMaxHP = 3;

        
        [Header("Fuel")]
        public float maxFuel = 10;
        public float fuelForMove = 0.2f;
        public float fuelFromEnemy;
        
        [Header("Bullet")]
        [Range(1, 10)] public float playerBulletSpeed = 9;
        
        
        [Header("Enemy")]
        [Range(0.5f, 5)] public float enemySpeed = 2;
        [Range(2, 5)] public float enemySpawnCooldown = 5;

    }
}