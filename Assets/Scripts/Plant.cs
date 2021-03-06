﻿using UnityEngine;
using System;
using System.Collections;

public class Plant : MonoBehaviour
{
    [Serializable]
    class Evolve
    {
        public float growingTime;
    }

    [SerializeField] int _lifePointMax;
    [SerializeField] int _startingLifePoint;
    [SerializeField] float _regeneration;
    [SerializeField] Evolve[] _evolves;
    [SerializeField] SpriteAnimator _animator;
    [SerializeField] GrowingEngine _growingEngine;

    ElectricLight _light;
    float _currentGrowingTime;
    float _lifePoint;
    int _currentEvolveIndex;

    void Awake()
    {
        _lifePoint = _startingLifePoint;

        //_renderer.sprite = _evolves[_currentEvolveIndex].sprite;
        _animator.StartAnim(_currentEvolveIndex);
    }

    void OnEnable()
    {
        PlantManager.instance.Register(this);
    }

    void OnDisable()
    {
        if(PlantManager.internalInstance != null)
        {
            PlantManager.instance.UnRegister(this);
        }
    }

    public void SetLight(ElectricLight light)
    {
        _light = light;
    }

    public void HandleGrowth(float dt)
    {
        bool water = _growingEngine.RequestWater(dt);
        bool light = true; // todo

        if (light && water)
        {
            Grow(dt);
        }
        else
        {
            Die(light && water ? 2 : 1, dt);
        }

        if(_lifePoint <= 0)
        {
            _animator.StartAnim("die");
        }
    }

    void Grow(float dt)
    {
        HandleRegeneration(dt);

        if (_currentEvolveIndex >= _evolves.Length - 1)
            return;

        _currentGrowingTime += dt;
        if (_currentGrowingTime > _evolves[_currentEvolveIndex].growingTime)
        {
            _currentEvolveIndex++;
            //_renderer.sprite = _evolves[_currentEvolveIndex].sprite;
            _animator.StartAnim(_currentEvolveIndex);
            _currentGrowingTime -= _evolves[_currentEvolveIndex].growingTime;
        }
    }

    void HandleRegeneration(float dt)
    {
        _lifePoint += _regeneration * dt;
        if(_lifePoint > _lifePointMax)
        {
            _lifePoint = _lifePointMax;
        }
    }

    void Die(int factor, float dt)
    {
        _lifePoint -= factor * dt * 10; // base 10 coz int

        if(_lifePoint < 0)
        {
            _lifePoint = 0;
        }
    }
}
