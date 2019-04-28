using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour {

    [SerializeField] PlayerRobot _player;

	void Update () {
        float dt = Time.deltaTime;

        PlantManager.instance.CheckForInit();

        _player.Actualize(dt);
        ReceiverManager.instance.Actualize(dt);
        EnergyModuleManager.instance.Actualize();
        PlantManager.instance.Actualize(dt);
	}
}
