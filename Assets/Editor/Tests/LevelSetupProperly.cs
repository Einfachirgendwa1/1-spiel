using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEditor.SceneManagement;
using UnityEngine;
using UnityEngine.TestTools;
using Validation;
using Scene = UnityEngine.SceneManagement.Scene;

namespace Editor.Tests {
    public class LevelSetupProperly {
        [UnityTest]
        public IEnumerator ValidateEverything() {
            Debug.Log("Beginning validation.");

            foreach (EditorBuildSettingsScene sceneSettings in EditorBuildSettings.scenes) {
                if (!sceneSettings.enabled) continue;

                Debug.Log("Loading scene " + sceneSettings.path);
                Scene scene = EditorSceneManager.OpenScene(sceneSettings.path, OpenSceneMode.Single);

                foreach (GameObject go in AllChildrenInScene(scene)) {
                    ValidateGameObject(go);
                }
            }

            Debug.Log("Validation done.");

            yield return null;
        }

        private static void ValidateGameObject(GameObject go) {
            foreach (IValidate validate in go.GetComponents<IValidate>()) {
                validate.Validate();
            }

            foreach (IValidateMultiple validate in go.GetComponents<IValidateMultiple>()) {
                validate.Validate(ValidateGameObject);
            }
        }

        private static IEnumerable<GameObject> AllChildrenInScene(Scene scene) {
            foreach (GameObject root in scene.GetRootGameObjects()) {
                yield return root;

                Stack<Transform> stack = new();
                stack.Push(root.transform);

                while (stack.Count > 0) {
                    Transform current = stack.Pop();
                    foreach (Transform child in current) {
                        yield return child.gameObject;
                        stack.Push(child);
                    }
                }
            }
        }
    }
}