using UnityEngine;

public class ObjectSpawnner : MonoBehaviour
{
    private GameManager gm;
    [SerializeField]
    private GameObject obstacle, coin;
    [SerializeField]
    private int poolSize = 10;
    [SerializeField]
    private float distanceBetweenObjects;
    private float probabilityOfCoin;
    private GameObject[] objectPool;
    private Camera mainCamera;
    Vector3 screenHeight, screenWidth;
    private float cameraScreenHeight, cameraScreenWidth, lastYPos;
    private bool newObjectPooled;

    private void Start()
    {
        mainCamera = Camera.main;
        screenHeight = new Vector3(0, Screen.height, 0);
        screenWidth = new Vector3(Screen.width, 0, 0);
        cameraScreenHeight = mainCamera.ScreenToWorldPoint(screenHeight).y;
        cameraScreenWidth = mainCamera.ScreenToWorldPoint(screenWidth).x;
        lastYPos = cameraScreenHeight;
        gm = GameManager.instance;
        objectPool = new GameObject[poolSize + 2];
        PoolObjects();
        Player.PlayerHitSomething += (GameObject g) =>
        {
            int id;
            try { id = g.GetComponentInParent<Interactable>().id; }
            catch { id = g.GetComponent<Interactable>().id; }

            if (id == poolSize)
                PoolObjects();
        };
    }

    public GamedataScriptableObject gameData;

    private void Update()
    {   
        if (gameData.gameState == GameManager.GameStates.Started && newObjectPooled)
        {
            for (int i= 1; i <= poolSize; ++i)
                objectPool[i].SetActive(true);
            newObjectPooled = false;
        }
    }

    private void PoolObjects()
    {
        if (objectPool.Length > 0)
            for (int i = 1; i < objectPool.Length; ++i)
            {
                try { Destroy(objectPool[i]); }
                catch { Debug.Log(i); }
            }
                

        probabilityOfCoin = Mathf.Clamp(Random.Range(0, 1.0f), 0.3f, 0.5f);
        //probabilityOfCoin = probabilityOfCoin > 0.5f ? 0.5f : probabilityOfCoin;
        for (int i = 1; i <= poolSize; ++i)
        {
            float xPos = GetXPos(),
                yPos = lastYPos;

            GameObject objectInGame =
                Random.Range(0f, 1.0f) > probabilityOfCoin ?
                    Instantiate(obstacle) :
                    Instantiate(coin);
            if (objectInGame.CompareTag("ObstacleAndScore"))
            {
                float rotation = Random.Range(0, 360f),
                    scale = Random.Range(0.5f, 1.7f);
                objectInGame.transform.GetChild(0).localRotation = Quaternion.Euler(rotation, rotation, rotation);
                objectInGame.transform.GetChild(0).localScale = new Vector3(scale, scale, scale);
            }
            BoxCollider c = objectInGame.transform.GetChild(1).GetComponent<BoxCollider>();
            Vector3 size = c.size;
            c.size = new Vector3(cameraScreenWidth*4, size.y, size.z);
            objectInGame.transform.position = new Vector3(xPos, yPos, 10);
            objectInGame.GetComponent<Interactable>().id = i;
            objectInGame.SetActive(false);
            objectPool[i] = objectInGame;
            lastYPos += distanceBetweenObjects;
        }
        newObjectPooled = true;
    }

    private float GetXPos()
    {
        float[] xPos = { -cameraScreenWidth / 2, 0, cameraScreenWidth / 2 };
        return xPos[Random.Range(0, 3)];
    }
}
