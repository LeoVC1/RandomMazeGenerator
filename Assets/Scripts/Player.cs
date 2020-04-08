using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Player : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Star"))
        {
            GameManager.instance.imgFadeout.gameObject.SetActive(true);
            GameManager.instance.textMeshPro.gameObject.SetActive(true);
        }
    }
}
