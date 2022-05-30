using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GlobalEventSubscriber 
{
    private TimeLineConstructor _timeLineConstructor;
    private StatisticHandler _statisticHandler;
    public GlobalEventSubscriber(TimeLineConstructor timeLineConstructor, StatisticHandler statisticHandler)
    {
        _timeLineConstructor = timeLineConstructor;
        _statisticHandler = statisticHandler;
        // Подписчик мирового отображаемого события
        GlobalEventsQualifier.GlobalEventTimeLineEvent += DetectEvent;
    }

    public void DetectEvent(GLOBAL_EVENT eventId)
    {
        _statisticHandler.MakrGlobalEventCompletion(eventId);
        _timeLineConstructor.UpdateTimeLine(eventId);
    }
}
