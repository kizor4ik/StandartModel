using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using BayatGames.SaveGameFree;

public class BeamInstaller : MonoBehaviour
{
    //private string INITIALBEAMS_KEY = SAVE_LOAD_KEYS.InitialBeams.ToString();//"Initital Beams";
    private InitialBeams _initialBeams = new InitialBeams();
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
        // Необязтаельный двойной цикл, чтобы заполнить пустые процессы без ошибки.
        // Он не нужен, если все каналы распадов записать вручную
        // Можно удалить в финальном билде, чтобы не жрал производительсность
        //------------------
        foreach (Particle particle1 in _particleDescription.ListOfParticles)
        {
            foreach (Particle particle2 in _particleDescription.ListOfParticles)
            {
                if (DictOfProccesse.ContainsKey(particle1.Name.ToString() + particle2.Name.ToString()) || DictOfProccesse.ContainsKey(particle2.Name.ToString() + particle1.Name.ToString()))
                {
                    continue;
                }
                else
                {
                    ScatteringProccess emptyProccess = new ScatteringProccess();
                    Channel emptyChannel = new Channel();
                    emptyChannel.ProbabilityOfChannel = 1;
                    emptyChannel.RadiatedParticles = new List<PARTICLENAME>();
                    emptyChannel.RadiatedParticles.Add(particle1.Name);
                    emptyChannel.RadiatedParticles.Add(particle2.Name);
                    emptyProccess.Reagents = new Reagents(particle1.Name,particle2.Name);
                    emptyProccess.Channels = new List<Channel>();
                    emptyProccess.Channels.Add(emptyChannel);
                    DictOfProccesse.Add(particle1.Name.ToString() + particle2.Name.ToString(), emptyProccess);
                   // Debug.Log(particle1.Name.ToString() + particle2.Name.ToString() + " Does not Exists");
                }
            }
        }
        //------------------------------
        Load();
        InstallParticleConfiguration();

        // Подписываемся на изменения состава пучка
        ParticlePoolButton.ChooseParticleEvent += SetBeamParticle;
    }
    private void OnDestroy()
    {
        ParticlePoolButton.ChooseParticleEvent -= SetBeamParticle;
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
        Save();
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

    private void Save()
    {
        _initialBeams.LeftBeamParticle = _leftBeamParticle;
        _initialBeams.RightBeamParticle = _rightBeamParticle;
        SaveGame.Save<InitialBeams>(SAVE_LOAD_KEYS.InitialBeams.ToString(), _initialBeams);
    }
    private void Load()
    {
        if (SaveGame.Exists(SAVE_LOAD_KEYS.InitialBeams.ToString()))
        {
            InitialBeams loadedData = SaveGame.Load<InitialBeams>(SAVE_LOAD_KEYS.InitialBeams.ToString(), new InitialBeams());
            _leftBeamParticle = loadedData.LeftBeamParticle;
            _rightBeamParticle = loadedData.RightBeamParticle;
        }
    }
}
