using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Waypoint : MonoBehaviour
{
    public bool isOpen;
    public float heuristic;
    public float cost;
    public float fNumber;
    public Waypoint opener;
    public List<Waypoint> closeWaypoints;

    
}
