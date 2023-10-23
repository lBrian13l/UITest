using UnityEngine;
using TMPro;
using UnityEngine.UI;
using System;

public class ExchangeWindowView : WindowView
{
    [SerializeField] private TextMeshProUGUI _rate;
    [SerializeField] private TextMeshProUGUI _coins;
    [SerializeField] private TextMeshProUGUI _credits;
    [SerializeField] private TMP_InputField _inputField;
    [SerializeField] private TextMeshProUGUI _outputAmount;
    [SerializeField] private Button _exchangeButton;

    private ExchangeWindowAdapter _adapter;

    public int ExchangeAmount => String.IsNullOrEmpty(_inputField.text) ? 0 : Int32.Parse(_inputField.text);

    public void Init(ExchangeWindowAdapter adapter, int rate)
    {
        _adapter = adapter;
        _exchangeButton.onClick.AddListener(_adapter.OnExchange);
        _inputField.onValueChanged.AddListener(_adapter.OnInputAmountChanged);
        _rate.text = rate.ToString();
    }

    public void UpdateOutput(int output) => _outputAmount.text = output.ToString();

    public void UpdateCurrencies(int coins, int credits)
    {
        _coins.text = coins.ToString();
        _credits.text = credits.ToString();
    }

    private void OnDestroy() => _exchangeButton.onClick.RemoveListener(_adapter.OnExchange);
}
