using UnityEngine;

public abstract class Singleton<T> : MonoBehaviour where T : MonoBehaviour
{
    public static T Instance { get; private set; }

    protected virtual void Awake()
    {
        if(Instance == null)
        {
            Instance = FindObjectOfType<T>();

            if(Instance == null) 
            {
                GameObject newInstance = new GameObject();
                Instance = newInstance.AddComponent<T>();
                newInstance.name = typeof(T).ToString();
            }
        }
    }

    protected virtual void OnDisable()
    {
        Instance = null;
        Destroy(gameObject);
    }
}

public abstract class PersistentSingleton<T> : Singleton<T> where T : MonoBehaviour
{
    protected override void Awake()
    {
        if(Instance != null)
        {
            Destroy(gameObject);
        }

        base.Awake();
    }

    protected override void OnDisable()
    {
        DontDestroyOnLoad(gameObject);

        base.OnDisable();
    }
}


