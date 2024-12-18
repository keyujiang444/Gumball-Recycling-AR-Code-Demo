using System;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class VSWall : MonoBehaviour
{
    public GameObject vsWall;
    public Button vs1Btn;
    public Button vs2Btn;

    void Start()
    {
        vs1Btn.onClick.AddListener(OnVsClick);
        vs2Btn.onClick.AddListener(OnVsClick);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            vsWall.SetActive(true);
        }
    }

    private void OnVsClick()
    {
        SceneManager.LoadScene("DragonBattle");
    }
}