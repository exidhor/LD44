using UnityEngine;
using System.Collections.Generic;
using Tools;

public class ColliderManager : MonoSingleton<ColliderManager>
{
    List<Collider> _colliders = new List<Collider>();

    public void Register(Collider collider)
    {
        _colliders.Add(collider);
    }

    public void Unregister(Collider collider)
    {
        _colliders.Remove(collider);
    }

    public bool IsPossiblePosition(Rect player)
    {
        for(int i = 0; i < _colliders.Count; i++)
        {
            Rect rect = _colliders[i].GetCollider();

            if(rect.Overlaps(player))
            {
                return false;
            }
        }

        return true;
    }
}
