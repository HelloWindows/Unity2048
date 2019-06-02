using System;

public class Singleton<T> : IDisposable where T : new()
{
    private static T instance;

    public static T Instance
    {
        get
        {
            if (instance == null) {
                instance = default(T);
                if (null == instance) instance = new T();
                // end if
            } // end if
            return instance;
        }
    }

    public virtual void Dispose()
    {
        instance = default(T);
    } // end Dispose

    protected Singleton()
    {
    } // end Singleton
} // end class Singleton