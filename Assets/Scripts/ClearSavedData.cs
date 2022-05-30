using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using BayatGames.SaveGameFree;

public class ClearSavedData : MonoBehaviour
{
    void Start()
    {
        BayatGames.SaveGameFree.SaveGame.Clear();
    }

}
