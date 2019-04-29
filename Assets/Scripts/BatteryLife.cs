using UnityEngine;
using System.Collections.Generic;
using System;

public class BatteryLife : MonoBehaviour
{
    [Serializable]
    class PivotPerAnim
    {
        public string name;
        public List<Vector2> pivots;
    }

    [SerializeField] SpriteRenderer _renderer;
    [SerializeField] List<PivotPerAnim> _pivotPerAnims;

    Dictionary<string, List<Vector2>> _pivotMap = new Dictionary<string, List<Vector2>>();
    float _scaleAtMax;

    void Awake()
    {
        _scaleAtMax = transform.localScale.y;

        for(int i = 0; i < _pivotPerAnims.Count; i++)
        {
            _pivotMap.Add(_pivotPerAnims[i].name, _pivotPerAnims[i].pivots);
        }
    }

    public void Refresh(string animName, int index, float value01)
    {
        Vector2 pivot = _pivotMap[animName][index];

        Vector3 pos = transform.localPosition;
        pos.x = pivot.x;
        pos.y = pivot.y;
        transform.localPosition = pos;

        Vector3 scale = transform.localScale;
        scale.y = value01 * _scaleAtMax;
        transform.localScale = scale;
    }
}
