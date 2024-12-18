using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 标记
/// </summary>
public class Mark : MonoBehaviour
{
    public Transform target { get; set; }
    RectTransform rectTransform;

    void Awake()
    {
        rectTransform = transform as RectTransform;
    }

    void LateUpdate()
    {
        if (target != null)
        {
            // 将 3D 物体的位置转换为屏幕坐标
            Vector3 screenPosition = Camera.main.WorldToScreenPoint(target.position);

            // 设置 UI Image 在画布中的位置
            rectTransform.position = screenPosition;
        }
    }
}