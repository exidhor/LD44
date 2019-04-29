using UnityEngine;
using System.Collections;

public abstract class Receiver : MonoBehaviour
{
    public virtual bool canReceiveEnergy
    {
        get { return true; }
    }

    [SerializeField] Vector2 _centerCollider;
    [SerializeField] Vector2 _sizeCollider = Vector2.one;

    void OnEnable()
    {
        ReceiverManager.instance.Register(this);
    }

    void OnDisable()
    {
        if(ReceiverManager.internalInstance != null)
            ReceiverManager.instance.Unregister(this);
    }

    public abstract void OnReloading(float energy);
    public abstract void Actualize(float dt);

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
        Gizmos.color = Color.green;
        Rect rect = GetCollider();
        Gizmos.DrawWireCube(rect.center, rect.size);
    }
}
