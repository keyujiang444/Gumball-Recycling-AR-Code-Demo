using UnityEngine;
using UnityEngine.UI;
/// <summary>
/// 战斗UI，玩家操作跳跃冲击遥感等
/// </summary>
public class BattleUI : MonoBehaviour
{
    public VariableJoystick joystick;
    public Button m_FastBtn;
    public Button m_JumpBtn;
    public Button m_PushBtn;

    void Start()
    {
        m_FastBtn.onClick.AddListener(() => { Dragon.Localplayer?.Jump(); });
        m_JumpBtn.onClick.AddListener(() => { Dragon.Localplayer?.Fast(); });
        m_PushBtn.onClick.AddListener(() => { Dragon.Localplayer?.Push(); });
    }

    private void Update()
    {
        if (Dragon.Localplayer == null)
        {
            return;
        }

        Dragon.Localplayer.Move(joystick.Horizontal, joystick.Vertical);
    }
}