using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TimerController : MonoBehaviour
{
    public static TimerController instance;
    private bool gameActive;
    public float timer;
    public AudioSource BGM;
    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(gameObject);
        }

    }
    void Start()
    {
        gameActive = true;
    }

    // Update is called once per frame
    void Update()
    {
        if (gameActive)
        {
            timer += Time.deltaTime;
            UIController.Instance.UpdateTimer(timer);
        }
    }
    public void EndTimer()
    {
        gameActive = false;
        BGM.Stop();
        StartCoroutine(SetEndTimer());
    }
    private IEnumerator SetEndTimer()
    {
        yield return new WaitForSeconds(1f);
        float m = Mathf.FloorToInt(timer / 60);
        float s = Mathf.FloorToInt(timer % 60);
        UIController.Instance.EndPanel.SetActive(true);
        UIController.Instance.SurviveTimeText.text = m.ToString() + "·Ö" + s.ToString("00") + "Ãë";
        Time.timeScale = 0;
    }
}
