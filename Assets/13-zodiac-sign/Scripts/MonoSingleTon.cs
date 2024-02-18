using UnityEngine;

public class MonoSingleTon<T> : MonoBehaviour where T : MonoBehaviour
{
    private static T instance = null;
    public static T Instance
    {
        get
        {
            if (instance == null)
            {
                instance = FindObjectOfType<T>(typeof(T) as T);

                if (instance == null)
                {
                    GameObject instObj = new(typeof(T).ToString(), typeof(T));
                    instObj.TryGetComponent(out T _instance);
                    instance = _instance;
                }
            }
            return instance;
        }
    }

    protected virtual void Awake()
    {
        if (FindObjectOfType<T>(typeof(T) as T).gameObject != this.gameObject) 
        {
            Destroy(this.gameObject);
        }
        else
        {
            DontDestroyOnLoad(this.gameObject);
        }
    }
}
