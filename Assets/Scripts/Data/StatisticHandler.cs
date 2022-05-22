using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using BayatGames.SaveGameFree;

public class StatisticHandler : MonoBehaviour
{
    [SerializeField] private ParticlesDescription _particlesDescription;

    private ParticleDetectionStatistic _stats = new ParticleDetectionStatistic();
    public ParticleDetectionStatistic Stats => _stats;

    private StatisticCollector _statisticCollector;
    private GlobalEventsQualifier _globalEventsQualifier;

    void Awake()
    {
        _statisticCollector = new StatisticCollector(this);
        _globalEventsQualifier = new GlobalEventsQualifier(this);
        Load();
    }

    public void AddParticleDetection(PARTICLENAME name)
    {
        _stats.RegisteredParticles[name]++;
        Debug.Log("Particle " + name + " grabbed! Total amount " + _stats.RegisteredParticles[name].ToString());
        Save();
        CheckPositronCountMessage();
        _globalEventsQualifier.CheckEvents();
    }

    private void Save()
    {
        SaveGame.Save<ParticleDetectionStatistic>(SAVE_LOAD_KEYS.GameStats.ToString(), _stats);
    }
    private void Load()
    {
        if (SaveGame.Exists(SAVE_LOAD_KEYS.GameStats.ToString()))
        {
            ParticleDetectionStatistic loadedData = SaveGame.Load<ParticleDetectionStatistic>(SAVE_LOAD_KEYS.GameStats.ToString(), new ParticleDetectionStatistic());
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

    // Мировое или нет событие с ID "BunchOfPositronDetection"
    public delegate void BunchOfPositronDetectionDelegate(string id);
    public static event BunchOfPositronDetectionDelegate BunchOfPositronDetectionEvent;

    public void CheckPositronCountMessage()
    {
        if (_stats.RegisteredParticles[PARTICLENAME.Positron] % 10 == 0)
        {
            BunchOfPositronDetectionEvent("BunchOfPositronDetection");
        }
    }

}
