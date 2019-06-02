using System.IO;
using UnityEngine;
using UnityEngine.UI;

public class UISelectGameProgress : UIForm
{
    private GameObject m_GameObject;
    private const string m_PrefabPath = "UIGameProgress";

    public override void OnEnter()
    {
        GameObject prefab = Resources.Load<GameObject>(m_PrefabPath);
        m_GameObject = Object.Instantiate(prefab, UIManager.Instance.Canvas.transform);
        GameRecordList recordList = SerializeUtil.GetDataWithPath<GameRecordList>(PathUtil.RecordListPath);
        if (null == recordList) recordList = new GameRecordList();
        // end if
        for (int i = 0; i < 3; i++)
        {
            int index = i;
            m_GameObject.transform.Find("ProgressItem_" + i).GetComponent<Button>().onClick.AddListener(delegate () { OnSelect(index); });
            m_GameObject.transform.Find(string.Format("ProgressItem_{0}/Text", i)).GetComponent<Text>().text = recordList.GetRecordNameWithIndex(index);   
        } // end for
        m_GameObject.transform.Find("BackBtn").GetComponent<Button>().onClick.AddListener(OnClickBackBtn);
    } // end OnEnter

    public override void OnExit()
    {
        if (null == m_GameObject) return;
        // end if
        Object.Destroy(m_GameObject);
    } // end OnExit

    public override void OnUpdate()
    {

    } // end OnUpdate

    private void OnSelect(int index)
    {
        string path = PathUtil.GetRecordPathWithIndex(index);
        if (string.IsNullOrEmpty(path) || !File.Exists(path)) return;
        // end if
        GameManager.Instance.ContinueGame(path);
    } // end OnSelect

    private void OnClickBackBtn()
    {
        UIManager.Instance.OpenForm(new UIStartMenu());
    } // end OnClickBackBtn
} // end  class UISelectGameProgress
