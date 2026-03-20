using UnityEngine;
using UnityEngine.UI;

public class UIControls : MonoBehaviour
{
    public GameObject statsPanel;

    private void Start()
    {
        statsPanel.SetActive(false);
    }

    public void onstatsPanelPress()
    {
        if (statsPanel.activeInHierarchy)
        {
            statsPanel.SetActive(false);
        }
        else
        {
            statsPanel.SetActive(true);
        }
    }
}
