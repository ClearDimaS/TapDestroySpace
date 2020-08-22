using System.Collections;
using System.Collections.Generic;
using Unity.Collections;
using UnityEngine;

[CreateAssetMenu(menuName = "Databases/BonusDatabase", fileName = "BonusDatabase")]
public class BonusDataBase : BaseDB<BonusData>
{

}


[System.Serializable]
public class BonusData
{
    [Tooltip("Main Sprite")]
    [SerializeField] private Sprite mainSprite;
    public Sprite MainSprite
    {
        get { return mainSprite; }
        protected set { mainSprite = value; }
    }

    [Tooltip("Time added")]
    [SerializeField] private float addTime;
    public float AddTime
    {
        get { return addTime; }
        protected set { addTime = value; }
    }
}
