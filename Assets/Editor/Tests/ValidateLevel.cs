using System;
using System.Collections;
using System.Collections.Generic;
using System.Reflection;
using NUnit.Framework;
using UnityEditor;
using UnityEditor.SceneManagement;
using UnityEngine;
using UnityEngine.TestTools;
using Validation;
using static System.Reflection.BindingFlags;
using Scene = UnityEngine.SceneManagement.Scene;

namespace Editor.Tests {
    public class ValidateLevel {
        [UnityTest]
        public IEnumerator ValidateEverything() {
            foreach (EditorBuildSettingsScene sceneSettings in EditorBuildSettings.scenes) {
                if (!sceneSettings.enabled) continue;

                Debug.Log($"Loading scene {sceneSettings.path}");
                Scene scene = EditorSceneManager.OpenScene(sceneSettings.path, OpenSceneMode.Single);

                foreach (GameObject go in AllChildrenInScene(scene)) {
                    ValidateGameObject(go);
                }
            }

            yield return null;
        }

        private static void ValidateGameObject(GameObject go) {
            foreach (Component component in go.GetComponents<Component>()) {
                CheckAttrs(component);
            }

            foreach (IValidate validate in go.GetComponents<IValidate>()) {
                validate.Validate();
            }

            foreach (IValidateMultiple validate in go.GetComponents<IValidateMultiple>()) {
                validate.Validate(ValidateGameObject);
            }
        }

        private static void CheckAttrs(Component component) {
            Type type = component.GetType();
            foreach (FieldInfo fieldInfo in type.GetFields(Instance | NonPublic | Public)) {
                if (fieldInfo.GetCustomAttribute<Validate>() is { } validateAttr) {
                    object value = fieldInfo.GetValue(component);
                    Assert.IsTrue(validateAttr.Condition(value), validateAttr.Message(fieldInfo));
                }
            }
        }

        private static IEnumerable<GameObject> AllChildrenInScene(Scene scene) {
            foreach (GameObject root in scene.GetRootGameObjects()) {
                yield return root;

                Stack<Transform> stack = new();
                stack.Push(root.transform);

                while (stack.Count > 0) {
                    foreach (Transform child in stack.Pop()) {
                        yield return child.gameObject;
                        stack.Push(child);
                    }
                }
            }
        }
    }
}