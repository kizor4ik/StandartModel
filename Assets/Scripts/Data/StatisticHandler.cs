using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using BayatGames.SaveGameFree;

public class StatisticHandler : MonoBehaviour
{
    [SerializeField] private ParticlesDescription _particlesDescription;

    private const string STATISTIC_KEY = "GameStats";
    private ParticleDetectionStatistic _stats=new ParticleDetectionStatistic();
    private StatisticCollector _statisticCollector;


    void Awake()
    {
        _statisticCollector = new StatisticCollector(this);
        Load();
    }

    public void AddParticleDetection(PARTICLENAME name)
    {
        _stats.RegisteredParticles[name]++;
        Debug.Log("Particle " + name + " grabbed! Total amount " + _stats.RegisteredParticles[name].ToString());
        Save();
        CheckPositronCountMessage();
    }

    // Мировое событие с ID "BunchOfPositronDetection"
    public delegate void BunchOfPositronDetectionDelegate(string id);
    public static event BunchOfPositronDetectionDelegate BunchOfPositronDetectionEvent;

    public void CheckPositronCountMessage()
    {
        if (_stats.RegisteredParticles[PARTICLENAME.Positron] % 10 == 0)
        {
            BunchOfPositronDetectionEvent("BunchOfPositronDetection");
        }
    }

    private void Save()
    {
        SaveGame.Save<ParticleDetectionStatistic>(STATISTIC_KEY,_stats);
    }
    private void Load()
    {
        if (SaveGame.Exists(STATISTIC_KEY))
        {
            ParticleDetectionStatistic loadedData = SaveGame.Load<ParticleDetectionStatistic>(STATISTIC_KEY, new ParticleDetectionStatistic());
            _stats = loadedData;
        }
        else
        {
            foreach (Particle particle in _particlesDescription.ListOfParticles)
            {
                _stats.RegisteredParticles.Add(particle.Name, 0);
            }
        }
    }
    
}
