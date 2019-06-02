using System;

[Serializable]
public class GameRecordList {

    private readonly string[] m_NameArr;

    public GameRecordList()
    {
        m_NameArr = new string[ValueUtil.MaxRecordCount];
    }

    public string GetRecordNameWithIndex(int index)
    {
        if(null == m_NameArr || index < 0 || 
            index >= m_NameArr.Length || string.IsNullOrEmpty(m_NameArr[index])) return "空白";
        // end if
        return m_NameArr[index];
    } // end GetRecordNameWithIndex

    public void SetRecordNameWithIndex(int index, string name)
    {
        if (null == m_NameArr || index < 0 || index >= m_NameArr.Length) return;
        // end if
        m_NameArr[index] = name;
    } // end SetRecordNameWithIndex
} // end GameRecordList
