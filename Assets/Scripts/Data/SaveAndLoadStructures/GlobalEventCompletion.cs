using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GlobalEventCompletion 
{
    //Ключ - имя события, значение - бул - произошло ли событие.
    public Dictionary<GLOBAL_EVENT, bool> IsEventCompleted = new Dictionary<GLOBAL_EVENT, bool>();
}
