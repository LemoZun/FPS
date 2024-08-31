using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Monster2 : MonoBehaviour
{
    [SerializeField] PooledObject pooledObject;
    [SerializeField] int hp;
    private Missile takenMissile;

    private void OnEnable()
    {
        hp = 3;
    }

    private void OnCollisionEnter(Collision collision)
    {
        Debug.Log("충돌함");
        if(collision.gameObject.layer == LayerMask.NameToLayer("Missile")) //메타데이터를 뭘 빼먹었나? 계속 레이어 등록이 풀려서 아래가 실행이 안됨
        {
            takenMissile = collision.gameObject.GetComponent<Missile>();
        }
        if(takenMissile != null) // 레퍼런스 오류 없애기 위해 Null이 아닐때 조건 추가
        {
            TakeDamage(takenMissile.attackPoint);
        }
        
        
    }

    public void TakeDamage(int damage)
    {
        hp -= damage;

        if(hp <=0)
        {
            pooledObject.ReturnPool();
        }
    }
}
