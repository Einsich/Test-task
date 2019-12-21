using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputSystem : MonoBehaviour
{
    const int EventCount = 3;//предполагается фиксированное кол-во хот-кеев
    //подсказка для инспектора
    [Header("Esc", order = 0),Header("Space", order = 1),Header("Enter",order = 2),Space(3)]
    //значения обрабатываемых клавиш и сами события
    [SerializeField] KeyCode[] keyCodes = new KeyCode[EventCount];
    [SerializeField] Action[] events = new Action[EventCount];

    public delegate void PressEvent(KeyCode code);
    public event PressEvent keyPressed;
    //интерфейс для подписки/ отписки
    public Action EscEvent { get => events[0]; set => events[0] = value; }
    public Action SpaceEvent { get => events[1]; set => events[1] = value; }
    public Action EnterAction { get => events[2]; set => events[2] = value; }


    void Start()
    {
        //тестирование
        EscEvent += A;
        SpaceEvent += B;
        EnterAction += C;
    }

    void Update()
    {
        if (!Input.anyKey)
            return;
        for (int i = 0; i < EventCount; i++)
            if (Input.GetKeyDown(keyCodes[i]))
                events[i]?.Invoke();
        if (keyPressed != null) 
        {
            for (KeyCode code = (KeyCode)0; code < KeyCode.End; code++)
                keyPressed(code);
            Debug.Log("Catch all input");
        }
    }
    public void SetAction(Action action, KeyCode code)
    {
        for (int i = 0; i < EventCount; i++)
            if (action == events[i])
            {
                keyCodes[i] = code;
            }
    }
    //тестирование
    private void A()
    {
        Debug.Log("Esc");
        keyPressed -= D;
    }
    private void B()
    {
        Debug.Log("Space");
        SetAction(SpaceEvent, KeyCode.A);
    }
    private void C()
    {
        Debug.Log("Enter");
        keyPressed += D;
    }
    private void D(KeyCode code)
    {
        
    }

}
