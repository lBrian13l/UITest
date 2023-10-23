using UnityEngine;
using TMPro;

public class ReservePanelView : PanelView
{
    [SerializeField] private TextMeshProUGUI _medpacks;
    [SerializeField] private TextMeshProUGUI _armor;

    private ReservePanelAdapter _adapter;

    public void Init(ReservePanelAdapter adapter)
    {
        _adapter = adapter;
    }

    public void UpdateReserves(int medpackAmount, int armorAmount)
    {
        _medpacks.text = medpackAmount.ToString();
        _armor.text = armorAmount.ToString();
    }
}
