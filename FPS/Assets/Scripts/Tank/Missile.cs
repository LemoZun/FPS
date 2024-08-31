using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Missile : MonoBehaviour
{
    [SerializeField] PooledObject pooledObject;

    [SerializeField] float moveSpeed;
    [SerializeField] public int attackPoint;
    [SerializeField] float returnTime = 5f;

    private Coroutine bulletLife;



    // Update is called once per frame
    void Update()
    {
        if (bulletLife == null)
        {
            bulletLife = StartCoroutine(BulletDestroyRoutine());
        }
        transform.Translate(Vector3.forward * moveSpeed * Time.deltaTime);

    }

    private void OnCollisionEnter(Collision collision)
    {
        if(collision.gameObject.layer == LayerMask.NameToLayer("Monster"))
        {
            pooledObject.ReturnPool();
            Debug.Log("몬스터와 충돌해서 미사일 사라짐");
        }
        
    }

    IEnumerator BulletDestroyRoutine()
    {
        yield return new WaitForSeconds(returnTime);
        Debug.Log("5초 지나서 미사일 사라짐");

        if(pooledObject != null ) //gameObject로 하면 가끔 에러가 나온다
        {
            pooledObject.ReturnPool();
            Debug.Log("리턴풀");
            StopCoroutine(bulletLife);
            bulletLife = null;
        }
    }
}
// 가끔 미사일이 자기 멋대로 빙빙 돌면서 날아갈때가 있음



