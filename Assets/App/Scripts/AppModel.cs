using System.Collections;
using System.Collections.Generic;
using UniRx;
using UnityEngine;

public class AppModel
{
    public enum AppStatus
    {
        None,
        Opening,
        Intro,
        Game
    }

    private ReactiveProperty<AppStatus> _status;
    public IReadOnlyReactiveProperty<AppStatus> Status { get { return _status; } }
    public void SetStatus(AppStatus status) { _status.Value = status; }

    private ReactiveProperty<string> _name;
    public IReadOnlyReactiveProperty<string> Name { get { return _name; } }
    public void SetName(string name) { _name.Value = name; }

    public AppModel()
    {
        _status = new ReactiveProperty<AppStatus>(AppStatus.None);
        _name = new ReactiveProperty<string>(string.Empty);
    }
}
