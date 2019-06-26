using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoomEnemiesManager : MonoBehaviour {

    [SerializeField]
    float SpawnHeight;
    [SerializeField]
    int EnemyDistNumber;
    [SerializeField]
    int EnemyCacNumber;

    List<GameObject> listOfDistEnemy = new List<GameObject>();
    List<GameObject> listOfCacEnemy = new List<GameObject>();

    List<PlayerClass> targetsList = new List<PlayerClass>();

    Object EnemyDist;
    Object EnemyCac;

    Vector3 minAreaSpawn;
    Vector3 maxAreaSpawn;

    int enemyCacInSpawning;
    int enemyDistInSpawning;


    void Start () {
        EnemyDist = Resources.Load("Enemies/EnemyDist");
        EnemyCac = Resources.Load("Enemies/EnemyCac");
        minAreaSpawn = new Vector3(this.transform.Find("Structure/WestWall").position.x + 1.5f, 0, this.transform.Find("Structure/SouthWall").position.z + 1.5f);
        maxAreaSpawn = new Vector3(this.transform.Find("Structure/EastWall").position.x - 1.5f, 0, this.transform.Find("Structure/NorthWall").position.z - 1.5f);
    }

	void Update () {
        if (FindObjectOfType<GM_CanvasScript>() != null&&
            FindObjectOfType<GM_CanvasScript>().playersReady)
        {
            if (EnemyCacNumber != 0)
            {
                CheckEnemyCacNumbers(EnemyCac, listOfCacEnemy, EnemyCacNumber);
                CheckCacEnemiesTargets();
            }

            if (EnemyDistNumber != 0)
            {
                CheckEnemyDistNumbers(EnemyDist, listOfDistEnemy, EnemyDistNumber);
                CheckDistEnemiesTargets();
            }
        }

    }

    void CheckEnemyCacNumbers(Object EnemyPrefab,List<GameObject> listToCheck, int enemyNbrs)
    {
        if(listToCheck.Count == enemyNbrs)
        {
            enemyCacInSpawning = 0;
        }

        CleanListOfNullValue(listToCheck);
                
        int enemiesToSpawn = enemyNbrs - listToCheck.Count - enemyCacInSpawning;
        if (enemiesToSpawn !=0)
        {

            for (int i = 0; i < enemiesToSpawn; i++)
            {
                enemyCacInSpawning++;
                StartCoroutine(SpawnEnemy(EnemyPrefab, listToCheck));
            }
        }
    }


    void CheckEnemyDistNumbers(Object EnemyPrefab, List<GameObject> listToCheck, int enemyNbrs)
    {
        if (listToCheck.Count == enemyNbrs)
        {
            enemyDistInSpawning = 0;
        }

        CleanListOfNullValue(listToCheck);

        int enemiesToSpawn = enemyNbrs - listToCheck.Count - enemyDistInSpawning;
        if (enemiesToSpawn != 0)
        {

            for (int i = 0; i < enemiesToSpawn; i++)
            {
                enemyDistInSpawning++;
                StartCoroutine(SpawnEnemy(EnemyPrefab, listToCheck));
            }
        }
    }

    void CleanListOfNullValue(List<GameObject> listToClean)
    {
        if(listToClean.Count != 0)
        {
            listToClean.Remove(null);

        }

    }

    IEnumerator SpawnEnemy(Object EnemyToSpawn,List<GameObject> listToUpdate)
    {
        yield return new WaitForSeconds(5.0f);
        Vector3 spawnVector = new Vector3(Random.Range(minAreaSpawn.x, maxAreaSpawn.x), SpawnHeight, Random.Range(minAreaSpawn.x, maxAreaSpawn.x));
        GameObject newEnemy = Instantiate(EnemyToSpawn, spawnVector, Quaternion.identity) as GameObject;
        listToUpdate.Add(newEnemy);
    }

    void CheckCacEnemiesTargets()
    {
        CleanListOfNullValue(listOfCacEnemy);
        foreach (GameObject cacEnemy in listOfCacEnemy)
        {
            if (targetsList.Count < GameManager.playersNmbrs)
            {
                if (cacEnemy.GetComponent<EnemyCacScript>().targetsList.Count != 0)
                {
                    for (int i = 0; i < cacEnemy.GetComponent<EnemyCacScript>().targetsList.Count; i++)
                    {
                        if (!targetsList.Contains(cacEnemy.GetComponent<EnemyCacScript>().targetsList[i].GetComponent<PlayerClass>()))
                        {

                            targetsList.Add(cacEnemy.GetComponent<EnemyCacScript>().targetsList[i].GetComponent<PlayerClass>());
                        }
                    }
                }
            }

            if (cacEnemy.GetComponent<EnemyCacScript>().targetsList.Count < targetsList.Count)
            {
                for (int i = 0; i < targetsList.Count; i++)
                {
                    if (!cacEnemy.GetComponent<EnemyCacScript>().targetsList.Contains(targetsList[i].GetComponent<PlayerClass>()))
                    {

                        cacEnemy.GetComponent<EnemyCacScript>().targetsList.Add(targetsList[i].GetComponent<PlayerClass>());
                    }
                }
            }
        }
    }

    void CheckDistEnemiesTargets()
    {
        CleanListOfNullValue(listOfDistEnemy);

        foreach (GameObject distEnemy in listOfDistEnemy)
        {
            if (targetsList.Count < GameManager.playersNmbrs)
            {
                if (distEnemy.GetComponent<EnemyDistScript>().targetsList.Count != 0)
                {
                    for (int i = 0; i < distEnemy.GetComponent<EnemyDistScript>().targetsList.Count; i++)
                    {
                        if (!targetsList.Contains(distEnemy.GetComponent<EnemyDistScript>().targetsList[i].GetComponent<PlayerClass>()))
                        {

                            targetsList.Add(distEnemy.GetComponent<EnemyDistScript>().targetsList[i].GetComponent<PlayerClass>());
                        }
                    }
                }
            }

            if (distEnemy.GetComponent<EnemyDistScript>().targetsList.Count < targetsList.Count && distEnemy.GetComponent<EnemyDistScript>().targetsList.Count!=0)
            {
                for (int i = 0; i < targetsList.Count; i++)
                {
                    if (!distEnemy.GetComponent<EnemyDistScript>().targetsList.Contains(targetsList[i].GetComponent<PlayerClass>()))
                    {

                        distEnemy.GetComponent<EnemyDistScript>().targetsList.Add(targetsList[i].GetComponent<PlayerClass>());
                    }
                }
            }
        }
    }
}
