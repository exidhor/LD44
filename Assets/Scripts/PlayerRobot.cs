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

    public GameObject solarPanel;
    public float speed;

    public bool giveEnergy
    {
        get { return _giveEnergy; }
    }

    bool _giveEnergy;

    void Awake()
    {
        energy = maxEnergy;
        energySlider.value = energy / maxEnergy;
    }

    public void Actualize(float dt)
    {
        int direction = 0;
        if(Input.GetKey(KeyCode.LeftArrow))
        {
            direction = -1;
        }
        else if(Input.GetKey(KeyCode.RightArrow))
        {
            direction = 1;
        }

        bool isMoving = direction != 0;
        if (isMoving)
        {
            HandleOrientation(direction, dt);
        }

        bool tryToGiveEnergy = !isMoving && Input.GetKey(KeyCode.Space);

        if(tryToGiveEnergy)
        {
            float givenEnergy = GetGivenEnergy(dt);
            Receiver receiver = ReceiverManager.instance.TryToGiveEnergy(transform.position, givenEnergy);

            if (receiver != null)
            {
                energy -= givenEnergy;
                _giveEnergy = true;
            }
            else
            {
                _giveEnergy = false;
            }
        }
        else
        {
            _giveEnergy = false;
        }

        HandleReloading(!isMoving && !_giveEnergy, dt);
    }


    float GetGivenEnergy(float dt)
    {
        float givenEnergy = givenEnergySpeed * dt;

        if(givenEnergy > energy)
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
        solarPanel.gameObject.SetActive(canReload);

        if(canReload)
        {
            energy += reloadingSpeed * dt;

            if(energy > maxEnergy)
            {
                energy = maxEnergy;
            }
        }

        energySlider.value = energy / maxEnergy;
    }
}
