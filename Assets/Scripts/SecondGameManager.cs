using UnityEngine;

public class SecondGameManager : MonoBehaviour
{
    public static SecondGameManager Instance { get; private set; }

    public GameObject[] lowerCubes = new GameObject[4];
    public GameObject[] upperCubes = new GameObject[4];

    public Transform[] lowerPositions;
    public Transform[] upperPositions;

    private SelectableCube firstSelected = null;

    void Awake()
    {
        Instance = this;
    }

    void Start()
    {
        SetupExistingCubes();
    }

    void SetupExistingCubes()
    {
        for (int i = 0; i < 4; i++)
        {
            // Привʼязуємо скрипти
            var lowerSel = lowerCubes[i].GetComponent<SelectableCube>();
            if (lowerSel == null) lowerSel = lowerCubes[i].AddComponent<SelectableCube>();
            lowerSel.index = i;
            lowerSel.isUpper = false;

            var upperSel = upperCubes[i].GetComponent<SelectableCube>();
            if (upperSel == null) upperSel = upperCubes[i].AddComponent<SelectableCube>();
            upperSel.index = i;
            upperSel.isUpper = true;

            // Позиції
            lowerCubes[i].transform.position = lowerPositions[i].position;
            upperCubes[i].transform.position = upperPositions[i].position;

            // Звʼязок верхнього з нижнім
            upperCubes[i].transform.SetParent(lowerCubes[i].transform);
        }
    }

    public void OnCubeSelected(SelectableCube cube)
    {
        if (firstSelected == null)
        {
            firstSelected = cube;
        }
        else
        {
            if (firstSelected.isUpper && cube.isUpper)
            {
                SwapUpper(firstSelected.index, cube.index);
            }
            else if (!firstSelected.isUpper && !cube.isUpper)
            {
                SwapLower(firstSelected.index, cube.index);
            }

            firstSelected = null;
        }
    }

   public void SwapUpper(int i, int j)
{
    // 1. Знімаємо з батьків перед пересуванням
    upperCubes[i].transform.SetParent(null);
    upperCubes[j].transform.SetParent(null);

    // 2. Міняємо місцями у масиві
    var temp = upperCubes[i];
    upperCubes[i] = upperCubes[j];
    upperCubes[j] = temp;

    // 3. Переміщаємо на відповідні позиції
    upperCubes[i].transform.position = upperPositions[i].position;
    upperCubes[j].transform.position = upperPositions[j].position;

    // 4. Прив’язуємо до відповідних нижніх кубів
    upperCubes[i].transform.SetParent(lowerCubes[i].transform);
    upperCubes[j].transform.SetParent(lowerCubes[j].transform);

    // 5. Оновлюємо індекси у SelectableCube
    upperCubes[i].GetComponent<SelectableCube>().index = i;
    upperCubes[j].GetComponent<SelectableCube>().index = j;
}

  public void SwapLower(int i, int j)
{
    // обмін нижніх кубів
    var tempLower = lowerCubes[i];
    lowerCubes[i] = lowerCubes[j];
    lowerCubes[j] = tempLower;

    lowerCubes[i].transform.position = lowerPositions[i].position;
    lowerCubes[j].transform.position = lowerPositions[j].position;

    lowerCubes[i].GetComponent<SelectableCube>().index = i;
    lowerCubes[j].GetComponent<SelectableCube>().index = j;

    // обмін верхніх кубів у масиві (логічно)
    var tempUpper = upperCubes[i];
    upperCubes[i] = upperCubes[j];
    upperCubes[j] = tempUpper;

    // оновлюємо індекси у SelectableCube
    upperCubes[i].GetComponent<SelectableCube>().index = i;
    upperCubes[j].GetComponent<SelectableCube>().index = j;
}

}
