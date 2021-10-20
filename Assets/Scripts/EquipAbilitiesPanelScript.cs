using System;
using UnityEngine;

public class EquipAbilitiesPanelScript : MonoBehaviour
{
    private RectTransform rt;
    [SerializeField]
    private float duration = 0.3f;
    public static event Action EquipAbilitiesMenuClosed;
    
    public void Awake()
    {
        rt = gameObject.GetComponent<RectTransform>();
        GameCanvasManager.GameSceneLoaded += (string sceneName) =>
        {
            if(sceneName == "GameScene" && rt != null)
                LeanTween.scale(rt, Vector3.one, duration);
        };
    }
    
    public void CloseMenu()
    {
        LeanTween.scale(rt, Vector3.zero, duration);
        EquipAbilitiesMenuClosed?.Invoke();
    }
}
