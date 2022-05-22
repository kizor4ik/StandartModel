using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GlobalEventSubscriber 
{
    private TimeLineConstructor _timeLineConstructor;
    public GlobalEventSubscriber(TimeLineConstructor timeLineConstructor)
    {
        _timeLineConstructor = timeLineConstructor;
        // Ниже нужно подписаться на все мировые события
        GlobalEventsQualifier.TestPhotonDiscoveryEvent += DetectEvent;
        GlobalEventsQualifier.ElectronDiscoveryEvent += DetectEvent;
        GlobalEventsQualifier.PositronDiscoveryEvent += DetectEvent;
    }

    public void DetectEvent(GLOBAL_EVENT eventId)
    {
        _timeLineConstructor.UpdateTimeLine(eventId);
    }
}
