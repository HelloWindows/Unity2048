using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static partial class Global
{
    public static bool Interactable
    {
        get
        {
            if (GameMode == GameMode.Interactive &&
                GameStatus == GameStatus.Operating) return true;
            // end if
            return false;
        }
    }
} // end class Global
