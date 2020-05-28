using UnityEngine;
using System.Collections;

public class FSingleton<T> where T : class,new() {

    protected static T instance = null;
    public static T GetInstance()
    {
        if (instance == null)
        {
            instance = new T();
        }
        return instance;
    }
}
