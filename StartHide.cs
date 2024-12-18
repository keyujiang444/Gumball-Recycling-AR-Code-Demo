using System.Collections;
using UnityEngine;

public class StartHide : MonoBehaviour
{
    public float delay = 4;
    
    IEnumerator Start()
    {
        yield return  new WaitForSeconds(delay);
        gameObject.SetActive(false);
    }

}
