using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class UnityExtension {

    public static void SetParent(this Transform transform, Transform parent, Vector3 localPos, Quaternion localRot, Vector3 localSca)
    {
        transform.SetParent(parent);
        transform.localPosition = localPos;
        transform.localRotation = localRot;
        transform.localScale = localSca;
    }
}
