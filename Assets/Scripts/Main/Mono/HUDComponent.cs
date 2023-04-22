using TMPro;
using UnityEngine;

public class HUDComponent : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField]
    TextMeshProUGUI lblName;

    [SerializeField]
    GameObject obj;

    [SerializeField]
    Camera mainCamera;

    [SerializeField]
    Camera uiCamera;

    public void Start()
    {
        //mainCamera = Camera.main;
        //uiCamera = GameObject.Find("UICamera").GetComponent<Camera>();
    }

    void Update()
    {
        UpdateNamePosition();
    }

    Vector3 m_position;
    /// <summary>
    /// 更新名字位置
    /// </summary>
    void UpdateNamePosition()
    {
        ////取模型在主摄像机中的世界坐标
        //m_position = obj.transform.position;
        ////转换为主摄像机的屏幕坐标
        //m_position = mainCamera.WorldToScreenPoint(m_position);
        ////用得到的屏幕坐标，在UI摄像机中转换为世界坐标
        //m_position = uiCamera.ScreenToWorldPoint(m_position);
        //m_position.z = 0f;
        //m_position.y += 0.1f;
        //lblName.transform.position = m_position;

        RectTransformUtility.WorldToScreenPoint(Camera.main, obj.transform.position);
    }
}


