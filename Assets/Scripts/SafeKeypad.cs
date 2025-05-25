using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

/// <summary>
/// Панельний сейф з кодом.
/// При правильному коді – затемнення екрану.
/// </summary>
public class SafeKeypad : MonoBehaviour
{
    [Header("UI")]
    public TextMeshProUGUI display;     // вивід цифр
    public Image blackoutPanel;         // чорний екран

    [Header("Налаштування")]
    public string correctCode = "197362584"; // 9-значний секрет
    public float fadeDuration = 1f;          // сек.

    private string input = "";

    #region ── КНОПКИ ────────────────────────────────────────────
    public void PressDigit(string d)   // 0-9
    {
        if (input.Length >= 9) return;   // зайвих не приймаємо
        input += d;
        UpdateDisplay();

        if (input.Length == 9)           // автоперевірка після 9-ї
            Validate();
    }

    public void PressClear()            // C
    {
        input = "";
        UpdateDisplay();
    }

    public void PressEnter()            // ▶
    {
        Validate();
    }
    #endregion

    #region ── Перевірка й затемнення ────────────────────────────
    void Validate()
    {
        if (input == correctCode)
        {
            StartCoroutine(FadeToBlack());
        }
        else
        {
            // помилка – підморгни червоним (або звук)
            StartCoroutine(ErrorFlash());
        }
    }

    IEnumerator ErrorFlash()
    {
        display.color = Color.red;
        yield return new WaitForSeconds(0.4f);
        display.color = Color.white;
        PressClear();
    }

    IEnumerator FadeToBlack()
    {
        float t = 0f;
        while (t < fadeDuration)
        {
            float a = Mathf.Lerp(0f, 1f, t / fadeDuration);
            SetBlackoutAlpha(a);
            t += Time.deltaTime;
            yield return null;
        }
        SetBlackoutAlpha(1f);
        // TODO: тут можна завантажити кат-сцену чи сцену перемоги
    }

    void SetBlackoutAlpha(float a)
    {
        var c = blackoutPanel.color;
        c.a = a;
        blackoutPanel.color = c;
    }
    #endregion

    void UpdateDisplay()
    {
        display.text = input;
    }

    void Start()
    {
        // переконайся, що початковий альфа = 0
        SetBlackoutAlpha(0);
        UpdateDisplay();
    }
}
