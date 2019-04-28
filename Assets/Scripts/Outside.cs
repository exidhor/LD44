using UnityEngine;
using System.Collections;
using Tools;

public class Outside : MonoSingleton<Outside>
{
    [SerializeField] Vector2 _centerCollider;
    [SerializeField] Vector2 _sizeCollider = Vector2.one;

    Rect _rect;

    void Awake()
    {
        _rect = GetCollider();
    }

    public bool IsOutside(Vector2 wpos)
    {
        return _rect.Contains(wpos);
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
        Gizmos.color = Color.red;
        Rect rect = GetCollider();
        Gizmos.DrawWireCube(rect.center, rect.size);
    }
}
