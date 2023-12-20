using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    public bool paused;

    //References
    public GameObject pauseMenu;
    public GameObject loading;
    string music = "Music";

    void Awake()
    {
        loading.SetActive(true);
        Invoke("LoadingScreen", 3);
    }

    void LoadingScreen()
    {
        loading.SetActive(false);
        FindObjectOfType<AudioManager>().Play(music);
    }

    public void Play()
    {
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
        SceneManager.LoadScene("Game");
    }

    public void ExitToDesktop()
    {
        Application.Quit();
    }

    public void Resume()
    {
        paused = false;

        pauseMenu.SetActive(false);
        Time.timeScale = 1f;

        AudioSource[] audios = FindObjectsOfType<AudioSource>();
        foreach(AudioSource a in audios)
        {
            a.UnPause();
        }
    }

    public void Pause()
    {
        paused = true;

        pauseMenu.SetActive(true);
        Time.timeScale = 0f;

        AudioSource[] audios = FindObjectsOfType<AudioSource>();
        foreach(AudioSource a in audios)
        {
            a.Pause();
        }
    }

    public void MenuButton()
    {
        if(paused)
        {
            Resume();
        }
        else
        {
            Pause();
        }
    }

    public void ExitToMenu()
    {
        Time.timeScale = 1f;
        paused = false;
        SceneManager.LoadScene("MainMenu");
    }

    public void Level1()
    {
        SceneManager.LoadScene("Level 1");
    }
    public void Level2()
    {
        SceneManager.LoadScene("Level 2");
    }
    public void Level3()
    {
        SceneManager.LoadScene("Level 3");
    }
    public void Level4()
    {
        SceneManager.LoadScene("Level 4");
    }
}