using UnityEngine;
using System.Collections.Generic;
using Tools;

public class ElectricLightManager : MonoSingleton<ElectricLightManager>
{
    List<ElectricLight> _lights = new List<ElectricLight>();

    public void Register(ElectricLight light)
    {
        _lights.Add(light);
    }

    public void Unregister(ElectricLight light)
    {
        _lights.Remove(light);
    }

    public ElectricLight FindElectricLight(Vector2 wpos)
    {
        float bestDist = float.MaxValue;
        ElectricLight best = null;

        for (int i = 0; i < _lights.Count; i++)
        {
            Rect rect = _lights[i].GetCollider();

            if (rect.Contains(wpos))
            {
                float dist = Vector2.Distance(rect.center, wpos);

                if (dist < bestDist)
                {
                    best = _lights[i];
                    bestDist = dist;
                }
            }
        }

        return best;
    }
}
