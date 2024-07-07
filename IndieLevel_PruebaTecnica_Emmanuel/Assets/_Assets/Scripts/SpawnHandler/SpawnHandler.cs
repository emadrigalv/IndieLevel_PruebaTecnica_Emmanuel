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
    [SerializeField] private bool inRound;
    //[SerializeField] private int roundIndex;

    private Character_SO initialData;
    private List<GameObject> activeEnemies = new List<GameObject>();
    private Coroutine spawnEnemiesCoroutine;

    private void Start()
    {
        Initialize();
    }

    public void Initialize()
    {
        Enemy.spawnHandler = this;
        initialData = enemyData;
    }

    public void SpawnEnemies()
    {
        // agregar enemigos a la lista e inicializarlos en base a los nuevos datos
        spawnEnemiesCoroutine = StartCoroutine(WaitToSpawn());
        Debug.Log("oe ya volvio");
    }

    public void EnemyDead(Vector3 position, GameObject enemy)
    {
        objectPooler.SpawnFromPool("Coin", position);
        
        //sacar enemigos de la lista
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
        else enemyData = initialData;

        inRound = true;

        SpawnEnemies();
    }

    //[ContextMenu("Se acabo pa")]
    public void FinishedRound()
    {
        inRound = false;
        StopCoroutine(spawnEnemiesCoroutine);

        foreach (GameObject enemy in activeEnemies) 
        {
            enemy.SetActive(false);
        }

        activeEnemies.Clear();
        spawnEnemiesCoroutine = null;

        //Debuging purposes
        enemyData = initialData;
    }

    private IEnumerator WaitToSpawn()
    {
        if (inRound && activeEnemies.Count < enemiesAlive)
        {
            int enemiesToSpawn = enemiesAlive - activeEnemies.Count;

            for (int i = 0; i < enemiesToSpawn; i++)
            {
                yield return new WaitForSeconds(1);

                int randomSpawn = Random.Range(0, 4);
                GameObject enemy = objectPooler.SpawnFromPool("Enemy", spawnPositions[randomSpawn].position);
                enemy.GetComponent<Enemy>().InitializeEnemy();

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
