using System;
using UnityEngine;
using UnityEngine.UI;

public class PanelView : View
{
    [SerializeField] private Button _windowButton;

    public event Action WindowButtonPressed;

    private void OnButtonClick() => WindowButtonPressed?.Invoke();

    private void OnEnable() => _windowButton.onClick.AddListener(OnButtonClick);

    private void OnDisable() => _windowButton.onClick.RemoveListener(OnButtonClick);
}
