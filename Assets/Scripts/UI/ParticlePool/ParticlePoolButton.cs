using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParticlePoolButton : MonoBehaviour
{
    [SerializeField] private PARTICLENAME _particleName;
    [SerializeField] private UnityEngine.UI.Image _icon;

    //объявляем событие, что мы выбрали частицу
    public delegate void ChooseParticleDelegate(PARTICLENAME particleName);
    public static event ChooseParticleDelegate ChooseParticleEvent;

    public void OnClick()
    {
        ChooseParticleEvent(_particleName);
    }

    public void SetProperties(PARTICLENAME particleName, Sprite icon)
    {
        _particleName = particleName;
        _icon.sprite = icon;
    }
}
