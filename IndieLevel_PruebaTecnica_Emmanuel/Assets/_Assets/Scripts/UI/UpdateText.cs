using TMPro;
using UnityEngine;

public class UpdateText : MonoBehaviour
{
    [Header("Dependencies")]
    [SerializeField] private TMP_Text text;

    public void UpdateTextUI(int value) => text.text = value.ToString();
}
