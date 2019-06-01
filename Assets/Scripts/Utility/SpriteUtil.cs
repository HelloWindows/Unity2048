using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpriteUtil {

    private static Sprite[] numberSprites;
    public static Sprite[] NumberSprites
    {
        get
        {
            if (null == numberSprites) numberSprites = Resources.LoadAll<Sprite>("numTile");
            // end if
            return numberSprites;
        }
    }

    private static Sprite bgSprite;
    public static Sprite BgSprite
    {
        get
        {
            if(null == bgSprite) bgSprite = Resources.Load<Sprite>("panel");
            return bgSprite;
        }
    }

    public static Sprite GetSprite(int number)
    {
        if (0 == number) return null;
        // end if
        for (int i = 1; i < NumberSprites.Length; i++)
        {
            if (number == GetMultiple(i))
                return NumberSprites[i];
        } // end for
        return null;
    } // end GetSprite

    private static int GetMultiple(int index)
    {
        if (index > 1) return 2 * GetMultiple(index - 1);
        // end if
        return 2;
    } // end GetMultiple
} // end class SpriteUtil
