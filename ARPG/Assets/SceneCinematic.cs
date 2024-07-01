
using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using TMPro;

public class SceneCinematic : MonoBehaviour
{



    private void OnEnable()
    {
        SceneManager.LoadScene(1, LoadSceneMode.Single);
    }
}
