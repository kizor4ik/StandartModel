using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//using BayatGames.SaveGameFree;

public class StatisticHandler : MonoBehaviour
{
    [SerializeField] private ParticlesDescription _particlesDescription;

    private const string STATISTIC_KEY = "GameStats";
    private ParticleDetectionStatistic _stats;
    private StatisticCollector _statisticCollector;

    void Awake()
    {
        _statisticCollector = new StatisticCollector(_particlesDescription, STATISTIC_KEY);
        _stats = _statisticCollector.Stats;
    }


    // Для тестов
    //private void Update()
    //{
    //    if (Input.GetKeyDown(KeyCode.X))
    //    {
    //        _stats = _statisticCollector.Stats;
    //        foreach (Particle particle in _particlesDescription.ListOfParticles)
    //        {
    //            Debug.Log(particle.Name + " " + _stats.RegisteredParticles[particle.Name].ToString());
    //        }

    //    }
    //}
}
