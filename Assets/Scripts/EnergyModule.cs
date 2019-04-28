using UnityEngine;
using System.Collections;

public class EnergyModule : MonoBehaviour
{
    public int energy;

    [SerializeField] Color[] _colorPerEnergy;
    [SerializeField] SpriteRenderer[] _leds;

    void OnEnable()
    {
        EnergyModuleManager.instance.Register(this);
    }

    void OnDisable()
    {
        if(EnergyModuleManager.internalInstance != null)
            EnergyModuleManager.instance.Unregister(this);
    }

    public void SetEnergy(float energy01)
    {
        energy = Mathf.CeilToInt(energy01 * _leds.Length);
    }

    public void Refresh()
    {
        Color ledColor = _colorPerEnergy[energy];

        for (int i = 0; i < _leds.Length; i++)
        {
            _leds[i].gameObject.SetActive(i < energy);
            _leds[i].color = ledColor;
        }
    }
}
