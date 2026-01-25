using System;
using NUnit.Framework;
using UnityEngine;
using Validation;

namespace Enemies {
    public class EnemySpawner : MonoBehaviour, IValidateMultiple {
        public GameObject enemyPrefab;

        private void Start() {
            foreach (Transform child in transform) {
                GameObject enemyInstance = Instantiate(enemyPrefab, child.position, Quaternion.identity);

                EnemyMovement enemyMovement = enemyInstance.GetComponent<EnemyMovement>();
                if (enemyMovement is null) {
                    Debug.LogWarning("EnemySpawning: No EnemyMovement component found on enemy instance.");
                    continue;
                }

                foreach (Transform waypoint in child) {
                    enemyMovement.PatrollingPath.Add(waypoint.position);
                }
            }
        }

        public void Validate(Action<GameObject> validateAsWell) {
            Assert.IsNotNull(enemyPrefab, "enemyPrefab != null");

            GameObject instance = Instantiate(enemyPrefab);
            Assert.IsNotNull(instance, "instance != null");

            validateAsWell(instance);
            Assert.IsNotNull(instance.GetComponent<EnemyMovement>(), "instance.GetComponent<EnemyMovement>() != null");

            DestroyImmediate(instance);
        }
    }
}