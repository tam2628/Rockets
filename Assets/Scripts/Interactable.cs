using UnityEngine;

public class Interactable : MonoBehaviour
{
    [HideInInspector]
    public int id;
    private float speed = 49f;
    private GameManager gm;

    public GamedataScriptableObject gameData;
    private Vector3 targetPosition;

    private void Start()
    {
        gm = GameManager.instance;
        targetPosition = new Vector3(transform.position.x, -100, transform.position.z);
    }

    private void Update()
    {
        if(gameData.gameState == GameManager.GameStates.Started)
            transform.position = Vector3.Lerp(transform.position, targetPosition, Time.deltaTime * speed / 1000);
    }
}
