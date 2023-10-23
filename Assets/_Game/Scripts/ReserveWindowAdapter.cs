using System;
using ConsumableConfig = GameModel.ConsumableConfig;
using ConsumableTypes = GameModel.ConsumableTypes;

public struct ConsumablePrice
{
    private int _price;
    private bool _gold;

    public ConsumablePrice(int creditPrice, int coinPrice)
    {
        if (coinPrice >= creditPrice)
        {
            _price = coinPrice;
            _gold = true;
        }
        else
        {
            _price = creditPrice;
            _gold = false;
        }
    }

    public int Price => _price;
    public bool Gold => _gold;
}

public class ReserveWindowAdapter
{
    private ReserveWindowView _view;

    public event Action ViewShowed;
    public event Action ViewHided;

    public void Init(ReserveWindowView view)
    {
        _view = view;
        view.Init(this);
        view.Showed += OnShowed;
        view.Hided += OnHided;
        GameModel.ModelChanged += OnModelChanged;
    }

    public void Show()
    {
        _view.UpdateReserves(GameModel.GetConsumableCount(ConsumableTypes.Medpack), GameModel.GetConsumableCount(ConsumableTypes.ArmorPlate));
        _view.Show();
    }

    public void Hide() => _view.Hide();

    public void OnBuyMedpack() => OnBuyConsumable(ConsumableTypes.Medpack);

    public void OnBuyArmor() => OnBuyConsumable(ConsumableTypes.ArmorPlate);

    public ConsumablePrice GetConsumablePrice(ConsumableTypes type)
    {
        ConsumableConfig config = GetConsumableConfig(type);
        ConsumablePrice price = new ConsumablePrice(config.CreditPrice, config.CoinPrice);
        return price;
    }

    private void OnShowed() => ViewShowed?.Invoke();

    private void OnHided() => ViewHided?.Invoke();

    private void OnModelChanged() => _view.UpdateReserves(GameModel.GetConsumableCount(ConsumableTypes.Medpack), GameModel.GetConsumableCount(ConsumableTypes.ArmorPlate));

    private void OnBuyConsumable(ConsumableTypes type)
    {
        ConsumableConfig config = GameModel.ConsumablesPrice[type];

        if (config.CoinPrice >= config.CreditPrice)
            GameModel.BuyConsumableForGold(type);
        else
            GameModel.BuyConsumableForSilver(type);
    }

    private ConsumableConfig GetConsumableConfig(ConsumableTypes type) => GameModel.ConsumablesPrice[type];
}
