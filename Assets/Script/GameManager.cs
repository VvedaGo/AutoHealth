using System;
using System.Collections;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    [SerializeField] private UIController _uiController;
    [SerializeField] private Health _health;

    private Coroutine _timerHealth;

    private const int TIME_TO_NEW_HEALTH=20;
    private int _leftTimeToNewHealth=20;
    private void Start()
    {
        _leftTimeToNewHealth = TIME_TO_NEW_HEALTH;
    }
    public void OpenMenu()
    {
        _uiController.SelectMenu(currentView(_health.countHealth));
    }
    public void UseLife()
    {
        _health.LoseHealth();
        CheckNextHealth();
        OpenMenu();
    }
    public void RefillLife()
    {
        while (_health.countHealth < _health.maxHealthCount)
        {
            _health.GetHealth();
        }
        StopCoroutine(_timerHealth);
        _timerHealth = null;
        CheckNextHealth();
        OpenMenu();
    }
   private UIController.ViewLives currentView(int amountHealth)
    {
        switch (amountHealth)
        {
            case 0:
                return UIController.ViewLives.NoLives;
                break;
            case 5:
                return UIController.ViewLives.Full;
                break;
            default:
                return UIController.ViewLives.NotFull;
                break;
        }
    }

    private void CheckNextHealth()
    {
       _uiController.UpdateCountHealth(_health.countHealth.ToString());
        if (_health.countHealth == _health.maxHealthCount)
        {
            _uiController.UpdateTimerAmoung("FULL");
        }
        else
        {
            _timerHealth ??= StartCoroutine(TimerHealth());
        }
        if (_uiController.openMenu)
        {
            OpenMenu();
        }
    }

    private IEnumerator TimerHealth()
    {
        _leftTimeToNewHealth = TIME_TO_NEW_HEALTH;
       for(int i = 0; i < TIME_TO_NEW_HEALTH; i++)
       {
           SetTime();
           yield return new WaitForSecondsRealtime(1f);
           _leftTimeToNewHealth--;
       }
       _health.GetHealth();
       _timerHealth = null;
       CheckNextHealth();
   }

    private void SetTime()
    {
        string minutes;
        minutes = _leftTimeToNewHealth < 10 ? $"{_leftTimeToNewHealth / 60}0" : $"{_leftTimeToNewHealth / 60}";
        string seconds;
        seconds = _leftTimeToNewHealth < 10 ? $"0{_leftTimeToNewHealth % 60}" : $"{_leftTimeToNewHealth % 60}";
        string time = $"{minutes}:{seconds}";
        _uiController.UpdateTimerAmoung(time);
    }
}
