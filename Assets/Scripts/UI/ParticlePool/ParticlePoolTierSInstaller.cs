using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParticlePoolTierSInstaller : MonoBehaviour
{
    [SerializeField] private StatisticHandler _statisticHandler;
    [SerializeField] private Transform _quarkParent;
    [SerializeField] private Transform _leptonParent;
    [SerializeField] private Transform _gaugeBosonsParent;
    [SerializeField] private Transform _higgsParent;
    [SerializeField] private ParticlesDescription _particlesDescription;
    [SerializeField] private GameObject _buttonPrefab;

    private void Start()
    {
        foreach (Particle particle in _particlesDescription.ListOfParticles)
        {
            if (_statisticHandler.ParticleDiscoveringCompletion.IsEventCompleted[particle.Name])
            {
                var copy = Instantiate(_buttonPrefab);
                if (particle.Spin == "1/2")
                {
                    copy.transform.SetParent(_leptonParent);
                }
                if (particle.Spin == "1")
                {
                    copy.transform.SetParent(_gaugeBosonsParent);
                }
                ParticlePoolButton particleButton = copy.GetComponent<ParticlePoolButton>();
                particleButton.SetProperties(particle.Name, particle.Icon);
                copy.transform.localPosition = new Vector3(Random.Range(-500, 500), Random.Range(-300, 300), 0);
                copy.SetActive(true);
            }
        }
    }
}
