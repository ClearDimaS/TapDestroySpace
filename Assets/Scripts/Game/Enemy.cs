using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Enemy : MonoBehaviour
{
    [SerializeField]
    private Image MyImage;

    private EnemyData data;

    private int attack;

    bool isDestroyed;

    Vector3 direction;
    public void Init(EnemyData _data, bool _moveRight) 
    {
        isDestroyed = false;
        data = _data;
        //MyImage.sprite = data.MainSprite;
        attack = data.Attack;
        direction = _moveRight? Vector3.right : Vector3.left;
    }

    public static Action<GameObject> OnEnemyOutOfBoundsQueue;
    public static Action<int> OnEnemyClicked;
    public static Action<int> OnEnemyOutOfBounds;

    private void FixedUpdate()
    {
        if(isDestroyed == false)
            transform.Translate(direction * data.Speed * Time.deltaTime);

        if (ScreenViewManager.instance.IsInsideBounds(transform.position, 0.2f) == false)
            FireEvents(true);
    }

    public void OnClick()
    {
        if (isDestroyed == false) 
        {
            isDestroyed = true;
            OnEnemyClicked(attack);
            AudioManager.instance.Play(Constants.SND_ASTEROID_EXPLOSION);
            GetComponent<Animator>().Play(Constants.ANIM_ENEMY_EXPLOSION);
            Invoke("DelayComingBack", 0.4f);
        }
    }

    void DelayComingBack() 
    {
        FireEvents(false);
        isDestroyed = false;
    }

    private void FireEvents(bool ifEscaped)
    {
        OnEnemyOutOfBoundsQueue(gameObject);
        if(ifEscaped)
            OnEnemyOutOfBounds(attack);
    }
}
