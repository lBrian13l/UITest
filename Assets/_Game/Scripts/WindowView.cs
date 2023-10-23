using UnityEngine;
using UnityEngine.UI;

public abstract class WindowView : View
{
    [SerializeField] private Button _closeButton;

    private void OnEnable()
    {
        _closeButton.onClick.AddListener(Hide);
    }

    private void OnDisable()
    {
        _closeButton.onClick.RemoveListener(Hide);
    }
}
