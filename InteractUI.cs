using UnityEngine;
using UnityEngine.UI;

public class InteractUI : MonoBehaviour
{
    public static InteractUI Instance;
    [SerializeField] private GameObject m_Detail;
    [SerializeField] private GameObject m_Board;
    [SerializeField] private Button m_Jump;
    [SerializeField] private Button m_Fly;
    [SerializeField] private Button m_Run;
    [SerializeField] private Button m_Die;
    [SerializeField] private Button m_Mark;
    [SerializeField] private Image m_Content;
    [SerializeField] private Button m_CloseBtn;

    public MarkModel m_Model;

    public const int Jump = 1;
    public const int Fly = 2;
    public const int Run = 3;
    public const int Die = 4;
    void Awake()
    {
        Instance = this;
        m_CloseBtn.onClick.AddListener(HideDetail);
        m_Jump.GetComponent<Button>().onClick.AddListener(() =>
        {
            m_Model.SetAnimation(Jump);
        });
        m_Fly.GetComponent<Button>().onClick.AddListener(() =>
        {
            m_Model.SetAnimation(Fly);
        });
        m_Run.GetComponent<Button>().onClick.AddListener(() =>
        {
            m_Model.SetAnimation(Run);
        });
        m_Die.GetComponent<Button>().onClick.AddListener(() =>
        {
            m_Model.SetAnimation(Die);
        });
        m_Mark.GetComponent<Button>().onClick.AddListener(() =>
        {
            ShowDetail(m_Model.m_Detail);
        });
    }

    private void HideDetail()
    {
        m_Detail.SetActive(false);
    }

    public void ShowDetail(Sprite content)
    {
        m_Detail.SetActive(true);
        m_Content.sprite = content;
    }

    public void ShowMark(MarkModel model)
    {
        m_Model = model;
        // m_Mark.target = m_Model.transform;
        m_Mark.gameObject.SetActive(true);
        m_Board.gameObject.SetActive(m_Model.HasAnimation);
    }

    public void HideMark()
    {
        m_Model = null;
        // m_Mark.target = null;
        m_Mark.gameObject.SetActive(false);
        m_Board.gameObject.SetActive(false);
    }
}