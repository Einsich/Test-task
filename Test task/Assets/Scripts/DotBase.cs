using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DotBase : MonoBehaviour
{
    [SerializeField] SpriteRenderer selectSprite;
    [SerializeField] protected SpriteRenderer dot;
    void Start()
    {

    }
    bool selected = true;
    public bool select { set { selected = selectSprite.enabled = value; } get => selected; }
}
