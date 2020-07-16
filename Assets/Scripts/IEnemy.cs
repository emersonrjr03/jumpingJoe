using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IEnemy {
    void OnCollisionEnterChild(Transform source, Collision collision);

    void OnCollisionExitChild(Transform source, Collision collision);
}
