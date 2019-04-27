using UnityEngine;
using System.Collections;

public class GrowingEngine : Receiver
{
    public float maxEnergy;
    public float _energy;

    public override void OnReloading(float energy)
    {
        _energy += energy;

        if(_energy > maxEnergy)
        {
            _energy = maxEnergy;
        }
    }

    public override void Actualize(float dt)
    {
        if(_energy > 0)
        {
            Debug.Log("Plant Grow");
        }
    }
}
