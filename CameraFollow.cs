using UnityEngine;

/// <summary>
/// 相机跟随
/// </summary>
public class CameraFollow : MonoBehaviour
{
    public Transform target; // 要跟随的目标
    public float smoothSpeed = 0.125f; // 平滑移动的速度
    public Vector3 offset; // 相机与目标之间的偏移量
    public VariableJoystick variableJoystick;
    public float rotationSpeed = 5.0f; // 旋转速度

    void LateUpdate()
    {
        if (target != null)
        {
            // 计算相机目标位置
            Vector3 desiredPosition = target.position + offset;
            // 使用平滑插值来移动相机
            Vector3 smoothedPosition = Vector3.Lerp(transform.position, desiredPosition, smoothSpeed);
            transform.position = smoothedPosition;

            // 根据虚拟摇杆的输入计算旋转角度
            float horizontalInput = variableJoystick.Horizontal;
            float rotationY = horizontalInput * rotationSpeed * Time.deltaTime;

            // 绕Y轴旋转
            transform.RotateAround(target.position, Vector3.up, rotationY);
            // 计算出新的相机方向并应用
            Quaternion rotation = Quaternion.Euler(0, rotationY, 0);
            offset = rotation * offset;

            // 可以选择让相机始终朝向目标
            transform.LookAt(target);
        }
    }
}