using UnityEngine;

public class PlayerBackground : MonoBehaviour
{
    private void Start()
    {
        GetComponent<Renderer>().material = GameManager.instance.GetPlayerBackgroundMaterial();        
    }
}
