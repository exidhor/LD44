using UnityEngine;
using System.Collections;

public class Generator : Receiver
{
    public GeneratorType type
    {
        get { return _type; }
    }

    public bool isOn
    {
        get { return _isOn; }
    }

    [SerializeField] GeneratorType _type;

    bool _isOn;

    public override void Actualize(float dt)
    {
        // nothing
    }

    public override void OnReloading(float energy)
    {
        _isOn = true;
    }
}
