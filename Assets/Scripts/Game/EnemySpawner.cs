//using System.Collections;
//using System.Collections.Generic;
//using UnityEngine;

//public class EnemySpawner : MonoBehaviour
//{
//    [Tooltip("Class controlling game state")]
//    [SerializeField] private GameStateController gameStateController;

//    [Tooltip("Enemies settings List")]
//    [SerializeField] private EnemyDataBase enemySettings;

//    [Tooltip("Objects ammount in the pool")]
//    [SerializeField] private int poolCount;

//    [Tooltip("EnemieBase prefab")]
//    [SerializeField] private GameObject enemiePrefab;

//    [Tooltip("Time between the sapwns")]
//    [SerializeField] private float spawnDeltaTime;

//    public static Dictionary<GameObject, Enemy> Enemies;
//    private Queue<GameObject> currentEnemies;

//    private void Start()
//    {
//        Enemies = new Dictionary<GameObject, Enemy>();
//        currentEnemies = new Queue<GameObject>();

//        for (int i = 0; i < poolCount; i++)
//        {
//            InstantiateEnemy();
//        }

//        Enemy.OnEnemyOutOfBounds += ReturnEnemy;
//        gameStateController.OnGameOver += ReturnAll;
//        StartCoroutine(Spawn());
//    }

//    private void OnEnable()
//    {
//        if (Enemies != null)
//        {
//            StartCoroutine(Spawn());
//        }
//    }

//    private void InstantiateEnemy()
//    {
//        GameObject prefab = Instantiate(enemiePrefab);
//        prefab.transform.SetParent(transform);
//        prefab.transform.localScale = new Vector3(1, 1, 1);
//        Enemy script = prefab.GetComponent<Enemy>();
//        prefab.SetActive(false);
//        Enemies.Add(prefab, script);
//        currentEnemies.Enqueue(prefab);
//    }



//    private void ReturnEnemy(GameObject _enemy, int attack) 
//    {
//        _enemy.transform.position = transform.position;
//        _enemy.SetActive(false);
//        currentEnemies.Enqueue(_enemy);
//    }

//    private void ReturnAll()
//    {
//        foreach (KeyValuePair<GameObject, Enemy> keyValuePair in Enemies)
//        {
//            ReturnEnemy(keyValuePair.Key, 0);
//        }
//    }
//}


using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : SpawnPool<EnemyDataBase, Enemy>
{
    [Tooltip("Time between the sapwns")]
    [SerializeField] private float spawnDeltaTime;

    [Tooltip("Class controlling game state")]
    [SerializeField] private GameStateController gameStateController;

    // Start is called before the first frame update
    protected override void Awake()
    {
        base.Awake();
        Enemy.OnEnemyOutOfBoundsQueue += ReturnObj;
        gameStateController.OnGameOver += ReturnAll;
    }

    private void OnEnable()
    {
        if (ObjElementsDict != null)
        {
            StartCoroutine(Spawn());
        }
    }
    // elementSettings      ObjElementsDict
    private IEnumerator Spawn()
    {
        if (spawnDeltaTime == 0)
        {
            Debug.LogWarning($"spawnDeltaTime is not specified so using default value 1 s.");
            spawnDeltaTime = 1;
        }
        while (true)
        {
            yield return new WaitForSeconds(spawnDeltaTime);
            if (TryGetNextElementInTheQueue(out Enemy script))
            {
                // Generation
                float yPos;
                float xPos;

                int randInd = Random.Range(0, 2);
                xPos = ScreenViewManager.instance.screenBorders[randInd];
                yPos = Random.Range(ScreenViewManager.instance.screenBorders[2] * 0.8f, ScreenViewManager.instance.screenBorders[3] * 0.8f);

                if (randInd == 1)
                    script.transform.localScale = new Vector3(-1, 1, 1);
                else
                    script.transform.localScale = new Vector3(1, 1, 1);

                bool moveRight = script.transform.localScale.x > 0;

                script.Init(elementSettings.GetRandomElement(), moveRight);
                
                script.transform.position = new Vector2(xPos, yPos);
            }
        }
    }
}

