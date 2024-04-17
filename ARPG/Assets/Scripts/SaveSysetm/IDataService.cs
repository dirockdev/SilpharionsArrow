using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IDataService
{
    bool SaveData<T>(string RealtivePath, T Data, bool Encrypted);
    T LoadData<T>(string RealtivePath, bool Encrypted);
}
