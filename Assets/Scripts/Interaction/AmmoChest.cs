using System;
using Guns;
using Guns.Ammunition;
using Player;
using UnityEngine;

namespace Interaction {
    public class AmmoChest : MonoBehaviour, IInteractable {
        private GunController gunController;

        private void Start() {
            gunController = GameObject.Find("/Player").GetComponent<PlayerGunController>();
        }

        public string Description => "pick up ammo";
        public bool CanInteract => true;

        public void Interact() {
            foreach (BulletType type in Enum.GetValues(typeof(BulletType))) {
                gunController.Ammo[type] += type.DefaultAmount();
            }

            Destroy(gameObject);
        }
    }
}