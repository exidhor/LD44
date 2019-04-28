using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using Tools;

public class PlayerRobot : MonoSingleton<PlayerRobot>
{
    public float maxEnergy;
    public float energy;
    public float reloadingSpeed;
    public float givenEnergySpeed;
    public Slider energySlider;
    [SerializeField] SpriteAnimator _animator;
    [SerializeField] GameObject _camera;

    // for anims
    int _lastDirection;
    bool _gaveEnergy;
    bool _wasReloading;

    public float speed;

    void Awake()
    {
        energy = maxEnergy;
        energySlider.value = energy / maxEnergy;

        _lastDirection = -1000000;

        _animator.StartIdle();

        RefreshCameraPosition();
    }

    void RefreshCameraPosition()
    {
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
            HandleOrientation(direction, dt);
        }

        bool tryToGiveEnergy = !isMoving && Input.GetKey(KeyCode.Space);
        bool giveEnergy = false;

        if (tryToGiveEnergy)
        {
            float givenEnergy = GetGivenEnergy(dt);
            Receiver receiver = ReceiverManager.instance.TryToGiveEnergy(transform.position, givenEnergy);

            if (receiver != null)
            {
                energy -= givenEnergy;
                giveEnergy = true;
            }
        }

        bool canReload = !isMoving && !giveEnergy;
        HandleReloading(canReload, dt);
        UpdateAnim(direction, giveEnergy, canReload);

        RefreshCameraPosition();
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
                _animator.StartAnim("reload");
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

    float GetGivenEnergy(float dt)
    {
        float givenEnergy = givenEnergySpeed * dt;

        if (givenEnergy > energy)
        {
            return energy;
        }

        return givenEnergy;
    }

    void HandleOrientation(int direction, float dt)
    {
        float xMove = speed * direction * dt;
        Vector3 move = new Vector3(xMove, 0, 0);

        transform.position += move;

        Vector3 scale = transform.localScale;
        scale.x = Mathf.Abs(scale.x) * direction;
        transform.localScale = scale;
    }

    void HandleReloading(bool canReload, float dt)
    {
        if (canReload)
        {
            energy += reloadingSpeed * dt;

            if (energy > maxEnergy)
            {
                energy = maxEnergy;
            }
        }

        energySlider.value = energy / maxEnergy;
    }
}
