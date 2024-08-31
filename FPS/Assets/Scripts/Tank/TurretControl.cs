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

        //������ �� ���� ȸ���� ���
        
        xAngle = Mathf.Clamp(xAngle + inputxAngle, -maxAngle, 0);
        yAngle += inputyAngle; 


        transform.localRotation = Quaternion.Euler(xAngle, yAngle, 0);
    }


    // �ȵǸ� ������Ʈ�� �־����� ���� Ȯ���� ����!!
    // ��ũ ��Ʈ�Ѱ� �ͷ� ��Ʈ���� ���� �ִ°� ������..?
    // ��ũ��Ʈ�ѿ��� �ڽ�������Ʈ�� �ͷ��� �޾Ƽ� �����ϴ°� ���� �ƴϸ� �̰� ����..

    //// �� left right �ϱ�.. �� x��ȸ���� ..? ����Ƽ ���� �� �� ���߰ڴ�
    //if (Input.GetKey(KeyCode.UpArrow))
    //{
    //    transform.Rotate(Vector3.right * rotateSpeed * Time.deltaTime);
    //}
    //transform.Rotate(Vector3.left * rotateSpeed * Time.deltaTime);
    // �̰� �׳ɾ��� deltaTime���� �ڵ����� ȸ����(������ ������ �翬..)
    //transform.rotation = Quaternion.Euler(xAngle, 0, 0); �̷��� �ٸ��� ȸ���� ���� ������
    // �ᱹ x�� ȸ������ �ƴ϶� ��ü������ �����, z�� ȸ���� �ʿ����
    // y�� ȸ���� �߰����ָ� ��
    //rotation���� �ϸ� �̻����� ��������̶�?
    //transform.rotation = Quaternion.Euler(xAngle, yAngle, 0);
    //GetAxisRaw�� ȣ����Ż ��Ƽ���� �޴� ����� �ִ�
    //�� wasd ����Ű�� ��� �Ǳ� ������ ����Ƽ���� ����Ű�� wasd�� ���ְų� ���� ������ ����
    // Mathf.Clamp : Ư�� ���� �ּҰ�, �ִ밪 ���̷� ���ѽ� ���
    // �� Mathf.Clamp(float value, float min, float max)
    //������ ��� �Ʊ淡 -maxAngle�� �ؾ� �� �Ǵ°���..
    //yAngle = Mathf.Clamp(yAngle + inputyAngle, 0, 360); �̷��� �ϸ� 360�������� ���� ���־ ������ ����
    // �׳� ��ǲ�ޱ� ������� �ޱۿ� �־��ָ� ��

}
