using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;

public class InvUIHandler : MonoBehaviour
{
    public GameObject invUI;
    Button[] invButtons;

    void Start()
    {
        invUI.SetActive(false);
        invButtons = new Button[12];
    }

    void Update()
    {
        if (Keyboard.current.iKey.wasPressedThisFrame)
        {
            invUI.SetActive(!invUI.activeInHierarchy);
        }
    }
}
