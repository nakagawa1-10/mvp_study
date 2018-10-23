using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Kosu.UnityLibrary;
using UniRx;
using DG.Tweening;


public class AppView : MonoBehaviour
{
    [SerializeField]
    private InputField _inputField;

    [SerializeField]
    private Button _editBtn;

    public System.Action<string> OnCompleteEdit;

    [SerializeField]
    private Text _welcomeText;

    private System.IDisposable _introUpdateStream;

    [SerializeField]
    private Text _introText;

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
        //if (!string.IsNullOrEmpty(_inputField.text))
        if (_inputField.text.IsNotNullOrEmpty())
        {
            //if (OnCompleteEdit != null) OnCompleteEdit(_inputField.text);
            OnCompleteEdit.SafeInvoke(_inputField.text);
        }
    }

    public void SetWelcomText(string name)
    {
        _welcomeText.text = name + "さんようこそ";
    }

    public void StartIntroUpdateLoop()
    {
        if (_introUpdateStream != null)
        {
            return;
        }

        Debug.Log("Start Intro Loop");

        _introUpdateStream = Observable.EveryUpdate().Subscribe(_ =>
        {
            //Debug.Log("update intro loop");
        });
    }

    public void StopIntroUpdateLoop()
    {
        if (_introUpdateStream == null)
        {
            return;
        }

        Debug.Log("Stop Intro Loop");

        _introUpdateStream.Dispose();
        _introUpdateStream = null;
    }

    public IEnumerator HideWelcomeText()
    {
        yield return _welcomeText.DOFade(0f, 1f).WaitForCompletion();
    }

    public void ShowIntroText()
    {
        _introText.DOFade(1f, 1f);
    }

}
