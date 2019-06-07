using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

[CustomEditor (typeof (EnemyCacScript))]
public class EnemyCacFovEditor : Editor {

    private void OnSceneGUI()
    {
        EnemyCacScript enemyFow = (EnemyCacScript)target;
        Handles.color = Color.white;
        Handles.DrawWireArc(enemyFow.transform.position, Vector3.up, Vector3.forward, 360, enemyFow.fovViewRadius);

    }


}


