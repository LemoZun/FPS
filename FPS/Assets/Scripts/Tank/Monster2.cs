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
        Debug.Log("�浹��");
        if(collision.gameObject.layer == LayerMask.NameToLayer("Missile")) //��Ÿ�����͸� �� ���Ծ���? ��� ���̾� ����� Ǯ���� �Ʒ��� ������ �ȵ�
        {
            takenMissile = collision.gameObject.GetComponent<Missile>();
        }
        if(takenMissile != null) // ���۷��� ���� ���ֱ� ���� Null�� �ƴҶ� ���� �߰�
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
