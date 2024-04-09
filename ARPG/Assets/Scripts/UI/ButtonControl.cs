
using UnityEngine;
using UnityEngine.UI;

public class ButtonControl : MonoBehaviour
{
    [SerializeField]
    string sound;
    private void Awake()
    {
        GetComponent<Button>().onClick.AddListener(() => AudioManager.instance.PlaySFXWorld(sound, transform.position));
    }
}
