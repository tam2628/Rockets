using UnityEngine;
using UnityEngine.SceneManagement;

public class ShopCanvasScript : MonoBehaviour
{
    public void CloseShopButtonPressed() => SceneManager.LoadScene(0);
}
