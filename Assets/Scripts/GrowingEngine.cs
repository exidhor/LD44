using UnityEngine;
using System.Collections;

public class GrowingEngine : Receiver
{
    public float maxEnergy;
    public float _energy;
    public float _energyDrain;
    public EnergyModule energyModule;

    public override void OnReloading(float energy)
    {
        _energy += energy;

        if(_energy > maxEnergy)
        {
            _energy = maxEnergy;
        }

        energyModule.SetEnergy(_energy / maxEnergy);
    }

    public override void Actualize(float dt)
    {
        //if(_energy > 0)
        //{
        //    Debug.Log("Plant Grow");
        //}
    }

    public bool RequestWater(float dt)
    {
        float required = _energyDrain * dt;

        if(required < _energy)
        {
            _energy -= required;
            energyModule.SetEnergy(_energy / maxEnergy);
            return true;
        }

        _energy = 0;
        energyModule.SetEnergy(_energy / maxEnergy);
        return false;
    }
}
