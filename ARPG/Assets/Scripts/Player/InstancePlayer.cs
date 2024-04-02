
using UnityEngine;

public class InstancePlayer : MonoBehaviour
{
    static InstancePlayer _instance;
    public static InstancePlayer instance
    {
        get
        {
            return _instance;
        }
    }

    void Awake()
    {
        _instance = this;
    }

}