using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using BayatGames.SaveGameFree;


public class StatisticCollector 
{
    private StatisticHandler _dataHandler;

    public StatisticCollector(StatisticHandler handler)
    {
        _dataHandler = handler;
        // Подписка на подбор частицы
        EmittedParticle.GrabParticleEvent += AddParticleDetection;
    }

    private void AddParticleDetection(PARTICLENAME name)
    {
        _dataHandler.AddParticleDetection(name);
    }


}
