using Gun;
using TMPro;
using UnityEngine;

namespace UI {
    public class AmmoCounter : MonoBehaviour {
        public GunController gunController;
        public TextMeshProUGUI text;

        private void Update() {
            text.text = $"{gunController.CurrentGun.Ammo}/{gunController.CurrentGun.magazineSize}";
        }
    }
}