using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(EnemyDistScript))]
public class EnemyDistFovEditor : Editor
{

    private void OnSceneGUI()
    {
        EnemyDistScript enemyFow = (EnemyDistScript)target;
        Handles.color = Color.white;
        Handles.DrawWireArc(enemyFow.transform.position, Vector3.up, Vector3.forward, 360, enemyFow.fovViewRadius);
        foreach (PlayerClass target in enemyFow.targetsList)
        {
            Vector3 targetDir = target.transform.position - enemyFow.transform.position;
            Handles.DrawLine(enemyFow.transform.position, targetDir * Vector3.Distance(target.transform.position, enemyFow.transform.position));
        }
    }

}
