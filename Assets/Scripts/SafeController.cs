using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class SafeController : MonoBehaviour
{
    [Header("UI")]
    public TMP_Text codeDisplay;           // Текст для показу зірочок
    public Image blackoutPanel;            // Чорна панель для затемнення

    [Header("Налаштування")]
    public string correctCode = "197362584"; // Код сейфу
    public float fadeDuration = 1f;           // Швидкість затемнення

    private string currentInput = "";

    void Start()
    {
        SetBlackoutAlpha(0f);
        UpdateDisplay();
    }

    // 🔢 Введення цифри (викликається кнопками 1–9)
    public void AddDigit(string digit)
    {
        if (currentInput.Length >= 9) return;

        currentInput += digit;
        UpdateDisplay();
    }

    // ❌ Скидання
    public void ClearInput()
    {
        currentInput = "";
        UpdateDisplay();
    }

    // 🆗 Перевірка введеного коду
    public void SubmitCode()
    {
        if (currentInput == correctCode)
        {
            Debug.Log("✅ Вірний код. Сейф відкрито.");
            StartCoroutine(FadeToBlack());
        }
        else
        {
            Debug.Log("❌ Невірний код.");
            ClearInput();
        }
    }

    void UpdateDisplay()
    {
        codeDisplay.text = new string('*', currentInput.Length);
    }

    void SetBlackoutAlpha(float alpha)
    {
        Color color = blackoutPanel.color;
        color.a = alpha;
        blackoutPanel.color = color;
    }

    System.Collections.IEnumerator FadeToBlack()
    {
        float t = 0f;
        while (t < fadeDuration)
        {
            float alpha = Mathf.Lerp(0f, 1f, t / fadeDuration);
            SetBlackoutAlpha(alpha);
            t += Time.deltaTime;
            yield return null;
        }

        SetBlackoutAlpha(1f);
        Debug.Log("🎉 Гру завершено або відкрито доступ.");
    }
}
