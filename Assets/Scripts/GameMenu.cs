using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseMenu : MonoBehaviour
{
    [Header("UI")]
    public GameObject pauseUI;

    [Header("Player")]
    // Ваш скрипт контролера — наприклад, PlayerController
    public MonoBehaviour playerController; 

    private bool isPaused = false;

    void Start()
    {
        // Переконуємося, що гра починається в активному стані
        Resume();
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (isPaused) Resume();
            else Pause();
        }
    }

    public void Resume()
    {
        // Вимикаємо меню
        pauseUI.SetActive(false);
        // Відновлюємо час гри
        Time.timeScale = 1f;
        // Вмикаємо контролер гравця
        if (playerController != null)
            playerController.enabled = true;
        // Приховуємо й блокуємо курсор
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
        isPaused = false;
    }

    public void Restart()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    public void Quit()
    {
        Application.Quit();
        Debug.Log("Вихід з гри...");
    }

    void Pause()
    {
        // Показуємо меню
        pauseUI.SetActive(true);
        // Зупиняємо всі фізичні та анімаційні процеси
        Time.timeScale = 0f;
        // Вимикаємо контролер гравця
        if (playerController != null)
            playerController.enabled = false;
        // Розблоковуємо курсор для UI
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
        isPaused = true;
    }
}
