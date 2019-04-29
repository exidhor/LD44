using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using Tools;

public class PlayerRobot : MonoSingleton<PlayerRobot>
{
    [SerializeField] float _speed;
    [SerializeField] float _maxEnergy;
    [SerializeField] float _startEnergy;
    [SerializeField] float _reloadingSpeed;
    [SerializeField] float _givenEnergySpeed;
    [SerializeField] SpriteAnimator _animator;
    [SerializeField] GameObject _camera;
    [SerializeField] BatteryLife _batteryLife;

    // for anims
    int _lastDirection;
    bool _gaveEnergy;
    bool _wasReloading;

    bool _isGettingUp;
    bool _isGettingDown;

    float _energy;

    [SerializeField] Vector2 _centerCollider;
    [SerializeField] Vector2 _sizeCollider = Vector2.one;

    public Rect GetCollider(Vector2 pos)
    {
        Vector2 center = _centerCollider + pos;
        Vector2 size = _sizeCollider;

        return new Rect(center.x - size.x / 2,
                        center.y - size.y / 2,
                        size.x,
                        size.y);
    }

    void OnDrawGizmos()
    {
        Gizmos.color = Color.blue;
        Rect rect = GetCollider(transform.position);
        Gizmos.DrawWireCube(rect.center, rect.size);
    }

    void Awake()
    {
        SetEnergy(_startEnergy);

        _lastDirection = -1000000;

        _animator.StartIdle();

        RefreshCameraPosition(true);
    }

    void SetEnergy(float energy)
    {
        _energy = energy;
        _batteryLife.Refresh(_animator.GetCurrentName(),
                             _animator.GetCurrentIndex(),
                             _energy / _maxEnergy);
    }

    void RefreshCameraPosition(bool first)
    {
        float startCamX = first ? 0f : _camera.transform.position.x;
        float moveX = transform.position.x - startCamX;

        Parallax.instance.Actualize(moveX);

        Vector3 pos = _camera.transform.position;
        pos.x = transform.position.x;
        _camera.transform.position = pos;
    }

    public void Actualize(float dt)
    {
        int direction = 0;
        if (Input.GetKey(KeyCode.LeftArrow))
        {
            direction = -1;
        }
        else if (Input.GetKey(KeyCode.RightArrow))
        {
            direction = 1;
        }

        bool isMoving = direction != 0;
        if (isMoving)
        {
            if (_isGettingDown)
            {
                direction = 0;
                isMoving = false;
                _isGettingDown = false;
                _isGettingUp = true;
                _animator.ReverseCurrentAnim(OnEndGettingUp);
            }
            else if (_wasReloading)
            {
                direction = 0;
                isMoving = false;
                _isGettingUp = true;
                _animator.StartAnim("getDown", OnEndGettingUp, true);
            }
            else if(_isGettingUp || _isGettingDown)
            {
                direction = 0;
                isMoving = false;
            }
            else
            {
                if(!_isGettingDown)
                {
                    HandleOrientation(direction, dt);
                }
            }
        }

        bool tryToGiveEnergy = !isMoving 
                            && !_isGettingUp 
                            && !_isGettingDown
                            && Input.GetKey(KeyCode.Space);

        bool giveEnergy = false;

        if (tryToGiveEnergy)
        {
            float givenEnergy = GetGivenEnergy(dt);
            Receiver receiver = ReceiverManager.instance.TryToGiveEnergy(transform.position, givenEnergy);

            if (receiver != null)
            {
                _energy -= givenEnergy;
                giveEnergy = true;
            }
        }

        bool canReload = !isMoving 
                      && !giveEnergy 
                      && !_isGettingUp 
                      && !_isGettingDown
                      && Outside.instance.IsOutside(transform.position);

        UpdateAnim(direction, giveEnergy, canReload);

        HandleReloading(canReload && !_isGettingUp && !_isGettingDown, dt);
        RefreshCameraPosition(false);
    }

    void UpdateAnim(int direction, bool giveEnergy, bool reload)
    {
        bool sameAsPreviousFrame = (direction == _lastDirection);
        _lastDirection = direction;

        if (direction != 0)
        {
            if (!sameAsPreviousFrame)
                _animator.StartAnim("run");
        }
        else
        {
            if (giveEnergy && !_gaveEnergy)
            {
                _animator.StartAnim("giveEnergy");
            }
            else if(reload && !_wasReloading)
            {
                if(!_isGettingDown)
                {
                    _isGettingUp = false;
                    _isGettingDown = true;
                    _animator.StartAnim("getDown", OnEndGettingDown);
                }
            }
            else if (!giveEnergy && _gaveEnergy 
                    || !sameAsPreviousFrame)
            {
                _animator.StartIdle();
            }
        }

        _lastDirection = direction;
        _gaveEnergy = giveEnergy;
        _wasReloading = reload;
    }

    void OnEndGettingDown()
    {
        _isGettingDown = false;
        _wasReloading = true;
        _animator.StartAnim("reload");
    }

    void OnEndGettingUp()
    {
        _isGettingUp = false;
    }

    float GetGivenEnergy(float dt)
    {
        float givenEnergy = _givenEnergySpeed * dt;

        if (givenEnergy > _energy)
        {
            return _energy;
        }

        return givenEnergy;
    }

    void HandleOrientation(int direction, float dt)
    {
        float xMove = _speed * direction * dt;
        Vector3 move = new Vector3(xMove, 0, 0);

        Vector3 newPos = transform.position + move;

        if(!ColliderManager.instance.IsPossiblePosition(GetCollider(newPos)))
        {
            return;
        }

        transform.position = newPos;

        Vector3 scale = transform.localScale;
        scale.x = Mathf.Abs(scale.x) * direction;
        transform.localScale = scale;
    }

    void HandleReloading(bool canReload, float dt)
    {
        float energy = _energy;

        if (canReload)
        {
            energy += _reloadingSpeed * dt;

            if (_energy > _maxEnergy)
            {
                energy = _maxEnergy;
            }
        }

        SetEnergy(energy);
    }
}
