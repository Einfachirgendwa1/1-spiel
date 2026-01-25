using NUnit.Framework;
using UnityEngine;
using Validation;

namespace Lift {
    public class Lift : MonoBehaviour, IValidate {
        [PositiveNonZero] public float delta = 0.008f;
        public float downPosY = 0.6f;
        public float upPosY = 3f;
        [PositiveNonZero] public float speed = 0.2f;

        public void Validate() {
            Assert.Less(downPosY, upPosY, "downPosY < upPosY");
        }
    }
}