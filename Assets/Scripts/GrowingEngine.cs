using UnityEngine;
using System.Collections;

public class GrowingEngine : Receiver
{
    [SerializeField] float maxEnergy;
    [SerializeField] float _energy;
    [SerializeField] float _energyDrain;
    [SerializeField] EnergyModule energyModule;
    [SerializeField] GeneratorType _generatorType;

    bool isLinked
    {
        get
        {
            return _generatorType == GeneratorType.None
                || GeneratorManager.instance.IsGeneratorOn(_generatorType);
        }
    }

    public override bool canReceiveEnergy
    {
        get { return isLinked; }
    }

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
        // todo ? 
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
