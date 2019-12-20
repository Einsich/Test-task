using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Dot : DotBase
{
    public Vector2 target;
    MotherDot mother;
    public float Speed = 0.5f; 
    public void CreateDot(MotherDot mother, Vector2 target)
    {
        this.mother = mother;
        this.target = target;
        dot.color = Manager.getTeamColor(mother.team);

    }
    void Update()
    {
        Vector2 d = (target - (Vector2)transform.position);
        if (d.magnitude < Time.deltaTime * Speed)
            transform.position = target;
        else
            transform.position += (Vector3)(d.normalized * Time.deltaTime * Speed);
    }
    public void Die()
    {
        if (!gameObject.activeSelf)
            return;
        mother.orbita.Remove(this);
        Manager.SetDot(this);
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        
        
        Dot other;
        MotherDot otherM;
        if(other = collision.gameObject.GetComponent<Dot>())
        {
            if (other.mother.team != mother.team)
            {
                Debug.Log("die");
                other.Die();
                Die();
            }
        } else if(otherM = collision.gameObject.GetComponent<MotherDot>())
        {
            
            if (otherM.team != mother.team)
            {
                Debug.Log("die mother");
                Die();
                otherM.Hit();
            }
        }
    }
}
