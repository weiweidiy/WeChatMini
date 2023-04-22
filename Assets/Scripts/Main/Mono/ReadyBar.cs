using Timers;
using TMPro;
using TowerDefense.Level;
using TowerDefense.UI.HUD;
using UnityEngine;

public class ReadyBar : MonoBehaviour
{
    public TextMeshProUGUI txtBar;

    uint sec = 5;

    private void Awake()
    {
        GameUI.instance.stateChanged += Instance_stateChanged;

        SetText("�෢�𹥻�ʣ�ࣺ" + sec.ToString() + "��");

        Timer timer = new Timer(1f, sec, () =>
        {
            sec--;
            SetText("�෢�𹥻�ʣ�ࣺ" + sec.ToString() + "��");

            if (sec == 0)
            {
                if (LevelManager.instanceExists)
                {
                    LevelManager.instance.BuildingCompleted();
                }
                else
                {
                    Debug.LogError("�ؿ�������");
                }
                SetActive(false);
            }

        });
        TimersManager.SetTimer(this, timer);
    }

    void SetText(string content)
    {
        txtBar.text = content;
    }

    void SetActive(bool active)
    {
        txtBar.transform.parent.gameObject.SetActive(active);
    }

    private void Instance_stateChanged(GameUI.State arg1, GameUI.State arg2)
    {
        if (arg2 == GameUI.State.Normal)
        {
            sec = 5;
            SetText("�෢�𹥻�ʣ�ࣺ" + sec.ToString() + "��");
            Timer timer = new Timer(1f, sec, () =>
            {
                sec--;
                SetText("�෢�𹥻�ʣ�ࣺ" + sec.ToString() + "��");

                if (sec == 0)
                {
                    if (LevelManager.instanceExists)
                    {
                        LevelManager.instance.BuildingCompleted();
                    }
                    else
                    {
                        Debug.LogError("�ؿ�������");
                    }
                }

            });
            TimersManager.SetTimer(this, timer);

            SetActive(true);
        }
        else
        {
            SetActive(false);
        }
    }
}
