using System;
using UnityEngine;

namespace Validation {
    public interface IValidateMultiple {
        void Validate(Action<GameObject> validateAsWell);
    }
}