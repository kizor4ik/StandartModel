
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ParticlePanel : MonoBehaviour
{
    [SerializeField] private Image _icon;
    [SerializeField] private Text _name;
    [SerializeField] private Text _spin;

    [SerializeField] StatisticHandler _statisticHandler;
    [SerializeField] private ParticlesDescription _particleDescription;
    public Dictionary<PARTICLENAME, Particle> DictOfParticles = new Dictionary<PARTICLENAME, Particle>();

    private void Awake()
    {
        // Собираем необходимые словари
        foreach (Particle particle in _particleDescription.ListOfParticles)
        {
            DictOfParticles.Add(particle.Name, particle);
        }

        // Подписываемся на изменения состава пучка
        ParticlePoolButton.ChooseParticleEvent += RefreshPanel;
    }

    public void RefreshPanel(PARTICLENAME particleName)
    {
        Particle curParticle = DictOfParticles[particleName];
        _icon.sprite = curParticle.Icon;
        
        _name.text = string.Format("Name: {0}", curParticle.Name.ToString());
        if (_statisticHandler.GlobalEventCompletion.IsEventCompleted[GLOBAL_EVENT.SpinDiscovery])
        {
            _spin.text = string.Format("Spin: {0}", curParticle.Spin.ToString());
        }
    }

    private void OnDestroy()
    {
        ParticlePoolButton.ChooseParticleEvent -= RefreshPanel;
    }
}
