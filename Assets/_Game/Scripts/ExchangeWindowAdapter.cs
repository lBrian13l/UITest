using System;

public class ExchangeWindowAdapter
{
    private ExchangeWindowView _view;

    public event Action ViewShowed;
    public event Action ViewHided;

    public void Init(ExchangeWindowView view)
    {
        _view = view;
        view.Init(this, GameModel.CoinToCreditRate);
        view.Showed += OnShowed;
        view.Hided += OnHided;
        GameModel.ModelChanged += OnModelChanged;
    }

    public void Show()
    {
        _view.UpdateCurrencies(GameModel.CoinCount, GameModel.CreditCount);
        _view.UpdateOutput(_view.ExchangeAmount * GameModel.CoinToCreditRate);
        _view.Show();
    }

    public void Hide() => _view.Hide();

    public void OnInputAmountChanged(string inputAmount)
    {
        if (String.IsNullOrEmpty(inputAmount))
            inputAmount = "0";

        int outputAmount = Int32.Parse(inputAmount) * GameModel.CoinToCreditRate;
        _view.UpdateOutput(outputAmount);
    }

    public void OnExchange()
    {
        int exchangeAmount = _view.ExchangeAmount;
        GameModel.ConvertCoinToCredit(exchangeAmount);
    }

    private void OnShowed() => ViewShowed?.Invoke();

    private void OnHided() => ViewHided?.Invoke();

    private void OnModelChanged() => _view.UpdateCurrencies(GameModel.CoinCount, GameModel.CreditCount);
}
