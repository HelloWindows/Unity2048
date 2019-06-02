using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PathUtil  {

    public static string PersistentDataPath
    {
        get
        {
#if UNITY_EDITOR
            return Application.dataPath + "/Persistent";
#else
            return Application.persistentDataPath;
#endif
        }
    }

    public static string CurrentRecordPath
    {
        get
        {
             return PersistentDataPath + "/current.bin";
        }
    }

    public static string RecordListPath
    {
        get
        {
            return PersistentDataPath + "/record_list.bin";
        }
    }

    private static string[] recordArr;

    public static string GetRecordPathWithIndex(int index)
    {
        if (null == recordArr)
        {
            recordArr = new string[ValueUtil.MaxRecordCount];
            for (int i = 0; i < recordArr.Length; i++)
            {
                recordArr[i] = string.Format(PersistentDataPath + "/record_{0}.bin", i);
            } // end for
        } // end if
        if (index < 0 || index >= recordArr.Length) return string.Empty;
        // end if
        return recordArr[index];
    } // end GetRecordPathWithIndex
} // end class PathUtil
