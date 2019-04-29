using UnityEngine;
using System.Collections;

public class Collider : MonoBehaviour
{
    [SerializeField] Vector2 _centerCollider;
    [SerializeField] Vector2 _sizeCollider = Vector2.one;

    void OnEnable()
    {
        ColliderManager.instance.Register(this);
    }

    void OnDisable()
    {
        if(ColliderManager.internalInstance != null)
        {
            ColliderManager.instance.Unregister(this);
        }
    }

    public Rect GetCollider()
    {
        Vector2 center = _centerCollider + (Vector2)transform.position;
        Vector2 size = _sizeCollider;

        return new Rect(center.x - size.x / 2,
                        center.y - size.y / 2,
                        size.x,
                        size.y);
    }

    void OnDrawGizmos()
    {
        Gizmos.color = Color.blue;
        Rect rect = GetCollider();
        Gizmos.DrawWireCube(rect.center, rect.size);
    }
}
