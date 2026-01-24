using UnityEngine;

namespace Enemies {
    public class EnemySpawning : MonoBehaviour {
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
    }
}