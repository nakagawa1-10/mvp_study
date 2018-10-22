using System.Collections;
using System.Collections.Generic;
using UniRx;
using UnityEngine;

public class AppPresenter : MonoBehaviour
{
    [SerializeField]
    private AppView _view;

    private AppModel _model;


    private void Awake()
    {
        _model = new AppModel();
        //_model.Name.sub
    }

    private void OnEnable()
    {
        _view.OnCompleteEdit += OnCompleteEdit;
        _model.Name.Subscribe(OnChangeName);
    }


    private void OnCompleteEdit(string inputName)
    {
        _model.SetName(inputName);
    }

    private void OnChangeName(string name)
    {
        _view.SetWelcomText(name);
    }
}
