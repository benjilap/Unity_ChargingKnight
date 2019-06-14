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

    int enemyInSpawning;


    void Start () {
        EnemyDist = Resources.Load("Enemies/EnemyDist");
        EnemyCac = Resources.Load("Enemies/EnemyCac");
        minAreaSpawn = new Vector3(this.transform.Find("Structure/WestWall").position.x + 1.5f, 0, this.transform.Find("Structure/SouthWall").position.z + 1.5f);
        maxAreaSpawn = new Vector3(this.transform.Find("Structure/EastWall").position.x - 1.5f, 0, this.transform.Find("Structure/NorthWall").position.z - 1.5f);
    }

	void Update () {
        if (EnemyCacNumber != 0)
        {
            CheckEnemyNumbers(EnemyCac, listOfCacEnemy, EnemyCacNumber);
            CheckCacEnemiesTargets();
        }

        if(EnemyDistNumber != 0)
        {
            CheckEnemyNumbers(EnemyDist, listOfDistEnemy, EnemyDistNumber);
            CheckDistEnemiesTargets();
        }


    }

    void CheckEnemyNumbers(Object EnemyPrefab,List<GameObject> listToCheck, int enemyNbrs)
    {
        if(listToCheck.Count == enemyNbrs)
        {
            enemyInSpawning = 0;
        }

        CleanListOfNullValue(listToCheck);
                
        int enemiesToSpawn = enemyNbrs - listToCheck.Count - enemyInSpawning;
        if (enemiesToSpawn !=0)
        {

            for (int i = 0; i < enemiesToSpawn; i++)
            {
                enemyInSpawning++;
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
                if (distEnemy.GetComponent<EnemyCacScript>().targetsList.Count != 0)
                {
                    for (int i = 0; i < distEnemy.GetComponent<EnemyCacScript>().targetsList.Count; i++)
                    {
                        if (!targetsList.Contains(distEnemy.GetComponent<EnemyCacScript>().targetsList[i].GetComponent<PlayerClass>()))
                        {

                            targetsList.Add(distEnemy.GetComponent<EnemyCacScript>().targetsList[i].GetComponent<PlayerClass>());
                        }
                    }
                }
            }

            if (distEnemy.GetComponent<EnemyCacScript>().targetsList.Count < targetsList.Count)
            {
                for (int i = 0; i < targetsList.Count; i++)
                {
                    if (!distEnemy.GetComponent<EnemyCacScript>().targetsList.Contains(targetsList[i].GetComponent<PlayerClass>()))
                    {

                        distEnemy.GetComponent<EnemyCacScript>().targetsList.Add(targetsList[i].GetComponent<PlayerClass>());
                    }
                }
            }
        }
    }
}
