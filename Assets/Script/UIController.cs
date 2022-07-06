using System.Collections;
using DG.Tweening;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UIController : MonoBehaviour
{
    public enum ViewLives
    {
        Full,
        NotFull,
        NoLives
    }
    [SerializeField] private GameObject _fullLives;
    [SerializeField] private GameObject _notFullLives;
    [SerializeField] private GameObject _noLives;
    [SerializeField] private GameObject _dailyBonus;

    [SerializeField] private TextMeshProUGUI[] _timerHealth;
    [SerializeField] private TextMeshProUGUI[] _countHealth;

    [SerializeField] private RectTransform _openPosition;
    [SerializeField] private RectTransform _closePosition;

    [SerializeField] private Image _backGround;

    [HideInInspector] public bool openMenu;
    private ViewLives _currentView;
    private Sequence _queue;
    private GameObject _currentMenu;

    private const float DURATION_FADE=1f;
    private const float DURATION_MOVE=1f;
    private void Start()
    {
        _currentMenu=_dailyBonus;
        _queue  = DOTween.Sequence();
    }
    public void SelectMenu(ViewLives selectedView)
    {
        if (_currentView!=selectedView)
        {
            _queue.Append(_currentMenu.transform.DOMove(_closePosition.position, DURATION_MOVE));
        }
        if (openMenu == false)
        {
            _backGround.gameObject.SetActive(true);
            _backGround.DOFade(0.5f,DURATION_FADE);
        }
        openMenu = true;
        switch (selectedView)
        {
            case ViewLives.Full:
                _currentMenu = _fullLives;
                break;
            case ViewLives.NotFull:
                _currentMenu = _notFullLives;
                break;
            case ViewLives.NoLives:
                _currentMenu = _noLives;
                break;
        }
        _queue.Append(_currentMenu.transform.DOMove(_openPosition.position, DURATION_MOVE));
        _currentView = selectedView;
    }
    public void CloseMenu()
    {
        openMenu = false;
        _queue.Append(_currentMenu.transform.DOMove(_closePosition.position, DURATION_MOVE));
        _backGround.DOFade(0f,DURATION_FADE);
        StartCoroutine(WaiterToClose());
        IEnumerator WaiterToClose()
        {
            yield return new WaitForSeconds(DURATION_FADE);
            _backGround.gameObject.SetActive(false);
        }
    }
    public void UpdateTimerAmoung(string time)
    {
        foreach (var timer in _timerHealth)
        {
            timer.text = time;
        }
    }
    public void UpdateCountHealth(string amount)
    {
        foreach (var health in _countHealth)
        {
            health.text = amount;
        }
    }

}
