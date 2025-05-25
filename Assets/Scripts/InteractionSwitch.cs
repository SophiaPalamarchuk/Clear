using UnityEngine;

public class InteractionSwitch : MonoBehaviour
{
    public Transform player;
    public float activationDistance = 3f;

    public GameObject textObject;        // Надпис "Натисни F"
    public GameObject playerController;  // Об'єкт з контролером персонажа
    public GameObject puzzleUI;          // UI або камера п'ятнашок
    public GameObject gameRoot;          // Всі об'єкти гри в п'ятнашки

    public Camera mainCamera;
    public Camera puzzleCamera;

    private bool isNear = false;
    private bool isPuzzleMode = false;

    void Start()
    {
        SetPuzzleMode(false);
        if (textObject != null)
            textObject.SetActive(false);
    }

    void Update()
    {
        isNear = Vector3.Distance(player.position, transform.position) <= activationDistance;

        // Показувати текст тільки якщо гравець поряд і не в режимі п'ятнашок
        if (textObject != null)
            textObject.SetActive(isNear && !isPuzzleMode);

        if (isNear && Input.GetKeyDown(KeyCode.F))
        {
            isPuzzleMode = !isPuzzleMode;
            SetPuzzleMode(isPuzzleMode);
        }
    }

    void SetPuzzleMode(bool active)
    {
        // 🔁 Камери
        mainCamera.gameObject.SetActive(!active);
        puzzleCamera.gameObject.SetActive(active);

        // 🎮 Контролери
        playerController.SetActive(!active);
        puzzleUI.SetActive(active);
        gameRoot.SetActive(active);

        // 🖱️ Курсор
        Cursor.lockState = active ? CursorLockMode.None : CursorLockMode.Locked;
        Cursor.visible = active;

        Debug.Log(active ? "🧩 Вхід у гру п'ятнашки" : "🎮 Повернення до персонажа");
    }
}