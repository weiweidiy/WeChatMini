using System;
using Timers;
using TMPro;
using TowerDefense.Level;
using UnityEngine;

public class CountDown : MonoBehaviour
{
    public event Action onComplete;

    uint sec = 5000;

    public TextMeshProUGUI txtSec;

    Timer timer;
    // Start is called before the first frame update
    void Awake()
    {

        timer = new Timer(1f, 5000, () =>
        {
            sec--;
            txtSec.text = sec.ToString();

            if (sec == 0)
            {
                if (LevelManager.instanceExists)
                {
                    LevelManager.instance.BuildingCompleted();
                }
                else
                {
                    Debug.LogError("¹Ø¿¨²»´æÔÚ");
                }
                onComplete?.Invoke();
            }

        });
    }

    private void OnEnable()
    {
        sec = 5000;
        TimersManager.SetTimer(this, timer);
    }

    private void OnDisable()
    {

    }
}
