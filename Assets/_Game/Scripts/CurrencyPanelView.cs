using UnityEngine;
using TMPro;

public class CurrencyPanelView : PanelView
{
    [SerializeField] private TextMeshProUGUI _coins;
    [SerializeField] private TextMeshProUGUI _credits;

    private CurrencyPanelAdapter _adapter;

    public void Init(CurrencyPanelAdapter adapter)
    {
        _adapter = adapter;
    }

    public void UpdateCurrencies(int coins, int credits)
    {
        _coins.text = coins.ToString();
        _credits.text = credits.ToString();
    }
}
