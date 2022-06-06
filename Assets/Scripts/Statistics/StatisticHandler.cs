using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;
using BayatGames.SaveGameFree;

public class StatisticHandler : MonoBehaviour
{
    [SerializeField] private ParticlesDescription _particlesDescription;
    [SerializeField] private GlobalEventsDescription _globalEventsDescription;

    private ParticleDetectionStatistic _stats = new ParticleDetectionStatistic();
    public ParticleDetectionStatistic Stats => _stats;

    private GlobalEventCompletion _globalEventCompletion = new GlobalEventCompletion();
    public GlobalEventCompletion GlobalEventCompletion => _globalEventCompletion;

    private ParticleDiscoveringCompletion _particleDiscoveringCompletion = new ParticleDiscoveringCompletion();
    public ParticleDiscoveringCompletion ParticleDiscoveringCompletion => _particleDiscoveringCompletion;

    private StatisticCollector _statisticCollector;
    private GlobalEventsQualifier _globalEventsQualifier;
    private ParticleDiscoveringEventQualifier _particleDiscoveringQualifier;

    void Awake()
    {  
        _statisticCollector = new StatisticCollector(this);
        _globalEventsQualifier = new GlobalEventsQualifier(this);
        _particleDiscoveringQualifier = new ParticleDiscoveringEventQualifier(this);
        Load();
        GlobalEventsQualifier.GlobalEventTimeLineEvent += MakrGlobalEventCompletion;
        ParticleDiscoveringEventQualifier.ParticleDiscoveryEvent += MarkParticleDiscoveringCompletion;
    }

    public void AddParticleDetection(PARTICLENAME changedParticle)
    {
        _stats.RegisteredParticles[changedParticle]++;
        Debug.Log("Particle " + changedParticle + " grabbed! Total amount " + _stats.RegisteredParticles[changedParticle].ToString());
        Save();
         _globalEventsQualifier.CheckEvents(changedParticle);
    }

    public void MakrGlobalEventCompletion(GLOBAL_EVENT eventId)
    {
        _globalEventCompletion.IsEventCompleted[eventId] = true;
        _particleDiscoveringQualifier.CheckEvents(eventId);
        Save();

    }
    public void MarkParticleDiscoveringCompletion(PARTICLENAME particleName)
    {
        _particleDiscoveringCompletion.IsEventCompleted[particleName] = true;
        Save();
    }
    private void Save()
    {
        SaveGame.Save<ParticleDetectionStatistic>(SAVE_LOAD_KEYS.GameStats.ToString(), _stats);
        SaveGame.Save<GlobalEventCompletion>(SAVE_LOAD_KEYS.GlobalEventCompletion.ToString(), _globalEventCompletion);
        SaveGame.Save<ParticleDiscoveringCompletion>(SAVE_LOAD_KEYS.UIRefreshEventsCompletion.ToString(), _particleDiscoveringCompletion);
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

        if (SaveGame.Exists(SAVE_LOAD_KEYS.GlobalEventCompletion.ToString()))
        {
            _globalEventCompletion = SaveGame.Load<GlobalEventCompletion>(SAVE_LOAD_KEYS.GlobalEventCompletion.ToString(), new GlobalEventCompletion());
        }
        else
        {
            foreach (GlobalEvent globalEvent in _globalEventsDescription.ListOfGlobalEvents)
            {
                _globalEventCompletion.IsEventCompleted.Add(globalEvent.Name, false);
            }
        }

        if (SaveGame.Exists(SAVE_LOAD_KEYS.UIRefreshEventsCompletion.ToString()))
        {
            _particleDiscoveringCompletion = SaveGame.Load<ParticleDiscoveringCompletion>(SAVE_LOAD_KEYS.UIRefreshEventsCompletion.ToString(), new ParticleDiscoveringCompletion());
        }
        else
        {
            foreach (PARTICLENAME particleName in Enum.GetValues(typeof(PARTICLENAME)))
            {       
                _particleDiscoveringCompletion.IsEventCompleted.Add(particleName, false);
            }
            _particleDiscoveringCompletion.IsEventCompleted[PARTICLENAME.Electron] = true;
            _particleDiscoveringCompletion.IsEventCompleted[PARTICLENAME.Positron] = true;
        }
    }

    private void OnDestroy()
    {
        GlobalEventsQualifier.GlobalEventTimeLineEvent -= MakrGlobalEventCompletion;
        ParticleDiscoveringEventQualifier.ParticleDiscoveryEvent -= MarkParticleDiscoveringCompletion;
    }

}
