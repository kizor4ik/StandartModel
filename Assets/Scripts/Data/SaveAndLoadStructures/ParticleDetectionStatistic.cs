using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParticleDetectionStatistic
{
    //Ключ - имя частицы, значение - количество задетектрированных событий.
    public Dictionary<PARTICLENAME, int> RegisteredParticles = new Dictionary<PARTICLENAME, int>();
}
