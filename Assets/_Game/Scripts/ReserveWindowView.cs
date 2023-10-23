using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System;
using ConsumableTypes = GameModel.ConsumableTypes;

public class ReserveWindowView : WindowView
{
    [SerializeField] private ReserveBuyPanel[] _buyPanels;
    [SerializeField] private Sprite _creditSprite;
    [SerializeField] private Sprite _coinSprite;
    [SerializeField] private Color _creditColor;
    [SerializeField] private Color _coinColor;

    private ReserveWindowAdapter _adapter;

    public void Init(ReserveWindowAdapter adapter)
    {
        _adapter = adapter;

        foreach (ReserveBuyPanel buyPanel in _buyPanels)
        {
            if (buyPanel.Type == ConsumableTypes.Medpack)
                buyPanel.BuyButton.onClick.AddListener(_adapter.OnBuyMedpack);
            else if (buyPanel.Type == ConsumableTypes.ArmorPlate)
                buyPanel.BuyButton.onClick.AddListener(_adapter.OnBuyArmor);
        }
    }

    public void UpdateReserves(int medpackAmount, int armorAmount)
    {
        GetBuyPanel(ConsumableTypes.Medpack).Amount.text = medpackAmount.ToString();
        GetBuyPanel(ConsumableTypes.ArmorPlate).Amount.text = armorAmount.ToString();
    }

    public override void Show()
    {
        UpdateCosts();

        base.Show();
    }

    private void UpdateCosts()
    {
        foreach (ReserveBuyPanel buyPanel in _buyPanels)
        {
            ConsumablePrice consumablePrice = _adapter.GetConsumablePrice(buyPanel.Type);
            buyPanel.Price.text = consumablePrice.Price.ToString();

            if (consumablePrice.Gold)
            {
                buyPanel.BuyButton.image.color = _coinColor;
                buyPanel.CostImage.sprite = _coinSprite;
            }
            else
            {
                buyPanel.BuyButton.image.color = _creditColor;
                buyPanel.CostImage.sprite = _creditSprite;
            }
        }
    }

    private ReserveBuyPanel GetBuyPanel(ConsumableTypes type)
    {
        foreach (ReserveBuyPanel buyPanel in _buyPanels)
        {
            if (buyPanel.Type == type)
                return buyPanel;
        }

        throw new Exception($"Type {type} not found");
    }

    private void OnDestroy()
    {
        foreach (ReserveBuyPanel buyPanel in _buyPanels)
        {
            if (buyPanel.Type == ConsumableTypes.Medpack)
                buyPanel.BuyButton.onClick.RemoveListener(_adapter.OnBuyMedpack);
            else if (buyPanel.Type == ConsumableTypes.ArmorPlate)
                buyPanel.BuyButton.onClick.RemoveListener(_adapter.OnBuyArmor);
        }
    }

    [Serializable]
    private struct ReserveBuyPanel
    {
        [SerializeField] private ConsumableTypes _type;
        [SerializeField] private TextMeshProUGUI _amount;
        [SerializeField] private TextMeshProUGUI _price;
        [SerializeField] private Button _buyButton;
        [SerializeField] private Image _costImage;

        public ConsumableTypes Type => _type;
        public TextMeshProUGUI Amount => _amount;
        public TextMeshProUGUI Price => _price;
        public Button BuyButton => _buyButton;
        public Image CostImage => _costImage;
    }
}
