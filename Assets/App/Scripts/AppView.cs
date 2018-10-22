using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AppView : MonoBehaviour
{
    [SerializeField]
    private InputField _inputField;

    [SerializeField]
    private Button _editBtn;

    public System.Action<string> OnCompleteEdit;

    [SerializeField]
    private Text _welcomeText;


    private void OnEnable()
    {
        _editBtn.onClick.AddListener(OnClickEditBtn);
    }

    private void OnDisable()
    {
        _editBtn.onClick.RemoveAllListeners();
    }

    private void OnClickEditBtn()
    {
        if (!string.IsNullOrEmpty(_inputField.text))
        {
            if (OnCompleteEdit != null) OnCompleteEdit(_inputField.text);
        }
    }

    public void SetWelcomText(string name)
    {
        _welcomeText.text = name + "さんようこそ";
    }


}
