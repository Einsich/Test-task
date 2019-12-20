using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Manager : MonoBehaviour
{
    public MotherDot dotMotherPrefab;
    static Dot dotPrefab;
    List<MotherDot> motherDots = new List<MotherDot>();
    static int teamsCount = 2;
    private void Awake()
    {
        dotPrefab = Resources.Load<Dot>("Dot");
    }
    void Start()
    {
        float da = 360f / teamsCount;
        for (int i = 0; i <teamsCount; i++)
        {
            MotherDot buf;
            motherDots.Add(buf = Instantiate(dotMotherPrefab));
            buf.CreateMotherDot((Teams)i, Quaternion.Euler(0, 0, i * da) * new Vector2(3, 0));
        }
    }

    // Update is called once per frame
    void Update()
    {

    }
    static Color[] teamsColors = { Color.red, Color.blue };
    public static Color getTeamColor(Teams team) => teamsColors[(int)team];

    static Stack<Dot> pull = new Stack<Dot>();
    public static Dot GetDot() {
        Dot dot = pull.Count == 0 ? Instantiate(dotPrefab) : pull.Pop();

        dot.gameObject.SetActive(true);
        return dot;
    }
    public static void SetDot(Dot dot)
    {
        dot.gameObject.SetActive(false);
        pull.Push(dot);
    }
    static MotherDot curTaped = null;
    public static void Tap(MotherDot taped)
    {
        if (curTaped == null)
        {
            curTaped = taped;
            curTaped.Select( true);
        }
        else
        {
            if (curTaped == taped || taped == null)
            {
                curTaped.Select(false);
                curTaped = null;
            }
            else
            {
                curTaped.Attack(taped);
            }
        }
    }
    
}
public enum Teams
{
    Red,
    Blue
}