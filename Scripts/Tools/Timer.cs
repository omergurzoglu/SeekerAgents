using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.PlayerLoop;
using UnityEngine.UI;

public class Timer : MonoBehaviour 
{
    [SerializeField] private Image uiFill;
    [SerializeField] private TextMeshProUGUI uiText;
    public int duration;
    private int _remainingDuration;
    private bool _pause;
    private void Start() => Being(duration);

    private void Being(int second)
    {
        _remainingDuration = second;
        StartCoroutine(UpdateTimer());
    }

    private IEnumerator UpdateTimer()
    {
        while(_remainingDuration >= 0)
        {
            if (!_pause)
            {
                uiText.text = $"{_remainingDuration / 60:00}:{_remainingDuration % 60:00}";
                uiFill.fillAmount = Mathf.InverseLerp(0, duration, _remainingDuration);
                _remainingDuration--;
                yield return new WaitForSeconds(1f);
            }
            TimeUp();
            yield return null;
        }
    }

    private void TimeUp()
    {
        StatusCheck.Instance.ResetGame();
    }

   
}