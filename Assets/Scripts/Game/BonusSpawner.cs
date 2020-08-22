using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BonusSpawner : SpawnPool<BonusDataBase, Bonus>
{
    [Tooltip("Class controlling game state")]
    [SerializeField] private GameStateController gameStateController;

    // Start is called before the first frame update
    protected override void Awake()
    {
        base.Awake();
        Bonus.OnBonusTappedQueue += ReturnObj;
        gameStateController.OnGameOver += ReturnAll;
    }
    public void ActivateBonus()
    {
        if (TryGetNextElementInTheQueue(out Bonus script)) 
        {
            script.Init(elementSettings.GetRandomElement());
            AudioManager.instance.Play(Constants.SND_BONUS_DROP);
            // Generation
            float yPos;
            float xPos;

            xPos = Random.Range(ScreenViewManager.instance.screenBorders[0], ScreenViewManager.instance.screenBorders[1]);
            yPos = Random.Range(ScreenViewManager.instance.screenBorders[2], ScreenViewManager.instance.screenBorders[3]);

            script.transform.position = new Vector2(xPos, yPos);
        }
    }
}
