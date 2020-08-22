using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Bonus : MonoBehaviour
{
    [SerializeField]
    private Image MyImage;

    private BonusData data;

    private float addTime;

    bool isDestroyed;
    public void Init(BonusData _data)
    {
        isDestroyed = false;
        data = _data;
        MyImage.sprite = data.MainSprite;
        addTime = data.AddTime;
    }

    public static Action<GameObject> OnBonusTappedQueue;
    public static Action<float> OnBonusTappedCustom;

    public void OnClick()
    {
        if (isDestroyed == false)
        {
            isDestroyed = true;
            AudioManager.instance.Play(Constants.SND_BONUS);
            FireEvents();
        }
    }

    private void FireEvents()
    {
        OnBonusTappedQueue?.Invoke(gameObject);
        OnBonusTappedCustom?.Invoke(addTime);
    }
}
