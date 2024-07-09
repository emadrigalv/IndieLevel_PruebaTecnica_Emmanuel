using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnHandler : MonoBehaviour
{
    [Header("Dependencies")]
    [SerializeField] private Pooler objectPooler;
    [SerializeField] private List<Transform> spawnPositions;
    [SerializeField] private Character_SO enemyData;

    [Header("Parameters")]
    [SerializeField] private int enemiesAlive;
    [SerializeField] private float enemySpawnTime;
    [SerializeField] private bool inRound;
    [SerializeField] private InitialData initialData;

    private List<GameObject> activeEnemies = new List<GameObject>();
    private Coroutine spawnEnemiesCoroutine;

    private void Start()
    {
        EnemiesInitialize();
    }

    public void EnemiesInitialize()
    {
        Enemy.spawnHandler = this;

        enemyData.attackDamage = initialData.attackDamage;
        enemyData.maxHealth = initialData.maxHealth;
        enemyData.movementSpeed = initialData.movementSpeed;
    }

    public void SpawnEnemies()
    {
        spawnEnemiesCoroutine = StartCoroutine(WaitToSpawn());
    }

    public void EnemyDead(Vector3 position, GameObject enemy)
    {
        objectPooler.SpawnFromPool("Coin", position);
        
        activeEnemies.Remove(enemy);
        enemy.SetActive(false);
    }

    public void StartRound(int roundIndex)
    {

        if (roundIndex > 1)
        {
            enemyData.attackDamage = initialData.attackDamage * roundIndex;
            enemyData.maxHealth = initialData.maxHealth * roundIndex;
            enemyData.movementSpeed = initialData.movementSpeed + (0.1f * roundIndex);
        }

        inRound = true;

        SpawnEnemies();
    }

    public void FinishedRound()
    {
        inRound = false;
        if (spawnEnemiesCoroutine != null) StopCoroutine(spawnEnemiesCoroutine);

        foreach (GameObject enemy in activeEnemies) 
        {
            enemy.SetActive(false);
        }

        activeEnemies.Clear();
        spawnEnemiesCoroutine = null;

        Debug.Log("parar spawneo");
    }

    private IEnumerator WaitToSpawn()
    {
        if (inRound && activeEnemies.Count < enemiesAlive)
        {
            int enemiesToSpawn = enemiesAlive - activeEnemies.Count;

            for (int i = 0; i < enemiesToSpawn; i++)
            {
                yield return new WaitForSeconds(enemiesToSpawn);

                int randomSpawn = Random.Range(0, 4);
                GameObject enemy = objectPooler.SpawnFromPool("Enemy", spawnPositions[randomSpawn].position);

                activeEnemies.Add(enemy);
            }
        }
        else 
        { 
            yield return new WaitForSeconds(1); 
        }

        spawnEnemiesCoroutine = StartCoroutine(WaitToSpawn());
    }
}

[System.Serializable]
public struct InitialData
{
    public float movementSpeed;
    public int attackDamage;
    public int maxHealth;
}
