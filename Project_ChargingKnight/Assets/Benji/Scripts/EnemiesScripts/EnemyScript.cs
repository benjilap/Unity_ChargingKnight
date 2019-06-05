using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyScript : MonoBehaviour {

    public float fovViewRadius;
    [SerializeField,Range(0, 179)]
    float fovViewAngle;

    [SerializeField]
    float timeOfPatrol = 1;
    [SerializeField]
    float distOfPatrol = 1;
    Vector3 enemyPatrolNextPos;
    Vector3 randomNextDir;
    bool inMove;


    public LayerMask targetMask;
    public LayerMask obstacleMask;
    
    protected Rigidbody enemyRb;
    protected NavMeshAgent enemyNavAgent;

    [HideInInspector]
    public List<PlayerClass> targetsList = new List<PlayerClass>();

    Vector3 enemyTempDir = Vector3.forward;
    float[] angleLimit = new float[2];
    float timeSaveFov;

    protected void InitLayerMask()
    {
        if (this.gameObject.layer != LayerMask.NameToLayer(this.name))
        {
            for (int i = 0; i < 32; i++)
            {

                if (LayerMask.LayerToName(i) != "")
                {
                    if (LayerMask.LayerToName(i) == this.gameObject.name)
                    {
                        targetMask.value |= 0 << i;
                        this.gameObject.layer = i;

                    }
                    else if (LayerMask.LayerToName(i) == "MinimapRender" ) 
                    {
                        targetMask.value |= 0 << i;
                        obstacleMask.value |= 0 << i;
                    }
                    else
                    {
                        targetMask.value |= 1 << i;
                        obstacleMask.value |= 1 << i;
                    }
                }
            }
            for (int i = 0; i < this.transform.childCount; i++)
            {
                if (this.transform.GetChild(i).gameObject.layer == LayerMask.NameToLayer("Default"))
                {
                    this.transform.GetChild(i).gameObject.layer = this.gameObject.layer;
                }
            }
        }
    }

    protected void FovRadar()
    {
        if (enemyRb.velocity.magnitude > 0)
        {
            enemyTempDir = enemyNavAgent.velocity.normalized;
        }
        bool canswitch=false;
        float angleLerpTimer = (Time.time - timeSaveFov);
        float angleRayDir = Mathf.LerpAngle(Vector3.Angle(enemyTempDir, Vector3.forward) + angleLimit[0], Vector3.Angle(enemyTempDir, Vector3.forward) + angleLimit[1],angleLerpTimer);
        UpdateAngleLimit();
        Vector3 FovRayDir = new Vector3(Mathf.Sin( angleRayDir * Mathf.Deg2Rad), 0, Mathf.Cos(angleRayDir * Mathf.Deg2Rad));
        Vector3 angleMaxLimit = this.transform.position + new Vector3(Mathf.Sin((Vector3.Angle(enemyTempDir, Vector3.forward) + angleLimit[0]) * Mathf.Deg2Rad), 0, Mathf.Cos((Vector3.Angle(enemyTempDir, Vector3.forward) + angleLimit[0]) * Mathf.Deg2Rad)) * fovViewRadius;
        Vector3 angleMinLimit = this.transform.position + new Vector3(Mathf.Sin((Vector3.Angle(enemyTempDir, Vector3.forward) + angleLimit[1]) * Mathf.Deg2Rad), 0, Mathf.Cos((Vector3.Angle(enemyTempDir, Vector3.forward) + angleLimit[1]) * Mathf.Deg2Rad)) * fovViewRadius;

        Debug.DrawLine(this.transform.position, this.transform.position + FovRayDir * fovViewRadius, Color.green);
        Debug.DrawLine(this.transform.position, angleMaxLimit, Color.blue);
        Debug.DrawLine(this.transform.position, angleMinLimit, Color.blue);


        Ray rayFov = new Ray(this.transform.position, FovRayDir);
        RaycastHit hit;
        if (angleLerpTimer >= 1)
        {
            canswitch = true;
        }
        if (Vector3.Distance(this.transform.position + FovRayDir * fovViewRadius, angleMinLimit) <= 0.05f)
        {
            if (canswitch)
            {

                angleLimit[0] *= -1;
                angleLimit[1] *= -1;
                timeSaveFov = Time.time;
                canswitch = false;
            }
        }
        if (Physics.Raycast(rayFov, out hit, fovViewRadius, targetMask))
        {
            Debug.Log(hit.collider.name);
            if (hit.collider.GetComponent<PlayerClass>() && !targetsList.Contains(hit.collider.GetComponent<PlayerClass>())) 
            {
                targetsList.Add(hit.collider.GetComponent<PlayerClass>());
            }
        }

    }

    protected void InitVar()
    {
        enemyRb = this.GetComponent<Rigidbody>();
        enemyNavAgent = this.GetComponent<NavMeshAgent>();
        enemyPatrolNextPos = this.transform.position;
    }

    protected void UpdateAngleLimit()
    {
        angleLimit[0] =  fovViewAngle / 2;
        angleLimit[1] = -fovViewAngle / 2;

    }

    protected void EnemyPatrolling()
    {

        if (Vector3.Distance(this.transform.position, enemyPatrolNextPos) <= 0.19f)
        {
            Debug.Log("0");
            StartCoroutine(PatrolNextPosLoop());
            Ray rayDir = new Ray(this.transform.position, randomNextDir);
            Ray rayDown = new Ray(this.transform.position+randomNextDir*distOfPatrol, Vector3.down);

            RaycastHit hit;
            if (!Physics.Raycast(rayDir, out hit, distOfPatrol, obstacleMask) && Physics.Raycast(rayDown, out hit, distOfPatrol, obstacleMask))
            {

                enemyPatrolNextPos = this.transform.position + randomNextDir * distOfPatrol;

            }
            else
            {
                enemyPatrolNextPos = this.transform.position + randomNextDir * -distOfPatrol;
            }
        }
        else
        {
            Debug.Log("1");

            if (!inMove)
            {
                inMove = true;
                enemyNavAgent.SetDestination(enemyPatrolNextPos);
            }
        }
    }

    IEnumerator PatrolNextPosLoop()
    {
        yield return new WaitForSeconds(timeOfPatrol);
        inMove = false;
        randomNextDir = new Vector3(Random.Range(-1.0f, 1.0f), 0, Random.Range(-1.0f, 1.0f)).normalized;

    }
}
