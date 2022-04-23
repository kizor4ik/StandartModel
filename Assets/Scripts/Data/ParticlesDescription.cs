using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[CreateAssetMenu(fileName = "ParticlesDescription", menuName = "DataDescription/ParticlesDescription", order = 51)]
public class ParticlesDescription : ScriptableObject
{
    [SerializeField] private List<Particle> _listOfParticles;

    public List<Particle> ListOfParticles => _listOfParticles;

}
