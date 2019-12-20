using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class MotherDot : DotBase,IPointerDownHandler
{
    public float creationSpeed = 0.5f;
    public float orbitaRadius = 1f;
    public Teams team;
    int health = 20;
    public Text text;
    public List<Dot> orbita = new List<Dot>();
    void Start()
    {
        select = false;
        StartCoroutine(Spawn());
    }

    public void CreateMotherDot(Teams team, Vector2 position)
    {
        this.team = team;
        dot.color = Manager.getTeamColor(team);
        transform.position = position;
        text = Instantiate(text, FindObjectOfType<Canvas>().transform);
        text.transform.position = position;
        text.text = health.ToString();
    }

    IEnumerator Spawn()
    {
        while (true)
        {
            yield return new WaitForSeconds(creationSpeed);
            Dot dot = Manager.GetDot();
            dot.transform.position = transform.position;
            float a = Random.value * Mathf.PI * 2;
            dot.CreateDot(this, (Vector2)transform.position +  new Vector2(Mathf.Cos(a), Mathf.Sin(a)) * orbitaRadius);
            dot.select = select;
            orbita.Add(dot);
        }
    }
    public void Hit()
    {
        health--;
        text.text = health.ToString();
        if(health<=0)
        {
            gameObject.SetActive(false);
            text.gameObject.SetActive(false);
        }
    }
    public void OnPointerDown(PointerEventData eventData)
    {
        Manager.Tap(this);
    }
    public void Select(bool select)
    {
        this.select = select;
        foreach (var x in orbita)
            x.select = select;
    }
    public void Attack(MotherDot other)
    {
        foreach (var x in orbita)
            x.target = other.transform.position;
    }
}
