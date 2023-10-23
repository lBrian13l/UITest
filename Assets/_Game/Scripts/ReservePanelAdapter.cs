using System;
using ConsumableTypes = GameModel.ConsumableTypes;

public class ReservePanelAdapter
{
    private ReservePanelView _view;

    public event Action WindowButtonPressed;

    public void Init(ReservePanelView view)
    {
        _view = view;
        view.Init(this);
        view.WindowButtonPressed += OnWindowButtonPressed;
        view.UpdateReserves(GameModel.GetConsumableCount(ConsumableTypes.Medpack), GameModel.GetConsumableCount(ConsumableTypes.ArmorPlate));
        GameModel.ModelChanged += OnModelChanged;
    }

    private void OnWindowButtonPressed() => WindowButtonPressed?.Invoke();

    private void OnModelChanged() => _view.UpdateReserves(GameModel.GetConsumableCount(ConsumableTypes.Medpack), GameModel.GetConsumableCount(ConsumableTypes.ArmorPlate));
}
