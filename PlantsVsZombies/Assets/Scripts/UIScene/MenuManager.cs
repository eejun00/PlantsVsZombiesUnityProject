using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MenuManager : MonoBehaviour
{
    public GameObject menu;
    private bool isGamePaused = false;

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (menu.activeSelf)
            {
                CloseMenu();
            }
            else
            {
                ToggleMenu();
            }
        }

        // ������ �Ͻ� ���� ������ ��� �Է��� �����ϴ�.
        if (isGamePaused)
        {
            // ���⿡ �Ͻ� ���� ������ ���� �߰� ������ ���� �� �ֽ��ϴ�.
            return;
        }

        // ������ ���� ���� ���� ������Ʈ ������ ���⿡ �ۼ��մϴ�.
    }

    public void OnClickMenuBtn()
    {
        //menu.SetActive(true);
        //PauseGame();
        ToggleMenu();
    }

    public void OnClickBackGame()
    {
        //menu.SetActive(false);
        //ResumeGame();
        CloseMenu();
    }
    private void ToggleMenu()
    {
        menu.SetActive(!menu.activeSelf);
        if (menu.activeSelf)
        {
            PauseGame();
        }
        else
        {
            ResumeGame();
        }
    }

    private void CloseMenu()
    {
        menu.SetActive(false);
        ResumeGame();
    }
    public void OnClickMainMenu()
    {
        GFunc.LoadScene("TitleSceneLJY");
    }
    private void PauseGame()
    {
        Time.timeScale = 0.0f; // ���� �ð��� ����ϴ�.
        isGamePaused = true;
    }

    private void ResumeGame()
    {
        Time.timeScale = 1.0f; // ���� �ð��� ���� �ӵ��� �����ϴ�.
        isGamePaused = false;
    }

    public void OnClickRestartButton()
    {
        Time.timeScale = 1.0f; // ���� �ð��� ���� �ӵ��� �����ϴ�.
        // ���� ���� �ε����� ������
        int currentSceneIndex = SceneManager.GetActiveScene().buildIndex;

        // ���� ���� �ٽ� �ҷ���
        SceneManager.LoadScene(currentSceneIndex);
    }

    public void OnClickTestBtn()
    {
        Time.timeScale = 4.0f; // ���� �ð��� ���� �ӵ��� �����ϴ�.
        isGamePaused = false;
        menu.SetActive(false);
    }
}