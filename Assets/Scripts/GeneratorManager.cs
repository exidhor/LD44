using UnityEngine;
using System.Collections.Generic;
using Tools;

public enum GeneratorType
{
    None,
    Green,
    Blue,
}

public class GeneratorManager : MonoSingleton<GeneratorManager>
{
    [SerializeField] List<Generator> _generators = new List<Generator>();

    Dictionary<GeneratorType, Generator> _map = new Dictionary<GeneratorType, Generator>();

    void Awake()
    {
        for(int i = 0; i <_generators.Count; i++)
        {
            _map.Add(_generators[i].type, _generators[i]);
        }
    }

    public bool IsGeneratorOn(GeneratorType type)
    {
        return _map[type].isOn;
    }
}
