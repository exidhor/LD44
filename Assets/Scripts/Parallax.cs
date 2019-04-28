using UnityEngine;
using System.Collections.Generic;
using Tools;

public class Parallax : MonoSingleton<Parallax>
{
    [SerializeField] float _frontFactor;
    [SerializeField] float _midFactor;
    [SerializeField] float _farFactor;

    List<Transform> _front = new List<Transform>();
    List<Transform> _mid = new List<Transform>();
    List<Transform> _far = new List<Transform>();

    void Start()
    {
        GameObject[] frontGos = GameObject.FindGameObjectsWithTag("front");

        for(int i = 0; i < frontGos.Length; i++)
        {
            _front.Add(frontGos[i].transform);
        }

        GameObject[] midGos = GameObject.FindGameObjectsWithTag("mid");

        for (int i = 0; i < midGos.Length; i++)
        {
            _mid.Add(midGos[i].transform);
        }

        GameObject[] farGos = GameObject.FindGameObjectsWithTag("far");

        for (int i = 0; i < farGos.Length; i++)
        {
            _far.Add(farGos[i].transform);
        }
    }

    public void Actualize(float moveX)
    {
        SetLayer(_far, moveX, _farFactor);
        SetLayer(_mid, moveX, _midFactor);
        SetLayer(_front, moveX, _frontFactor);
    }

    void SetLayer(List<Transform> transforms, float moveX, float factor)
    {
        float move = moveX *_farFactor;

        for(int i = 0; i < transforms.Count; i++)
        {
            SetPositionX(transforms[i], move);
        }
    }

    void SetPositionX(Transform transform, float moveX)
    {
        Vector3 pos = transform.position;
        pos.x += moveX;
        transform.position = pos;
    }
}
