using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using TMPro;

public class SceneController : MonoBehaviour
{
    [SerializeField]
    private Slider loadingSlider;
    [SerializeField]
    private TMP_Text progressText;    
    [SerializeField]
    private GameObject loadingUI;
    public Color startColor;
    public Color endColor;
    public void CloseApplication()
    {
        Application.Quit();
    }
    public void ChangeScene(bool loading)
    {
        SaveLoadManager.loadGame = loading;
        loadingUI.SetActive(true);
        StartCoroutine(LoadYourAsyncScene());
        
    }
   
    IEnumerator LoadYourAsyncScene()
    {
        

        AsyncOperation asyncLoad = SceneManager.LoadSceneAsync(2);

        // Wait until the asynchronous scene fully loads
        while (!asyncLoad.isDone)
        {
            float progress = Mathf.Clamp01(asyncLoad.progress / 0.9f); // asyncLoad.progress llega a 0.9 cuando la escena está casi cargada

            // Actualiza la barra de progreso y el texto
            loadingSlider.value = progress;
            progressText.SetText( $"Cargando: { progress * 100f}%");

            loadingSlider.fillRect.GetComponent<Image>().color = Color.Lerp(startColor, endColor, progress);


            yield return null;
        }
    }
    public void ToMenu()
    {
       
        SceneManager.LoadScene(1);

    }
}
