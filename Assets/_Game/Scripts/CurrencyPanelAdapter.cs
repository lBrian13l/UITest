using System;

public class CurrencyPanelAdapter
{
    private CurrencyPanelView _view;

    public event Action WindowButtonPressed;

    public void Init(CurrencyPanelView view)
    {
        _view = view;
        view.Init(this);
        view.WindowButtonPressed += OnWindowButtonPressed;
        view.UpdateCurrencies(GameModel.CoinCount, GameModel.CreditCount);
        GameModel.ModelChanged += OnModelChanged;
    }

    private void OnWindowButtonPressed() => WindowButtonPressed?.Invoke();

    private void OnModelChanged() => _view.UpdateCurrencies(GameModel.CoinCount, GameModel.CreditCount);
}
