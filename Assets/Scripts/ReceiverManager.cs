﻿using UnityEngine;
using System.Collections.Generic;
using Tools;

public class ReceiverManager : MonoSingleton<ReceiverManager>
{
    List<Receiver> _receivers = new List<Receiver>();

    public void Register(Receiver receiver)
    {
        _receivers.Add(receiver);
    }

    public void Unregister(Receiver receiver)
    {
        _receivers.Remove(receiver);
    }

    // Update is called once per frame
    public void Actualize(float dt)
    {
        if(PlayerRobot.instance.giveEnergy)
        {

        }
    }

    public Receiver TryToGiveEnergy(Vector2 wpos, float energy)
    {
        Receiver receiver = FindReceiver(wpos);

        if(receiver != null)
        {
            receiver.OnReloading(energy);
        }

        return receiver;
    }

    Receiver FindReceiver(Vector2 wpos)
    {
        float bestDist = float.MaxValue;
        Receiver best = null;

        for (int i = 0; i < _receivers.Count; i++)
        {
            Rect rect = _receivers[i].GetCollider();

            if (rect.Contains(wpos))
            {
                float dist = Vector2.Distance(rect.center, wpos);

                if (dist < bestDist)
                {
                    best = _receivers[i];
                    bestDist = dist;
                }
            }
        }

        return best;
    }
}