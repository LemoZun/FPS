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
                Debug.Log("총알 맞춤!");
                monster.TakeDamage(1); // 필요시 int bulletDamage 같은걸로 데미지 지정 변수 써도 됨
            }
        }
    }

    IEnumerator ShootsRoutine()
    {
        WaitForSeconds delay = new WaitForSeconds(shootPeriod);
        while (Input.GetMouseButton(0) && magazine > 0 && !isReloading)
        {
            magazine--;
            Debug.Log($"남은 총알은 {magazine}개");
            ShootRay();

            yield return delay;
        }

        if (magazine <= 0)
        {
            Debug.Log("장전이 필요합니다.");
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
        Debug.Log("장전합니다");
        yield return new WaitForSeconds(reloadingTime);
        magazine = MaxBullets;
        Debug.Log("장전 완료");
        isReloading = false;
        reloadCoroutine = null;
    }
}
// 당장은 플레이어 오브젝트가 사실상 없는상태라 그렇지만
// Fps 컨트롤러가 너무 길어진다

// 연사 코루틴의 할당을 언제 해제해야하지? 
// 보통상황이라면 게임종료 상황같은게 있겠지만 당장은 없으니 ..
// ㄴ 마우스 클릭 시작할때 코루틴 시작 뗄때 코루틴 해제로 하면 

// 한번 장전 후 총알 개수가 몇개 건너 뛰어서 출력되는 현상 발생
// 마우스버튼down도 같이 써서 막는 시도
// 아 바꾸려고 단발도 구현하면서 알았는데 
// Collapse 켜둬서 이미 위에 나온 같은 로그가 안보였던거였다..
// 푸니까 정상작동하네 아 이걸로 몇시간을 아 아아악

// 장전중에 방아쇠 당기는놈이 어딨어.. 라고 하고싶지만 이건 게임이니까..
// 장전중에는 발사 못하게하는 로직도 추가해야하나
// 하는김에 장전중에 또 장전키 누르는 것도 같은조건으로 막을 수 있다.