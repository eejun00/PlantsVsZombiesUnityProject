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

        // 게임이 일시 정지 상태인 경우 입력을 막습니다.
        if (isGamePaused)
        {
            // 여기에 일시 정지 상태일 때의 추가 로직을 넣을 수 있습니다.
            return;
        }

        // 게임이 실행 중일 때의 업데이트 로직을 여기에 작성합니다.
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
        Time.timeScale = 0.0f; // 게임 시간을 멈춥니다.
        isGamePaused = true;
    }

    private void ResumeGame()
    {
        Time.timeScale = 1.0f; // 게임 시간을 정상 속도로 돌립니다.
        isGamePaused = false;
    }

    public void OnClickRestartButton()
    {
        Time.timeScale = 1.0f; // 게임 시간을 정상 속도로 돌립니다.
        // 현재 씬의 인덱스를 가져옴
        int currentSceneIndex = SceneManager.GetActiveScene().buildIndex;

        // 현재 씬을 다시 불러옴
        SceneManager.LoadScene(currentSceneIndex);
    }

    public void OnClickTestBtn()
    {
        Time.timeScale = 4.0f; // 게임 시간을 정상 속도로 돌립니다.
        isGamePaused = false;
        menu.SetActive(false);
    }
}