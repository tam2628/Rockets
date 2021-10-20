using UnityEngine;
using TMPro;
using System.Collections;

public class GameCanvasScript : MonoBehaviour
{
    public static GameCanvasScript instance;
    public TextMeshProUGUI startPosText, endPosText;
    public GameObject testPanel;
    private Player p;

    private void Awake()
    {
        p = Player.instance;

        if (instance == null)
            instance = this;
        else if (instance != this)
            Destroy(gameObject);
    }

    private void Start()
    {
        testPanel.SetActive(false);
        #if DEVELOPMENT_BUILD
            testPanel.SetActive(true);
        #endif
    }

    private void Update()
    {
        #if DEVELOPMENT_BUILD
            startPosText.text = "StartTouchPos: " + p.startTouchPos.ToString();
            endPosText.text = "EndTouchPos: " + p.endTouchPos.ToString();
        #endif
    }
}
