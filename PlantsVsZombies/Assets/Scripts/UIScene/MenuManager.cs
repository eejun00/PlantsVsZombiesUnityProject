using UnityEngine;
using UnityEngine.UI;

public class MenuManager : MonoBehaviour
{
    public GameObject menu;
    public void OnClickMenuBtn()
    {
        menu.SetActive(true);
    }

    public void OnClickMainMenu()
    {
        GFunc.LoadScene("TitleSceneLJY");
    }

    public void OnClickBackGame()
    {
        menu.SetActive(false);
    }
}