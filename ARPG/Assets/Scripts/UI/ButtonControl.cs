
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class ButtonControl : MonoBehaviour, IPointerEnterHandler, ISelectHandler
{
    [SerializeField]
    string soundClick;
    [SerializeField]
    string soundPointer;
    [SerializeField]
    bool canSoundPointer;
    public void OnPointerEnter(PointerEventData eventData)
    {
        if(canSoundPointer)return;
        AudioManager.instance.PlaySFXWorld(soundPointer, transform.position);
    }

    public void OnSelect(BaseEventData eventData)
    {
        if (canSoundPointer) return;
        AudioManager.instance.PlaySFXWorld(soundClick, transform.position);
    }

    private void Awake()
    {
        GetComponent<Button>().onClick.AddListener(() => AudioManager.instance.PlaySFXWorld(soundClick, transform.position));
    }
  
}
