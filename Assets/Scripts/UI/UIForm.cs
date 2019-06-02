using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class UIForm 
{
    public abstract void OnEnter();
    public virtual void OnUpdate()
    {

    } // end Update
    public abstract void OnExit();
} // end class UIForm
