using UnityEngine;
using System.Collections.Generic;
using Tools;

public class PlantManager : MonoSingleton<PlantManager>
{
    List<Plant> _plants = new List<Plant>();

    public void Register(Plant plant)
    {
        _plants.Add(plant);
    }

    public void UnRegister(Plant plant)
    {
        _plants.Remove(plant);
    }

    public void Actualize(float dt)
    {
        for(int i = 0; i < _plants.Count; i++)
        {
            _plants[i].HandleGrowth(dt);
        }
    }
}
