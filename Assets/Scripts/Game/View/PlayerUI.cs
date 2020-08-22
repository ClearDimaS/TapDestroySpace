using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class PlayerUI : MonoBehaviour
{
    [SerializeField]
    Text TimeText;

    [SerializeField]
    Text PointsText;

    [SerializeField]
    Image TapStreakImage;

    [SerializeField]
    Text TapStreakText;

    public void UpdatePoints(float points, float PointsToWin, bool ifAnimate)
    {
        if(ifAnimate)
            UpdateTextAnim(PointsText);
        PointsText.text = ((int)points).ToString() + " / " + PointsToWin;
    }

    public void UpdateTime(float time, bool ifAnimate)
    {
        if (ifAnimate)
            UpdateTextAnim(TimeText);
        TimeText.text = ((int)time).ToString();
    }

    public void UpdateTapStreak(int points, int MaxPoints, bool ifAnimate)
    {
        if (ifAnimate)
            UpdateTextAnim(TapStreakText);
        TapStreakText.text = ((int)points).ToString() + " / " + MaxPoints;
        TapStreakImage.fillAmount = (float)points / (float)MaxPoints;
    }

    void UpdateTextAnim(Text text) 
    {
        text.gameObject.GetComponent<Animator>().Play(Constants.ANIM_TEXT_UPD);
    }
}
