using Gun;
using TMPro;
using UnityEngine;

namespace UI {
    public class AmmoCounter : MonoBehaviour {
        private Guns player;
        private TextMeshProUGUI text;

        private void Start() {
            text = GetComponent<TextMeshProUGUI>();
            player = GameObject.Find("Player").GetComponent<Guns>();
            if (player == null) {
                Debug.LogError("Player not found");
            }
        }

        private void Update() {
            if (player == null) {
                return;
            }

            text.text = $"{player.CurrentGun.Ammo}/{player.CurrentGun.magazineSize}";
        }
    }
}