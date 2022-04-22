using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StatisticCollector : MonoBehaviour
{
    [SerializeField] private ParticlesDescription _particlesDescription;

    public Dictionary<PARTICLENAME, int> RegisteredParticles = new Dictionary<PARTICLENAME, int>();

    private void Awake()
    {
        // Собираем словарь. Ключ - имя частицы, значение - количество задетектрированных событий.
        foreach (Particle particle in _particlesDescription.ListOfParticles)
        {
            RegisteredParticles.Add(particle.Name, 0);
        }
    }

    private void Start()
    {
        EmittedParticle.GrabParticleEvent += AddParticleDetection;
    }

    public void AddParticleDetection(PARTICLENAME name)
    {
        RegisteredParticles[name]++;
    }

    public void GetParticlesStatistic(PARTICLENAME name)
    {
        Debug.Log(RegisteredParticles[name]);
    }


    //private void Update()
    //{
    //    if (Input.GetKeyDown(KeyCode.X))
    //    {
    //        foreach (Particle particle in _particlesDescription.ListOfParticles)
    //        {
    //            Debug.Log(particle.Name + " " + RegisteredParticles[particle.Name].ToString());
    //        }
    //    }
    //}

}
