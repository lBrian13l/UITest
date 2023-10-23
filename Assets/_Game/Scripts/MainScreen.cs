using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

public class MainScreen : MonoBehaviour
{
    [SerializeField] private CurrencyPanelView _currencyPanel;
    [SerializeField] private ReservePanelView _reservePanel;
    [SerializeField] private ReserveWindowView _reserveWindow;
    [SerializeField] private ExchangeWindowView _exchangeWindow;
    [SerializeField] private Image _fade;

    private Sequence _fadeSequence;

    private void Awake()
    {
        CurrencyPanelAdapter currencyPanelAdapter = new CurrencyPanelAdapter();
        ReservePanelAdapter reservePanelAdapter = new ReservePanelAdapter();
        ReserveWindowAdapter reserveWindowAdapter = new ReserveWindowAdapter();
        ExchangeWindowAdapter exchangeWindowAdapter = new ExchangeWindowAdapter();

        currencyPanelAdapter.Init(_currencyPanel);
        reservePanelAdapter.Init(_reservePanel);
        reserveWindowAdapter.Init(_reserveWindow);
        exchangeWindowAdapter.Init(_exchangeWindow);

        currencyPanelAdapter.WindowButtonPressed += exchangeWindowAdapter.Show;
        reservePanelAdapter.WindowButtonPressed += reserveWindowAdapter.Show;

        reserveWindowAdapter.ViewShowed += FadeIn;
        reserveWindowAdapter.ViewHided += FadeOut;
        exchangeWindowAdapter.ViewShowed += FadeIn;
        exchangeWindowAdapter.ViewHided += FadeOut;
    }

    private void Update() => GameModel.Update();

    private void FadeIn()
    {
        if (_fadeSequence != null)
            _fadeSequence.Kill();

        _fadeSequence = DOTween.Sequence();
        _fade.enabled = true;
        _fadeSequence.Append(_fade.DOFade(0.25f, 0.1f));
    }

    private void FadeOut()
    {
        if (_fadeSequence != null)
            _fadeSequence.Kill();

        _fadeSequence = DOTween.Sequence();
        _fadeSequence.Append(_fade.DOFade(0f, 0.1f)).OnComplete(() => _fade.enabled = false);
    }
}
