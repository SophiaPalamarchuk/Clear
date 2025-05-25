using UnityEngine;

public class SelectableCube : MonoBehaviour
{
    public int index;       // Індекс у ряду
    public bool isUpper;    // true — верхній, false — нижній

    private void OnMouseDown()
    {
        SecondGameManager.Instance.OnCubeSelected(this);
    }
}
