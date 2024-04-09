using UnityEngine;
using System.Collections.Generic;
using System.Collections;

public class AudioManager : MonoBehaviour
{
    public static AudioManager instance;
    public AudioClip[] audioSources;
    public AudioSource musicSource;
    public GameObject sfxPrefab; // Prefab para instanciar efectos de sonido

    public Dictionary<string, AudioClip> soundDictionary = new Dictionary<string, AudioClip>();

    private void Awake()
    {
        if (instance == null)
            instance = this;
        else if (instance != this)
            Destroy(gameObject);

        DontDestroyOnLoad(gameObject);

        // Ejemplo: a�adir sonidos al diccionario
        AddSound("backgroundMusic", musicSource.clip);
        // Agrega m�s sonidos seg�n sea necesario
    }
    private void Start()
    {
        for (int i = 0; i < audioSources.Length; i++)
        {

            AddSound(i.ToString(), audioSources[i]);
        }
    }
    // M�todo para a�adir sonidos al diccionario
    public void AddSound(string soundName, AudioClip soundClip)
    {
        if (!soundDictionary.ContainsKey(soundName))
            soundDictionary.Add(soundName, soundClip);
    }

    // M�todo para reproducir m�sica de fondo por nombre
    public void PlayMusic(string soundName)
    {
        if (soundDictionary.ContainsKey(soundName))
        {
            musicSource.clip = soundDictionary[soundName];
            musicSource.Play();
        }
        else
        {
            Debug.LogWarning("No se encontr� el sonido con el nombre: " + soundName);
        }
    }


    // M�todo para reproducir efectos de sonido por nombre en una posici�n espec�fica
    public void PlaySFXWorld(string soundName, Vector3 position=default, float timeAlive=0.5f, float volume=0.2f)
    {
        if (soundDictionary.ContainsKey(soundName))
        {
            GameObject sfxInstance = ObjectPoolManager.SpawnObject(sfxPrefab, position, Quaternion.identity);
            
            AudioSource sfxAudioSource = sfxInstance.GetComponent<AudioSource>();
            if (sfxAudioSource != null)
            {
                sfxAudioSource.clip = soundDictionary[soundName];
                sfxAudioSource.volume = volume;
                sfxAudioSource.Play();
            }
            else
            {
                Debug.LogWarning("El prefab de efectos de sonido no tiene un componente AudioSource.");
            }
            StartCoroutine(ReturnToPool(sfxInstance,timeAlive));
        }
        else
        {
            Debug.LogWarning("No se encontr� el sonido con el nombre: " + soundName);
        }

    }
    public IEnumerator ReturnToPool(GameObject gameObject, float timeAlive)
    {
        float elapsedTime = 0f;
        while (elapsedTime < timeAlive)
        {
            elapsedTime += Time.deltaTime;
            yield return null;
        }
        ObjectPoolManager.ReturnObjectToPool(gameObject);
    }
}
