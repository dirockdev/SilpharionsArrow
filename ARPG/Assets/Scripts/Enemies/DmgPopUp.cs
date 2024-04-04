using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using DG.Tweening;

public class DmgPopUp : MonoBehaviour
{
    public TMP_Text text;

    private void OnEnable()
    {
        StartCoroutine(ReturnToPool());
    }
    public void Inicialize(int dmgText, bool crit, Color color)
    {
        text.SetText(dmgText.ToString());
        
        text.alpha = 1f;
        text.DOFade(0, 1f);
        text.rectTransform.DOAnchorPos3D(new Vector3(Random.Range(-15, 15), 10, 0), 1f);
        if (crit) text.color = Color.yellow;
        else text.color = color;

    }
    IEnumerator ReturnToPool()
    {
        float elapsedTime = 0f;
        while (elapsedTime < 1f)
        {
            elapsedTime += Time.deltaTime;
            yield return null;
        }
        ObjectPoolManager.ReturnObjectToPool(gameObject);
    }
}
