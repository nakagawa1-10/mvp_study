using System.Collections;
using System.Collections.Generic;
using UniRx;
using UnityEngine;

public class AppModel
{

    private ReactiveProperty<string> _name;
    public IReadOnlyReactiveProperty<string> Name { get { return _name; } }
    public void SetName(string name) { _name.Value = name; }

    public AppModel()
    {
        _name = new ReactiveProperty<string>(string.Empty);
    }
}
