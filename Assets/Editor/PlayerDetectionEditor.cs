using Enemies;
using UnityEditor;
using UnityEngine;

namespace Editor {
    [CustomEditor(typeof(EnemyPlayerDetection))]
    public class PlayerDetectionEditor : UnityEditor.Editor {
        private void OnSceneGUI() {
            EnemyPlayerDetection fov = (EnemyPlayerDetection)target;
            Handles.color = Color.white;
            Handles.DrawWireArc(fov.transform.position, Vector3.up, Vector3.forward, 360, fov.radius);

            Vector3 viewAngle01 = DirectionFromAngle(fov.transform.eulerAngles.y, -fov.angle / 2);
            Vector3 viewAngle02 = DirectionFromAngle(fov.transform.eulerAngles.y, fov.angle / 2);

            Handles.color = Color.yellow;
            Handles.DrawLine(fov.transform.position, fov.transform.position + viewAngle01 * fov.radius);
            Handles.DrawLine(fov.transform.position, fov.transform.position + viewAngle02 * fov.radius);

            if (fov.State != EnemyState.Attacking) {
                return;
            }

            Handles.color = Color.green;
            Handles.DrawLine(fov.transform.position, fov.Player.transform.position);
        }

        private static Vector3 DirectionFromAngle(float eulerY, float angleInDegrees) {
            angleInDegrees += eulerY;

            return new Vector3(Mathf.Sin(angleInDegrees * Mathf.Deg2Rad), 0, Mathf.Cos(angleInDegrees * Mathf.Deg2Rad));
        }
    }
}