using System.Collections;
using System.Collections.Generic;
using UniRx;
using UnityEngine;
using Kosu.UnityLibrary;

public class AppPresenter : MonoBehaviour
{
    [SerializeField]
    private AppView _view;

    private AppModel _model;

    private List<System.IDisposable> _streams = new List<System.IDisposable>();

    private void Awake()
    {
        _model = new AppModel();
    }

    private void OnEnable()
    {
        Bind();
    }

    private void OnDisable()
    {
        foreach (var stream in _streams)
        {
            stream.Dispose();
        }

        _streams.Clear();
    }

    private void Bind()
    {
        // view bind
        _view.OnCompleteEdit = OnCompleteEdit;
        // model bind
        //_model.Name.Subscribe(OnChangeName).AddTo(gameObject);
        _streams.Add(_model.Status.Subscribe(OnChangeState));
        _streams.Add(_model.Name.Subscribe(OnChangeName));
        _streams.Add(UniRxUtility.ObserveInputKeyDown(KeyCode.F1, () =>
        {
            Debug.Log("test");
        }));
    }

    private void Start()
    {
        _model.SetStatus(AppModel.AppStatus.Opening);
    }

    private void OnCompleteEdit(string inputName)
    {
        _model.SetName(inputName);
    }

    private void OnChangeName(string name)
    {
        if (name.IsNullOrEmpty()) return;

        _view.SetWelcomText(name);

        // statusが3s後にイントロになります
        // intro status中に毎フレーム実行する
        // _view.StartIntroUpdateLoop();
        // statusが10s後にintroからgameに変わります
        // _view.StopIntroUpdateLoop();
        //if (_model.Status.Value == AppModel.AppStatus.Opening)
        //{
        //    Observable.Timer(System.TimeSpan.FromSeconds(3)).Subscribe(_ =>
        //    {
        //        _model.SetStatus(AppModel.AppStatus.Intro);
        //    }).AddTo(gameObject);
        //}
    }

    private void OnChangeState(AppModel.AppStatus status)
    {
        Debug.Log("State : " + status.ToString());

        switch (status)
        {
            case AppModel.AppStatus.Opening:
                Opening();
                break;
            case AppModel.AppStatus.Intro:
                Intro();
                break;
            case AppModel.AppStatus.Game:
                Game();
                break;
        }
    }

    private void Opening()
    {
        StartCoroutine(OpeningEnumerator());
    }

    private IEnumerator OpeningEnumerator()
    {
        yield return new WaitUntil(() => _model.Name.Value.IsNotNullOrEmpty());
        yield return new WaitForSeconds(3);
        yield return _view.HideWelcomeText();
        _model.SetStatus(AppModel.AppStatus.Intro);
    }

    private void Intro()
    {
        //_view.StartIntroUpdateLoop();
        //Observable.Timer(System.TimeSpan.FromSeconds(10)).Subscribe(_ =>
        //{
        //    _model.SetStatus(AppModel.AppStatus.Game);
        //}).AddTo(gameObject);
        StartCoroutine(IntroEnumerator());
    }

    private IEnumerator IntroEnumerator()
    {
        _view.StartIntroUpdateLoop();
        _view.ShowIntroText();
        yield return new WaitForSeconds(10);
        _model.SetStatus(AppModel.AppStatus.Game);
    }

    private void Game()
    {
        _view.StopIntroUpdateLoop();
    }
}
