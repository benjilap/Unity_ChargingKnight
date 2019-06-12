using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyScript : MonoBehaviour {


    //FovValue
    [SerializeField]
    float currentDirAngle;
    float[] angleLimit = new float[2];
    float timeSaveFov;
    public float fovViewRadius;
    [SerializeField,Range(0, 179)]
    float fovViewAngle;

    //PatrolValue
    [SerializeField]
    float timeOfPatrol = 1;
    [SerializeField]
    float distOfPatrol = 1;
    Vector3 enemyPatrolNextPos;
    Vector3 randomNextDir;
    bool inMove;
    float saveTimePatrolStuck;
    

    protected LayerMask targetMask;
    protected LayerMask obstacleMask;
    protected Rigidbody enemyRb;
    protected NavMeshAgent enemyNavAgent;
    protected int enemyDirFaced;

    [HideInInspector]
    public List<PlayerClass> targetsList = new List<PlayerClass>();

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
        UpdateAngleLimit();
        if (enemyNavAgent.velocity.magnitude > 0)
        {
           Vector3 enemyTempDir = enemyNavAgent.velocity.normalized;
            currentDirAngle = AngleTo(Vector3.forward, enemyTempDir);

        }
        bool canswitch=false;
        float angleLerpTimer = (Time.time - timeSaveFov)*5f;
        float angleRayDir = Mathf.LerpAngle(currentDirAngle + angleLimit[0], currentDirAngle + angleLimit[1],angleLerpTimer);
        UpdateAngleLimit();
        Vector3 FovRayDir = new Vector3(Mathf.Sin( angleRayDir * Mathf.Deg2Rad), 0, Mathf.Cos(angleRayDir * Mathf.Deg2Rad));
        Vector3 angleMaxLimit = this.transform.position + new Vector3(Mathf.Sin((currentDirAngle + angleLimit[0]) * Mathf.Deg2Rad), 0, Mathf.Cos((currentDirAngle + angleLimit[0]) * Mathf.Deg2Rad)) * fovViewRadius;
        Vector3 angleMinLimit = this.transform.position + new Vector3(Mathf.Sin((currentDirAngle + angleLimit[1]) * Mathf.Deg2Rad), 0, Mathf.Cos((currentDirAngle + angleLimit[1]) * Mathf.Deg2Rad)) * fovViewRadius;

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

            if (hit.collider.GetComponent<PlayerClass>() && !targetsList.Contains(hit.collider.GetComponent<PlayerClass>())) 
            {
                targetsList.Add(hit.collider.GetComponent<PlayerClass>());
            }
        }

    }

    private float AngleTo(Vector3 pos, Vector3 target)
    {
        float angle = 0;

        if (target.x > pos.x)
            angle = Vector3.Angle(target, pos);
        else
            angle = 360-Vector3.Angle(target, pos);

        return angle;
    }

    protected void InitVar()
    {
        enemyRb = this.GetComponent<Rigidbody>();
        enemyNavAgent = this.GetComponent<NavMeshAgent>();
        enemyPatrolNextPos = this.transform.position;
    }

    void UpdateAngleLimit()
    {
        angleLimit[0] =  fovViewAngle / 2;
        angleLimit[1] = -fovViewAngle / 2;

    }

    protected void EnemyPatrolling()
    {

        if (Vector3.Distance(this.transform.position, enemyPatrolNextPos) <= 0.16f)
        {

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
            if (!inMove)
            {

                inMove = true;
                enemyNavAgent.SetDestination(enemyPatrolNextPos);
            }
            else
            {
                if (enemyNavAgent.velocity.magnitude > 0)
                {
                    saveTimePatrolStuck = Time.time;
                }
                float stuckTimer = Time.time - saveTimePatrolStuck;
                if (stuckTimer >= 0.2f)
                {
                    enemyPatrolNextPos = this.transform.position;

                }
            }
        }
    }

    IEnumerator PatrolNextPosLoop()
    {
        yield return new WaitForSeconds(timeOfPatrol);
        inMove = false;
        randomNextDir = new Vector3(Random.Range(-1.0f, 1.0f), 0, Random.Range(-1.0f, 1.0f)).normalized;

    }

    protected PlayerClass TargetSelection()
    {
        if(targetsList.Count == 1)
        {
            return targetsList[0];
        }
        else 
        {
            PlayerClass tempTarget = targetsList[0];
            for(int i = 0; i < targetsList.Count; i++)
            {
                
                if(Vector3.Distance(this.transform.position,tempTarget.transform.position)> Vector3.Distance(this.transform.position, targetsList[i].transform.position))
                {
                    tempTarget = targetsList[i];
                }
            }
            return tempTarget;
        }
    }

    public int EnemyDirFaced()
    {
        Vector3 tempDirVector = Vector3.zero;
        if (enemyNavAgent.velocity.magnitude != 0)
        {
            tempDirVector = enemyNavAgent.velocity;
        }
        else
        {
            tempDirVector = this.transform.position - TargetSelection().transform.position;
        }

        if (tempDirVector.normalized.x > 0.5f)
        {
            enemyDirFaced= 1;

            return enemyDirFaced;

        }
        else if (tempDirVector.normalized.x < -0.5f)
        {
            enemyDirFaced= 3;
            return enemyDirFaced;

        }
        else if (tempDirVector.normalized.z >= 0.5f)
        {
            enemyDirFaced= 2;
            return enemyDirFaced;

        }
        else if (tempDirVector.normalized.z <= 0.5f)
        {
            enemyDirFaced= 0;
            return enemyDirFaced;

        }
        else
        {
            return enemyDirFaced;

        }


    }
}
