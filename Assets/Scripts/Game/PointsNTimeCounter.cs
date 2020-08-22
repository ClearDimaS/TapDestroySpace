using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PointsNTimeCounter : MonoBehaviour
{
    #region Singleton

    public static PointsNTimeCounter instance;

    private void Awake()
    {
        if (instance == null)
            instance = this;
        else
            Destroy(gameObject);
    }
    #endregion

    [SerializeField]
    GameStateController gameStateController;

    [SerializeField]
    BonusSpawner bonusSpawner;

    [SerializeField]
    PlayerUI playerUI;

    [SerializeField]
    int StartPointsAmmount;

    [SerializeField]
    int tapStreakForBonus;

    int tapStreakCounter;

    private int points;

    internal int GetPoints()
    {
        return points;
    }

    private int pointsToWin;

    private float timeLeft;

    private void Start()
    {
        Bonus.OnBonusTappedCustom += AddTime;
        Enemy.OnEnemyOutOfBounds += SubtractPoints;
        Enemy.OnEnemyClicked += AddPoints;
    }
    private void OnEnable()
    {
        if (LevelController.instance != null)
        {
            ResetTime();
            ResetPoints();
            ResetTapStreak();
            UpdateTime();
            UpdatePoints(false);
            UpdateTapStreak(false);
        }
    }

    private void FixedUpdate()
    {
        SubtractTime();
        UpdateTime();
    }

    private void SubtractTime()
    {
        timeLeft -= Time.deltaTime;
    }

    public void AddTime(float ammountToAdd)
    {
        timeLeft += ammountToAdd;
        playerUI.UpdateTime(timeLeft, true);
    }

    private void UpdateTime()
    {
        if (timeLeft >= 0)
            playerUI.UpdateTime(timeLeft, false);
        else
            gameStateController.gameOver(false);
    }

    private void ResetTime()
    {
        timeLeft = LevelController.instance.GetCurrentLevelTotalTime();
    }

    private void SubtractPoints(int ammount)
    {
        points -= ammount;

        UpdatePoints(true);
    }

    public void AddPoints(int ammount)
    {
        points += ammount;

        AddTapStreak();

        UpdatePoints(true);
    }

    void UpdatePoints(bool ifAnimate)
    {
        playerUI.UpdatePoints(points, pointsToWin, ifAnimate);

        if (points <= 0) 
        {
            gameStateController.gameOver(false);
            points = pointsToWin - 1;
        }
        if (points >= pointsToWin) 
        {
            gameStateController.gameOver(true);
            points = pointsToWin - 1;
        }
    }

    private void ResetPoints()
    {
        pointsToWin = LevelController.instance.GetCurrentLevelPointsToWin();
        points = StartPointsAmmount;
    }

    public void AddTapStreak()
    {
        tapStreakCounter++;
        UpdateTapStreak(true);
    }

    private void UpdateTapStreak(bool ifAnimate)
    {
        playerUI.UpdateTapStreak(tapStreakCounter, tapStreakForBonus, ifAnimate);
        if (tapStreakCounter == tapStreakForBonus)
        {
            ResetTapStreak();
            bonusSpawner.ActivateBonus();
        }
    }

    public void ResetTapStreak()
    {
        tapStreakCounter = 0;
        playerUI.UpdateTapStreak(tapStreakCounter, tapStreakForBonus, false);
    }
}
