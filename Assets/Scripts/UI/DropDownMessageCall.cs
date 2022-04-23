using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DropDownMessageCall : MonoBehaviour
{
    [SerializeField] private DropDownMessageDescription _messageDescription;
    [SerializeField] private Image _icon;
    [SerializeField] private Text _text;

    private Animator _anim;

    private Dictionary<string, DropDownMessage> _dictOfMessages = new Dictionary<string, DropDownMessage>();

    void Awake()
    {
        foreach (DropDownMessage message in _messageDescription.Message)
        {
            _dictOfMessages.Add(message.ID, message);
        }
        _anim = gameObject.GetComponent<Animator>();

        // Тестовая подписка на очередное детектрирования 10 частиц
        StatisticHandler.BunchOfPositronDetectionEvent += DropMessage;
    }

    private void SetUpData(string id)
    {
        _icon.sprite = _dictOfMessages[id].Icon;
        _text.text = _dictOfMessages[id].Text;
    }

    public void DropMessage(string id)
    {
        SetUpData(id);
        _anim.SetTrigger("DropMessage");
    }


    // Test's
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.X))
        {

            DropMessage("1");
        }
    }
}
