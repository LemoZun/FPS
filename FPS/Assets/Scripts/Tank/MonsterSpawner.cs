using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonsterSpawner : MonoBehaviour
{
    [SerializeField] ObjectPool[] monsterPool;
    [SerializeField] ObjectPool monster;

    // [SerializeField] int maxMonster; // ��� ������Ʈ Ǯ���� ��
    [SerializeField] float spawnLocation;


    private void Update()
    {
        SpawnMonster();
    }

    private void SpawnMonster()
    {
        Vector3 spawnPosition = new Vector3(Random.Range(-spawnLocation, spawnLocation), 1, Random.Range(-spawnLocation, spawnLocation));
        PooledObject spawnedMonster = monster.GetPool(spawnPosition, Quaternion.Euler(0,0,0));
    }
}
