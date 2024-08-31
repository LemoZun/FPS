using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FpsControler : MonoBehaviour
{
    [SerializeField] Transform camTransform;
    [SerializeField] float rotateSpeed;
    [SerializeField] float moveSpeed;
    [SerializeField] Transform muzzlePoint;

    void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

    void Update()
    {
       
        Move();
        Look();
        if(Input.GetMouseButtonDown(0))
        {
            Fire();
        }

    }

    private void Move()
    {
        // �̵������� �̻��ߴ� -> ���� ī�޶��� �����̼� ��������
        
        if (Input.GetKey(KeyCode.W))
        {
            transform.Translate(Vector3.forward * moveSpeed * Time.deltaTime, Space.Self);
        }        
        if (Input.GetKey(KeyCode.S))
        {
            transform.Translate(Vector3.back * moveSpeed * Time.deltaTime, Space.Self);
        }        
        if (Input.GetKey(KeyCode.A))
        {
            transform.Translate(Vector3.left * moveSpeed * Time.deltaTime, Space.Self);
        }        
        if (Input.GetKey(KeyCode.D))
        {
            transform.Translate(Vector3.right * moveSpeed * Time.deltaTime, Space.Self);
        }

    }
    private void Look()
    {
        float x = Input.GetAxis("Mouse X"); // ���콺 �¿� ������ ��
        float y = Input.GetAxis("Mouse Y"); // ���콺 ���� ������ ��
        

        transform.Rotate(Vector3.up, rotateSpeed * x * Time.deltaTime);
        camTransform.Rotate(Vector3.right,rotateSpeed * -y * Time.deltaTime);
    }

    private void Fire()
    {
        Debug.DrawRay(camTransform.position, camTransform.forward, Color.red);

        if(Physics.Raycast(camTransform.position,camTransform.forward, out RaycastHit hit))
        {
            Debug.DrawRay(camTransform.position, camTransform.forward * hit.distance, Color.red);
            GameObject instance = hit.collider.gameObject;
            Monster monster = instance.GetComponent<Monster>();

            if (monster != null)
            {
                Debug.Log("�Ѿ� ����!");
                monster.TakeDamage(1);
            }

            
        }
    }
}
