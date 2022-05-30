using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParticleDiscoveringEventSubscriber 
{
    private ParticlePoolInstaller _particlePoolInstaller;
    private StatisticHandler _statisticHandler;

    public ParticleDiscoveringEventSubscriber( StatisticHandler statisticHandler, ParticlePoolInstaller particlePoolInstaller)
    {
        _particlePoolInstaller = particlePoolInstaller;
        _statisticHandler = statisticHandler;
        // Подписчик мирового отображаемого события
        ParticleDiscoveringEventQualifier.ParticlePoolRefreshEvent += DetectEvent;
    }

    public void DetectEvent(PARTICLENAME particleName)
    {
        _statisticHandler.MarkParticleDiscoveringCompletion(particleName);
        _particlePoolInstaller.RefreshUI(particleName);
    }
}
