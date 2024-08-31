using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Monster : MonoBehaviour
{
    [SerializeField] PooledObject pooledObject;
    [SerializeField] int hp;
    public event Action OnDied;

    private void OnEnable()
    {
        hp = 3;
    }
    public void TakeDamage(int damage)
    {
        hp -= damage;

        if(hp <=0)
        {
            OnDied?.Invoke();
            pooledObject.ReturnPool();
        }
    }
}
