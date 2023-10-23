using System;
using UnityEngine;

public abstract class View : MonoBehaviour
{
    public event Action Showed;
    public event Action Hided;

    public virtual void Show()
    {
        gameObject.SetActive(true);
        Showed?.Invoke();
    }

    public void Hide()
    {
        gameObject.SetActive(false);
        Hided?.Invoke();
    }
}
