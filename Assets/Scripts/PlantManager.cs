using UnityEngine;
using System.Collections.Generic;
using Tools;

public class PlantManager : MonoSingleton<PlantManager>
{
    List<Plant> _plants = new List<Plant>();

    bool _isInit;

    public void Register(Plant plant)
    {
        _plants.Add(plant);
    }

    public void UnRegister(Plant plant)
    {
        _plants.Remove(plant);
    }

    public void CheckForInit()
    {
        if (_isInit) return;

        for(int i = 0; i < _plants.Count; i++)
        {
            ElectricLight light = ElectricLightManager.instance.FindElectricLight(_plants[i].transform.position);
        
            if(light == null)
            {
                Debug.Log("Error for plant " + _plants[i]  + " index " + i);
            }
            else
            {
                _plants[i].SetLight(light);
            }
        }

        _isInit = true;
    }

    public void Actualize(float dt)
    {
        for(int i = 0; i < _plants.Count; i++)
        {
            _plants[i].HandleGrowth(dt);
        }
    }
}
