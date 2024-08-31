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
            Debug.Log("���Ϳ� �浹�ؼ� �̻��� �����");
        }
        
    }

    IEnumerator BulletDestroyRoutine()
    {
        yield return new WaitForSeconds(returnTime);
        Debug.Log("5�� ������ �̻��� �����");

        if(pooledObject != null ) //gameObject�� �ϸ� ���� ������ ���´�
        {
            pooledObject.ReturnPool();
            Debug.Log("����Ǯ");
            StopCoroutine(bulletLife);
            bulletLife = null;
        }
    }
}
// ���� �̻����� �ڱ� �ڴ�� ���� ���鼭 ���ư����� ����



