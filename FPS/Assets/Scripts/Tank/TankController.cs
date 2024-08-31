using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TankController : MonoBehaviour
{
    
    [SerializeField] ObjectPool[] bulletType;
    [SerializeField] ObjectPool sellectedBullet;
    [SerializeField] float moveSpeed;
    [SerializeField] float rotateSpeed;
    [SerializeField] Transform muzzlePoint;
    

    private void Awake()
    {
        //sellectedBullet = bulletType[0];
    }


   
    void Update()
    {
        Move();
        BulletSellect();
        if (Input.GetKeyDown(KeyCode.Space))
        {
            Fire();
        }




    }

    private void Move()
    {
        if(Input.GetKey(KeyCode.W))
        {
            transform.Translate(Vector3.forward * moveSpeed * Time.deltaTime);
        }
        if (Input.GetKey(KeyCode.S))
        {
            transform.Translate(Vector3.back * moveSpeed * Time.deltaTime);
        }

        if (Input.GetKey(KeyCode.A))
        {
            transform.Rotate(Vector3.up * -rotateSpeed * Time.deltaTime);
        }
        if(Input.GetKey(KeyCode.D))
        {
            transform.Rotate(Vector3.up * rotateSpeed * Time.deltaTime);
        }
    }

  

    private void Fire()
    {
        Debug.Log("Fire �޼��� ȣ�� ����"); // ȣ�� Ȯ��

        PooledObject bulletCheck =  sellectedBullet.GetPool(muzzlePoint.position,muzzlePoint.rotation);

        if(bulletCheck != null)
        {
            Debug.Log("�Ѿ��� ������");
        }
        else
        {
            Debug.Log("�Ѿ��� �������� ����");
        }
    }

    private void BulletSellect()
    {
        if(Input.GetKeyDown(KeyCode.Alpha1))
        {
            sellectedBullet = bulletType[0];
            Debug.Log("1�� �Ѿ� ����");
        }
        else if(Input.GetKeyDown(KeyCode.Alpha2))
        {
            sellectedBullet = bulletType[1];
            Debug.Log("2�� �Ѿ� ����");
        }
    }
}
