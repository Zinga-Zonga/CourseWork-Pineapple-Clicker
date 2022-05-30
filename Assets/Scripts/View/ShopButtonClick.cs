using UnityEngine;

public class ShopButtonClick : MonoBehaviour
{
    [SerializeField] private GameObject panel;
    public void CloseAndOpen()
    {
        panel.SetActive(!panel.activeSelf);
    }
}
