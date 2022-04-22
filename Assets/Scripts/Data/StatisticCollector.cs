using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using BayatGames.SaveGameFree;


public class StatisticCollector 
{
    public ParticleDetectionStatistic Stats = new ParticleDetectionStatistic();
    private string _dataKey;

    public StatisticCollector(ParticlesDescription particlesDescription, string dataKey)
    {
        foreach (Particle particle in particlesDescription.ListOfParticles)
        {
            Stats.RegisteredParticles.Add(particle.Name, 0);
        }
        // Подписка на подбор частицы
        EmittedParticle.GrabParticleEvent += AddParticleDetection;
        _dataKey = dataKey;
        Load();
    }

    private void AddParticleDetection(PARTICLENAME name)
    {
        Stats.RegisteredParticles[name]++;
        Debug.Log("Particle " + name + " grabbed! Total amount " + Stats.RegisteredParticles[name].ToString());
        Save();
    }

    void Save()
    {
        SaveGame.Save<ParticleDetectionStatistic>(_dataKey, Stats);
    }
    public void Load()
    {
        if (SaveGame.Exists(_dataKey))
        {
            ParticleDetectionStatistic loadedData = SaveGame.Load<ParticleDetectionStatistic>(_dataKey, new ParticleDetectionStatistic());
            Stats = loadedData;
        }
    }
}
