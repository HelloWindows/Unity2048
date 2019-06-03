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

    public static Vector3 ToVector3(this NodeMoveDirection dir)
    {
        switch (dir)
        {
            case NodeMoveDirection.Up: return Vector3.up;
            case NodeMoveDirection.Down: return Vector3.down;
            case NodeMoveDirection.Left: return Vector3.left;
            case NodeMoveDirection.Right: return Vector3.right;
        } // end switch
        return Vector3.zero;
    }
}
