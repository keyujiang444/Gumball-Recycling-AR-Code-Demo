using UnityEngine;
/// <summary>
/// 标记模型
/// </summary>
public class MarkModel : MonoBehaviour
{
    private static readonly int anim = Animator.StringToHash("animation");
    public Sprite m_Detail;
    public Animator m_animtor;
    public bool HasAnimation;

    public void SetAnimation(int value)
    {
        m_animtor?.SetInteger(anim, value);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            InteractUI.Instance.ShowMark(this);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            InteractUI.Instance.HideMark();
        }
    }
}