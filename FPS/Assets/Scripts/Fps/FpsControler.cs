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
        // 이동방향이 이상했다 -> 메인 카메라의 로테이션 문제였다
        
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
        float x = Input.GetAxis("Mouse X"); // 마우스 좌우 움직임 량
        float y = Input.GetAxis("Mouse Y"); // 마우스 상하 움직임 량
        

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
                Debug.Log("총알 맞춤!");
                monster.TakeDamage(1);
            }

            
        }
    }
}
