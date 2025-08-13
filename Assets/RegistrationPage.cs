using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class RegistrationPage : MonoBehaviour
{
    public TMP_InputField NameInputField;
    public TMP_InputField PhoneNumberField;
    public Button RegisterButton;

    private void Awake()
    {
        RegisterButton.onClick.AddListener(Register);
    }
    public void Register()
    {
        var _name = NameInputField.text;
        var _phone = PhoneNumberField.text;
        Debug.Log($"Details:{_name},{_phone}");
    }
}