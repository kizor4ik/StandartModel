using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DropDownMessageCall : MonoBehaviour
{
    [SerializeField] private DropDownMessageDescription _messageDescription;
    [SerializeField] private GlobalEventsDescription _globalEventsDescription;
    [SerializeField] private Image _icon;
    [SerializeField] private Text _text;
    [SerializeField] TimeLineUIEnabler _timeLineCanvasEnabler;
  

    private Animator _anim;

    private Dictionary<string, DropDownMessage> _dictOfMessages = new Dictionary<string, DropDownMessage>();

    private GLOBAL_EVENT _lastDropedGlobalEvent;

    void Awake()
    {
        foreach (DropDownMessage message in _messageDescription.Message)
        {
            _dictOfMessages.Add(message.ID, message);
        }
        foreach (GlobalEvent globalEvent in _globalEventsDescription.ListOfGlobalEvents)
        {
            _dictOfMessages.Add(globalEvent.Message.ID, globalEvent.Message);
        }
        _anim = gameObject.GetComponent<Animator>();

        // Подписываемся на мировое событие
        GlobalEventsQualifier.GlobalEventTimeLineEvent += DropMessage;
    }

    private void SetUpData(DropDownMessage dropDownMessage)
    {
        _icon.sprite = dropDownMessage.Icon;
        _text.text = dropDownMessage.Text;
    }

    public void DropMessage(string id)
    {
        SetUpData(_dictOfMessages[id]);
        _anim.SetTrigger("DropMessage");
    }
    public void DropMessage(GLOBAL_EVENT id)
    {
        SetUpData(_dictOfMessages[id.ToString()]);
        _lastDropedGlobalEvent = id;
        _anim.SetTrigger("DropMessage");
    }

    public void ShowTimeLine()
    {
        _timeLineCanvasEnabler.Show(_lastDropedGlobalEvent);
    }

    // Test's
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.X))
        {

            DropMessage("X");
        }
    }

    private void OnDestroy()
    {
        GlobalEventsQualifier.GlobalEventTimeLineEvent -= DropMessage;
    }
}
