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

    [SerializeField] private Transform _leftBeamParent;
    [SerializeField] private Transform _rightBeamParent;
    [SerializeField] private Transform _emittedParticlesParent;

    // Булики нужны, чтобы понимать какая из пучков меняется через UI
    private bool _isLeftInstalling = false;
    private bool _isRightInstalling = false;

    private void Awake()
    {
        // Собираем необходимые словари
        foreach (Particle particle in _particleDescription.ListOfParticles)
        {
            DictOfParticles.Add(particle.Name, particle);
        }

        foreach (ScatteringProccess proccess in _scatteringDescription.ScatteringProcceses)
        {
            DictOfProccesse.Add(proccess.Reagents.BeamOne.ToString() + proccess.Reagents.BeamTwo.ToString(), proccess);
        }

        InstallParticleConfiguration();

        // Подписываемся на изменения состава пучка
        ParticlePoolButton.ChooseParticleEvent += SetBeamParticle;
    }

    private void InstallRightBeam()
    {
        GameObject RightBeam = Instantiate(DictOfParticles[_rightBeamParticle].BeamPrefab, Vector3.zero, Quaternion.identity);
        RightBeam.transform.SetParent(_rightBeamParent);
        RightBeam.transform.localPosition = Vector3.zero;
    }

    private void InstallLeftBeam()
    {
        GameObject LeftBeam = Instantiate(DictOfParticles[_leftBeamParticle].BeamPrefab, Vector3.zero, Quaternion.identity);
        LeftBeam.transform.SetParent(_leftBeamParent);
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
            emittedParticle.transform.SetParent(_emittedParticlesParent);
            emittedParticle.transform.localPosition = Vector3.zero;
        }
    }

    private void ClearPreviosInstallation()
    {
        foreach (Transform child in _leftBeamParent)
        {
            Destroy(child.gameObject);
        }
        foreach (Transform child in _rightBeamParent)
        {
            Destroy(child.gameObject);
        }
        foreach (Transform child in _emittedParticlesParent)
        {
            Destroy(child.gameObject);
        }
    }

    private void InstallParticleConfiguration()
    {
        ClearPreviosInstallation();
        InstallEmittedParticles();
        InstallLeftBeam();
        InstallRightBeam();
    }

    public void SetBeamParticle(PARTICLENAME particleName)
    {
        if (_isLeftInstalling)
        {
            _leftBeamParticle = particleName;
        }
        if (_isRightInstalling)
        {
            _rightBeamParticle = particleName;
        }
        InstallLeftBeam();
        InstallParticleConfiguration();
    }

    public void MarkLeftBeam()
    {
        _isLeftInstalling = true;
        _isRightInstalling = false;
    }
    public void MarkRightBeam()
    {
        _isLeftInstalling = false;
        _isRightInstalling = true;
    }


}
