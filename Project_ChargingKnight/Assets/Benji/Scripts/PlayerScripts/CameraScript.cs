using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraScript : MonoBehaviour {

    Vector3 camCurrentTarget;
    PlayerClass[] camTargets;

    [SerializeField]
    float camHeight = 10;
    [SerializeField]
    float camOffset = 0.5f;

	void Start () {
        this.transform.position = new Vector3(this.transform.position.x, camHeight, this.transform.position.z);
        CheckPlayer();
	}
	
	// Update is called once per frame
	void Update () {

    }

    private void FixedUpdate()
    {
        CheckPlayer();
        FollowTarget();
        
    }

    void CheckPlayer()
    {
        camTargets = GameObject.FindObjectsOfType<PlayerClass>();
        if (camTargets != null)
        {
            if (camTargets.Length == 2)
            {
                camCurrentTarget = new Vector3((camTargets[0].transform.position.x + camTargets[1].transform.position.x) / 2, camHeight, 
                                               (camTargets[0].transform.position.z + camTargets[1].transform.position.z) / 2);
            }
            else
            {
                camCurrentTarget = camTargets[0].transform.position + new Vector3(0,camHeight,0);
            }

        }
    }



    void FollowTarget()
    {
        if (camTargets != null)
        {
            //if (Vector3.Distance(this.transform.position, camCurrentTarget) > 0.5f)
            //{
                this.transform.position = Vector3.Lerp(this.transform.position, camCurrentTarget, camOffset);
            //}
        }
    }
}
