using System.IO;
using UnityEngine;
using UnityEngine.UI;

public class UISaveGameProgress : UIForm
{
    private GameObject m_GameObject;
    private readonly GameRecordList m_RecordList;
    private const string m_PrefabPath = "UIGameProgress";

    public UISaveGameProgress()
    {
        m_RecordList = SerializeUtil.GetDataWithPath<GameRecordList>(PathUtil.RecordListPath);
        if (null == m_RecordList) m_RecordList = new GameRecordList();
        // end if
    } // end UISaveGameProgress

    public override void OnEnter()
    {
        Global.GameStatus = GameStatus.Termination;
        GameObject prefab = Resources.Load<GameObject>(m_PrefabPath);
        m_GameObject = Object.Instantiate(prefab, UIManager.Instance.Canvas.transform);
        // end if
        for (int i = 0; i < 3; i++)
        {
            int index = i;
            m_GameObject.transform.Find("ProgressItem_" + i).GetComponent<Button>().onClick.AddListener(delegate () { OnSelect(index); });
            m_GameObject.transform.Find(string.Format("ProgressItem_{0}/Text", i)).GetComponent<Text>().text = m_RecordList.GetRecordNameWithIndex(index);   
        } // end for
        m_GameObject.transform.Find("BackBtn").GetComponent<Button>().onClick.AddListener(OnClickBackBtn);
    } // end OnEnter

    public override void OnExit()
    {
        if (null == m_GameObject) return;
        // end if
        Object.Destroy(m_GameObject);
    } // end OnExit

    private void OnSelect(int index)
    {
        string path = PathUtil.GetRecordPathWithIndex(index);
        if (string.IsNullOrEmpty(path) || !File.Exists(PathUtil.CurrentRecordPath)) return;
        // end if
        File.Copy(PathUtil.CurrentRecordPath, path, true);
        m_RecordList.SetRecordNameWithIndex(index, System.DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"));
        SerializeUtil.DataSaveWithPath(m_RecordList, PathUtil.RecordListPath);
        GameManager.Instance.ContinueGame(PathUtil.CurrentRecordPath);
    } // end OnSelect

    private void OnClickBackBtn()
    {
        GameManager.Instance.ContinueGame(PathUtil.CurrentRecordPath);
    } // end OnClickBackBtn
} // end  class UISaveGameProgress
