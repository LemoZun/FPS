using UnityEngine;

public class TurretControl : MonoBehaviour
{
    [SerializeField] float maxAngle = 60f; 
    [SerializeField] float rotateSpeed;
    private float xAngle;
    private float yAngle;

    private void Update()
    {
        AdjustTurret();
    }
    private void AdjustTurret()
    {
        float inputxAngle = 0f;
        float inputyAngle = 0f;

        if (Input.GetKey(KeyCode.LeftArrow))
        {
            inputyAngle = -rotateSpeed * Time.deltaTime;
        }
        if (Input.GetKey(KeyCode.RightArrow))
        {
            inputyAngle = rotateSpeed * Time.deltaTime;
        }

        if (Input.GetKey(KeyCode.UpArrow))
        {
            inputxAngle = -rotateSpeed * Time.deltaTime;
        }
        if (Input.GetKey(KeyCode.DownArrow))
        {
            inputxAngle = rotateSpeed * Time.deltaTime;
        }

        //제한을 둔 최종 회전값 계산
        
        xAngle = Mathf.Clamp(xAngle + inputxAngle, -maxAngle, 0);
        yAngle += inputyAngle; 


        transform.localRotation = Quaternion.Euler(xAngle, yAngle, 0);
    }


    // 안되면 업데이트에 넣었는지 먼저 확인해 제발!!
    // 탱크 컨트롤과 터렛 컨트롤이 따로 있는게 괜찮나..?
    // 탱크컨트롤에서 자식컴포넌트로 터렛을 받아서 조정하는게 낫나 아니면 이게 낫나..

    //// 왜 left right 일까.. 왜 x축회전이 ..? 유니티 축을 좀 더 봐야겠다
    //if (Input.GetKey(KeyCode.UpArrow))
    //{
    //    transform.Rotate(Vector3.right * rotateSpeed * Time.deltaTime);
    //}
    //transform.Rotate(Vector3.left * rotateSpeed * Time.deltaTime);
    // 이걸 그냥쓰면 deltaTime마다 자동으로 회전함(조건이 없으니 당연..)
    //transform.rotation = Quaternion.Euler(xAngle, 0, 0); 이려면 다른축 회전이 없이 고정됨
    // 결국 x축 회전만이 아니라 전체적으로 써야함, z축 회전은 필요없다
    // y축 회전만 추가해주면 됨
    //rotation으로 하면 이상해짐 월드기준이라?
    //transform.rotation = Quaternion.Euler(xAngle, yAngle, 0);
    //GetAxisRaw로 호리젠탈 버티컬을 받는 방법도 있다
    //ㄴ wasd 방향키가 모두 되기 때문에 유니티에서 방향키는 wasd는 빼주거나 따로 구현이 낫다
    // Mathf.Clamp : 특정 값을 최소값, 최대값 사이로 제한시 사용
    // ㄴ Mathf.Clamp(float value, float min, float max)
    //기준이 어떻게 됐길래 -maxAngle로 해야 잘 되는거지..
    //yAngle = Mathf.Clamp(yAngle + inputyAngle, 0, 360); 이렇게 하면 360도까지만 돌릴 수있어서 제한이 생김
    // 그냥 인풋앵글 받은대로 앵글에 넣어주면 됨

}
