using System.Collections;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public float Speed = 400;
    private float m_MoveSpeed = 200;
    public float rotationSpeed = 5.0f; // 旋转速度

    public VariableJoystick variableJoystick;
    public CharacterController rb;

    public bool teleporting;

    public void Update()
    {
        if (teleporting)
        {
            return;
        }

        m_MoveSpeed = Speed;
        if(Input.GetKey(KeyCode.LeftShift))
        {
            m_MoveSpeed = Speed * 2;
        }
        
        // 根据虚拟摇杆的输入计算旋转角度
        float horizontalInput = variableJoystick.Horizontal;
        if (Mathf.Abs(horizontalInput) < 0.5f)
        {
            Vector3 direction = transform.forward * variableJoystick.Vertical;
            rb.SimpleMove(direction * (m_MoveSpeed * Time.deltaTime));
        }
        else
        {
            float rotationY = horizontalInput * rotationSpeed * Time.deltaTime;
            transform.Rotate(Vector3.up, rotationY);
        }
    }

    public void Teleport(Transform point)
    {
        teleporting = true;
        rb.enabled = false;
        StartCoroutine(DelayRecover(point.position));
    }

    IEnumerator DelayRecover(Vector3 point)
    {
        yield return new WaitForSeconds(1);
        transform.position = point;
        yield return new WaitForSeconds(1);
        rb.enabled = true;
        teleporting = false;
    }
}