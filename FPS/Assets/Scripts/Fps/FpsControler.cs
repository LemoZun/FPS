using System.Collections;
using UnityEngine;

public class FpsControler : MonoBehaviour
{
    [SerializeField] Transform camTransform;
    [SerializeField] float rotateSpeed;
    [SerializeField] float moveSpeed;
    [SerializeField] Transform muzzlePoint;

    [SerializeField] float shootPeriod;
    [SerializeField] int magazine;
    const int MaxBullets = 30;
    [SerializeField] float reloadingTime = 2f;

    Coroutine fireCoroutine;
    Coroutine reloadCoroutine;
    bool isReloading = false;


    void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
        magazine = MaxBullets;
    }

    void Update()
    {

        Move();
        Look();

        if (Input.GetMouseButtonDown(0) && fireCoroutine == null && !isReloading)
            Fire();

        if (Input.GetMouseButtonUp(0) && fireCoroutine != null)
            StopFire();

        if (Input.GetKeyDown(KeyCode.R) && reloadCoroutine == null && !isReloading)
            Reload();
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
        camTransform.Rotate(Vector3.right, rotateSpeed * -y * Time.deltaTime);
    }

    private void Fire()
    {
        if (fireCoroutine == null)
            fireCoroutine = StartCoroutine(ShootsRoutine());
    }

    private void StopFire()
    {
        if (fireCoroutine != null)
        {
            StopCoroutine(fireCoroutine);
            fireCoroutine = null;
        }
    }

    private void ShootRay()
    {
        Debug.DrawRay(camTransform.position, camTransform.forward, Color.red);

        if (Physics.Raycast(camTransform.position, camTransform.forward, out RaycastHit hit))
        {
            Debug.DrawRay(camTransform.position, camTransform.forward * hit.distance, Color.red);
            GameObject instance = hit.collider.gameObject;
            Monster monster = instance.GetComponent<Monster>();

            if (monster != null)
            {
                Debug.Log("�Ѿ� ����!");
                monster.TakeDamage(1); // �ʿ�� int bulletDamage �����ɷ� ������ ���� ���� �ᵵ ��
            }
        }
    }

    IEnumerator ShootsRoutine()
    {
        WaitForSeconds delay = new WaitForSeconds(shootPeriod);
        while (Input.GetMouseButton(0) && magazine > 0 && !isReloading)
        {
            magazine--;
            Debug.Log($"���� �Ѿ��� {magazine}��");
            ShootRay();

            yield return delay;
        }

        if (magazine <= 0)
        {
            Debug.Log("������ �ʿ��մϴ�.");
        }
        fireCoroutine = null;
    }

    private void Reload()
    {
        if (reloadCoroutine == null)
            reloadCoroutine = StartCoroutine(ReloadRoutine());
    }
    IEnumerator ReloadRoutine()
    {
        isReloading = true;
        Debug.Log("�����մϴ�");
        yield return new WaitForSeconds(reloadingTime);
        magazine = MaxBullets;
        Debug.Log("���� �Ϸ�");
        isReloading = false;
        reloadCoroutine = null;
    }
}
// ������ �÷��̾� ������Ʈ�� ��ǻ� ���»��¶� �׷�����
// Fps ��Ʈ�ѷ��� �ʹ� �������

// ���� �ڷ�ƾ�� �Ҵ��� ���� �����ؾ�����? 
// �����Ȳ�̶�� �������� ��Ȳ������ �ְ����� ������ ������ ..
// �� ���콺 Ŭ�� �����Ҷ� �ڷ�ƾ ���� ���� �ڷ�ƾ ������ �ϸ� 

// �ѹ� ���� �� �Ѿ� ������ � �ǳ� �پ ��µǴ� ���� �߻�
// ���콺��ưdown�� ���� �Ἥ ���� �õ�
// �� �ٲٷ��� �ܹߵ� �����ϸ鼭 �˾Ҵµ� 
// Collapse �ѵּ� �̹� ���� ���� ���� �αװ� �Ⱥ������ſ���..
// Ǫ�ϱ� �����۵��ϳ� �� �̰ɷ� ��ð��� �� �ƾƾ�

// �����߿� ��Ƽ� ���³��� �����.. ��� �ϰ������ �̰� �����̴ϱ�..
// �����߿��� �߻� ���ϰ��ϴ� ������ �߰��ؾ��ϳ�
// �ϴ±迡 �����߿� �� ����Ű ������ �͵� ������������ ���� �� �ִ�.