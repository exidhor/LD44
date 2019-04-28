using UnityEngine;
using System.Collections.Generic;
using Tools;

public class EnergyModuleManager : MonoSingleton<EnergyModuleManager>
{
    List<EnergyModule> _modules = new List<EnergyModule>();

    public void Register(EnergyModule module)
    {
        _modules.Add(module);
    }

    public void Unregister(EnergyModule module)
    {
        _modules.Remove(module);
    }

    public void Actualize()
    {
        for(int i = 0; i < _modules.Count; i++)
        {
            _modules[i].Refresh();
        }
    }
}
