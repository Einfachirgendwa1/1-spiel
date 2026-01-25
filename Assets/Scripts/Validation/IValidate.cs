using System;
using UnityEngine;

namespace Validation {
    public interface IValidate {
        void Validate();
    }

    public interface IValidateMultiple {
        void Validate(Action<GameObject> validateAsWell);
    }
}