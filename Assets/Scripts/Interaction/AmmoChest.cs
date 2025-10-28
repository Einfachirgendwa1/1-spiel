using System;
using Guns;
using Guns.Ammunition;
using UnityEngine;

namespace Interaction {
    public class AmmoChest : MonoBehaviour, IInteractable {
        public GunController gunController;

        public string Description => "Pick up ammo";
        public bool CanInteract => true;

        public void Interact() {
            foreach (BulletType type in Enum.GetValues(typeof(BulletType))) {
                gunController.ammo[type] += type.DefaultAmount();
            }

            Destroy(gameObject);
        }
    }
}