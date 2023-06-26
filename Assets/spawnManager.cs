using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class spawnManager : MonoBehaviour
{
    public GameObject basicSwordman;
    public GameObject bigGuy;
    public GameObject crossBow;
    //public GameObject ghost;

    public GameObject newDoubleCrossbow;
    public GameObject newBasicSwordman;
    public GameObject newBigGuy;
    //public GameObject newGhost;

    public bool isBasicSwordmanCanSpawn = true;
    public bool isBigGuyCanSpawn = true;
    public bool isCrossBowCanSpawn = true;

    public bool readyForSpawn = true;

    public float basicSwordmanSpawnTime = 4.0f;
    public float bigGuySPawnTime = 7.0f;
    public float crossBowSpawnTime = 20.0f;

    public float basicSwordmanSpawnTimeMultiplier = 0.9f;
    public float bigGuySPawnTimeMultiplier = 0.9f;
    public float crossBowSpawnTimeMultiplier = 0.8f;

    public int basicSwordmansToGetHarder = 10;
    public int bigGuysToGetHarder = 8;
    public int crossBowsToGetHarder = 6;

    public int basicSwordmanCounter = 0;
    public int bigGuyCounter = 0;
    public int crossBowCounter = 0;

    public bool spawnedTutorialBasicSwordman = false;
    public bool spawnedTutorialBigGuy = false;
    public bool spawnedTutorialCrossBow = false;

    public bool killedTutorialBasicSwordman = false;
    public bool killedTutorialBigGuy = false;
    public bool killedTutorialCrossBow = false;

    public bool isPassedTutorial = false;

    private Vector3[] spawnPoints = new [] { new Vector3(-25f, 25f, 0f),
                                             new Vector3(-25f, 15f, 0f),
                                             new Vector3(-25f, -5f, 0f),
                                             new Vector3(-15f, -22f, 0f),
                                             new Vector3(10f, -22f, 0f),
                                             new Vector3(26f, -14f, 0f),
                                             new Vector3(26f, 16f, 0f),
                                             new Vector3(10f, 28, 0f),
    };
    void Start()
    {

    }

    void Update()
    {
        if (!isPassedTutorial)
        {
            spawnTutorialEnemies();
            checkIfPlayerPassedtutorial();
        }
        if (isPassedTutorial)
        {
            StartCoroutine(spawns());
        }
    }

    public void checkIfPlayerPassedtutorial()
    {
        if(killedTutorialBasicSwordman
            && killedTutorialBigGuy
            && killedTutorialCrossBow)
        {
            isPassedTutorial = true;
        }
    }

    public void spawnTutorialEnemies()
    {
        if (!spawnedTutorialBasicSwordman)
        {
            int numb = randomNumberFrom0to7();
            Vector3 spawnPoint = spawnPoints[numb];
            newBasicSwordman = Instantiate(basicSwordman, spawnPoint, Quaternion.identity);
            spawnedTutorialBasicSwordman = true;
            basicSwordmanCounter++;
        }
        if (newBasicSwordman == null )
        {
            killedTutorialBasicSwordman = true;
        }
        if(killedTutorialBasicSwordman && !spawnedTutorialBigGuy)
        {
            int numb = randomNumberFrom0to7();
            Vector3 spawnPoint = spawnPoints[numb];
            newBigGuy = Instantiate(bigGuy, spawnPoint, Quaternion.identity);
            spawnedTutorialBigGuy = true;
            bigGuyCounter++;
        }
        if (killedTutorialBasicSwordman && newBigGuy == null)
        {
            killedTutorialBigGuy = true;
        }
        if (killedTutorialBigGuy && killedTutorialBasicSwordman && !spawnedTutorialCrossBow)
        {
            int numb = randomNumberFrom0to7();
            Vector3 spawnPoint = spawnPoints[numb];
            newDoubleCrossbow = Instantiate(crossBow, spawnPoint, Quaternion.identity);
            spawnedTutorialCrossBow = true;
            crossBowCounter++;
        }
        if (killedTutorialBigGuy && newDoubleCrossbow == null)
        {
            killedTutorialCrossBow = true;
        }
    }

    public IEnumerator spawns()
    {
        if (readyForSpawn)
        {
            int tmp = ((int)Random.Range(0f, 10f));
            switch (tmp)
            {
                case 7:
                case 1:
                case 2:
                case 3:
                    StartCoroutine(spawnBasicSwordman());
                    getHarderIsPossible(basicSwordmansToGetHarder, ref basicSwordmanCounter, ref basicSwordmanSpawnTime, basicSwordmanSpawnTimeMultiplier);
                    break;
                case 4:
                case 5:
                    StartCoroutine(spawnBigGuy());
                    getHarderIsPossible(bigGuysToGetHarder, ref bigGuyCounter, ref bigGuySPawnTime, bigGuySPawnTimeMultiplier);
                    break;
                case 0:
                    StartCoroutine(spawnCrossBow());
                    getHarderIsPossible(crossBowsToGetHarder, ref crossBowCounter, ref crossBowSpawnTime, crossBowSpawnTimeMultiplier);
                    break;


            }
            readyForSpawn = false;
            yield return new WaitForSeconds(1.0f);
            readyForSpawn = true;
        }
    }

    public IEnumerator spawnBasicSwordman()
    {
        if (isBasicSwordmanCanSpawn)
        {
            int numb = randomNumberFrom0to7();
            Vector3 spawnPoint = spawnPoints[numb];
            isBasicSwordmanCanSpawn = false;
            newBasicSwordman = Instantiate(basicSwordman, spawnPoint, Quaternion.identity);
            basicSwordmanCounter++;
            yield return new WaitForSeconds(basicSwordmanSpawnTime);
            isBasicSwordmanCanSpawn = true;
        }
     }

    public IEnumerator spawnBigGuy()
    {
        if (isBigGuyCanSpawn)
        {
            int numb = randomNumberFrom0to7();
            Vector3 spawnPoint = spawnPoints[numb];
            isBigGuyCanSpawn = false;
            newBigGuy = Instantiate(bigGuy, spawnPoint, Quaternion.identity);
            bigGuyCounter++;
            yield return new WaitForSeconds(bigGuySPawnTime);
            isBigGuyCanSpawn = true;
        }
    }

    public IEnumerator spawnCrossBow()
    {
        if (isCrossBowCanSpawn)
        {
            int numb = randomNumberFrom0to7();
            Vector3 spawnPoint = spawnPoints[numb];
            isCrossBowCanSpawn = false;
            newDoubleCrossbow = Instantiate(crossBow, spawnPoint, Quaternion.identity);
            crossBowCounter++;
            yield return new WaitForSeconds(crossBowSpawnTime);
            isCrossBowCanSpawn = true;
        }
    }

    public int randomNumberFrom0to7()
    {
        return ((int)Random.Range(0f, 7.8f));
    }

    public void getHarderIsPossible(int numberToGetHarder,ref int actualNumber,ref float characterTimer, float characterTimerMultiplier)
    {
        if(actualNumber >= numberToGetHarder)
        {
            characterTimer *= characterTimerMultiplier;
            actualNumber = 0;
        }
    }
}
