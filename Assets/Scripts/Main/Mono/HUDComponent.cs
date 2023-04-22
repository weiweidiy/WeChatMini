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
    /// ��������λ��
    /// </summary>
    void UpdateNamePosition()
    {
        ////ȡģ������������е���������
        //m_position = obj.transform.position;
        ////ת��Ϊ�����������Ļ����
        //m_position = mainCamera.WorldToScreenPoint(m_position);
        ////�õõ�����Ļ���꣬��UI�������ת��Ϊ��������
        //m_position = uiCamera.ScreenToWorldPoint(m_position);
        //m_position.z = 0f;
        //m_position.y += 0.1f;
        //lblName.transform.position = m_position;

        RectTransformUtility.WorldToScreenPoint(Camera.main, obj.transform.position);
    }
}


