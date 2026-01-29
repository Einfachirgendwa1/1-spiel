using System;
using NUnit.Framework;
using UnityEngine;
using Validation;

namespace Enemies {
    public class EnemySpawner : MonoBehaviour, IValidateMultiple {
        [NonNull] public GameObject enemyPrefab;

        private void Start() {
            foreach (Transform child in transform) {
                GameObject enemyInstance = Instantiate(enemyPrefab, child.position, Quaternion.identity);

                Patrolling patrolling = enemyInstance.GetComponent<Patrolling>();
                if (patrolling is null) {
                    Debug.LogWarning("EnemySpawning: No Patrolling component found on enemy instance.");
                    continue;
                }

                foreach (Transform waypoint in child) {
                    patrolling.PatrollingPath.Add(waypoint.position);
                }
            }
        }

        public void Validate(Action<GameObject> validateAsWell) {
            GameObject instance = Instantiate(enemyPrefab);

            validateAsWell(instance);
            Assert.IsNotNull(instance.GetComponent<Patrolling>(), "instance.GetComponent<Patrolling>() != null");

            DestroyImmediate(instance);
        }
    }
}