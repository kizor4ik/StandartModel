using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BeamInstaller : MonoBehaviour
{
    [SerializeField] private PARTICLENAME _leftBeamParticle;
    [SerializeField] private PARTICLENAME _rightBeamParticle;

    [SerializeField] private ParticlesDescription _particleDescription;
    [SerializeField] private ScatteringDescription _scatteringDescription;

    public Dictionary<PARTICLENAME, Particle> DictOfParticles = new Dictionary<PARTICLENAME, Particle>();
    public Dictionary<string, ScatteringProccess> DictOfProccesse = new Dictionary<string, ScatteringProccess>();

    [SerializeField] private Transform LeftBeamParent;
    [SerializeField] private Transform RightBeamParent;
    [SerializeField] private Transform EmittedParticlesParent;
    

    private void Awake()
    {
        foreach (Particle particle in _particleDescription.ListOfParticles)
        {
            DictOfParticles.Add(particle.Name, particle);
        }

        foreach (ScatteringProccess proccess in _scatteringDescription.ScatteringProcceses)
        {
            DictOfProccesse.Add(proccess.Reagents.BeamOne.ToString() + proccess.Reagents.BeamTwo.ToString(), proccess);
        }


        InstallEmittedParticles();

        InstallLeftBeam();

        InstallRightBeam();

    }

    private void InstallRightBeam()
    {
        GameObject RightBeam = Instantiate(DictOfParticles[_rightBeamParticle].BeamPrefab, Vector3.zero, Quaternion.identity);
        RightBeam.transform.SetParent(RightBeamParent);
        RightBeam.transform.localPosition = Vector3.zero;
    }

    private void InstallLeftBeam()
    {
        GameObject LeftBeam = Instantiate(DictOfParticles[_leftBeamParticle].BeamPrefab, Vector3.zero, Quaternion.identity);
        LeftBeam.transform.SetParent(LeftBeamParent);
        LeftBeam.transform.localPosition = Vector3.zero;
    }

    private void InstallEmittedParticles()
    {
        Reagents reagents = new Reagents(_leftBeamParticle, _rightBeamParticle);
        string key = reagents.BeamOne.ToString() + reagents.BeamTwo.ToString();
        if (!DictOfProccesse.ContainsKey(key))
        {
            key = reagents.BeamTwo.ToString() + reagents.BeamOne.ToString();
        }
        int channelIndex = Random.Range(0, DictOfProccesse[key].Channels.Count);
        foreach (PARTICLENAME emittedParticleName in DictOfProccesse[key].Channels[channelIndex].RadiatedParticles)
        {
            GameObject emittedParticle = Instantiate(DictOfParticles[emittedParticleName].EmittedParticlePrefab, Vector3.zero, Quaternion.identity);
            emittedParticle.transform.SetParent(EmittedParticlesParent);
            emittedParticle.transform.localPosition = Vector3.zero;
        }
    }
   

 
}
