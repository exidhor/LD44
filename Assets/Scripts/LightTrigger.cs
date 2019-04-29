using UnityEngine;
using System.Collections;

public class LightTrigger : Receiver
{
    [SerializeField] float maxEnergy;
    [SerializeField] float _energy;
    [SerializeField] float _energyDrain;
    [SerializeField] EnergyModule energyModule;
    [SerializeField] GameObject _light;

    public override void OnReloading(float energy)
    {
        _energy += energy;

        if (_energy > maxEnergy)
        {
            _energy = maxEnergy;
        }

        energyModule.SetEnergy(_energy / maxEnergy);
    }

    public override void Actualize(float dt)
    {
        _energy -= _energyDrain * dt;

        if(_energy < 0)
        {
            _energy = 0;
        }

        energyModule.SetEnergy(_energy / maxEnergy);

        _light.SetActive(_energy > 0);
    }
}
