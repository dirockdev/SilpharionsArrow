
using System.IO;
using UnityEngine;
using Newtonsoft.Json;
using System;

public class JsonDataService : IDataService
{
    public T LoadData<T>(string RealtivePath, bool Encrypted)
    {
        string path = Application.persistentDataPath + RealtivePath;
        if (!File.Exists(path)) // Comprobar si el archivo existe
        {
            Debug.LogError("File not found: " + path); // Registro de error
            return default; // Devolver un valor predeterminado o nulo
        }

        try
        {
            T data = JsonConvert.DeserializeObject<T>(File.ReadAllText(path));
            return data;
        }
        catch (Exception e)
        {
            Debug.LogError("Error loading data: " + e.Message); // Registro de error
            return default; // Devolver un valor predeterminado o nulo
        }
    }


    public bool SaveData<T>(string RealtivePath, T Data, bool Encrypted)
    {
        string path=Application.persistentDataPath + RealtivePath;

        try
        {
            if(File.Exists(path))
            {
                File.Delete(path);
            }
            else
            {
                Debug.Log("Writing file first time");
            }
            using FileStream stream = File.Create(path);
            stream.Close();
            File.WriteAllText(path, JsonConvert.SerializeObject(Data));
            return true;
        }
        catch (Exception)
        {
            return false;
        }

    }

    
}
