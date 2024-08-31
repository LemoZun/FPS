using System.Collections;
using UnityEngine;

public class MonsterSpawner : MonoBehaviour
{
    [SerializeField] ObjectPool[] monsterPool;
    [SerializeField] ObjectPool monster;
    [SerializeField] Monster publisher;
    [SerializeField] float spawnPeriod = 3f;
    [SerializeField] float spawnLocation = 8f;
    [SerializeField] Coroutine spawnRoutine;

    private void OnEnable()
    {
        if (publisher != null)
        {
            publisher.OnDied += startSpawn;
        }
    }

    private void OnDisable()
    {
        //if(publisher == null)  or if(publisher != null) 
        //publisher가 null일때도 구독 정보는 메모리에 남는다고 했던거같은데
        // 그러면 조건을 아예 없애는게 맞을까?         
        publisher.OnDied -= startSpawn;

    }

    private void startSpawn()
    {
        if (spawnRoutine == null)
            spawnRoutine = StartCoroutine(Spawner());
    }

    IEnumerator Spawner()
    {
        yield return new WaitForSeconds(spawnPeriod);
        SpawnMonster();
        if (spawnRoutine != null)
            spawnRoutine = null;
    }
    private void SpawnMonster()
    {
        Vector3 spawnPosition = new Vector3(Random.Range(-spawnLocation, spawnLocation), 1, Random.Range(-spawnLocation, spawnLocation));
        PooledObject spawnedMonster = monster.GetPool(spawnPosition, Quaternion.Euler(0, 0, 0));

        Monster newMonster = spawnedMonster.GetComponent<Monster>();
        if (newMonster != null)
            newMonster.OnDied += startSpawn;

    }
}
