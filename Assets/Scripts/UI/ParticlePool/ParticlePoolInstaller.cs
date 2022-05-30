using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParticlePoolInstaller : MonoBehaviour
{
    [SerializeField] private StatisticHandler _statisticHandler;
    [SerializeField] private Transform _buttonsParent;
    [SerializeField] private ParticlesDescription _particlesDescription;
    [SerializeField] private GameObject _buttonPrefab;
    private ParticleDiscoveringEventSubscriber _uiRefreshEventSubscriber;

    private void Start()
    {
        foreach (Particle particle in _particlesDescription.ListOfParticles)
        {
            if (_statisticHandler.ParticleDiscoveringCompletion.IsEventCompleted[particle.Name])
            {
                var copy = Instantiate(_buttonPrefab);
                copy.transform.SetParent(_buttonsParent);
                ParticlePoolButton particleButton = copy.GetComponent<ParticlePoolButton>();
                particleButton.SetProperties(particle.Name, particle.Icon);
                copy.transform.localPosition = new Vector3(Random.Range(-500, 500), Random.Range(-300, 300), 0);
                copy.SetActive(true);
            }
        }
        _uiRefreshEventSubscriber = new ParticleDiscoveringEventSubscriber(_statisticHandler, this);
        gameObject.SetActive(false);
    }

    public void RefreshUI(PARTICLENAME particleName)
    {
        if (particleName==PARTICLENAME.Photon)
        Debug.Log("We are refreshing photon button");
    }
}
