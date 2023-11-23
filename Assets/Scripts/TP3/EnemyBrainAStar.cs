using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Scripting.APIUpdating;

// Je n'ai pas compris les étapes à réaliser pour A* même en ayant compris son principe, ainsi j'ai commenté le code d'une solution pour essayer de le comprendre. 
public class EnemyBrainAStar : MonoBehaviour
{
    public List<Waypoint> waypoints;
    public List<Waypoint> path;

    public Waypoint currentWaypoint;
    public Waypoint finalWaypoint;
    public Waypoint finalClosestWaypoint;

    public int currentWaypointIndex = 0;
    public float speed = 5f;
    public bool isTargetTouched = false;

    private void Awake() {
        finalClosestWaypoint = FindClosestWaypoint(finalWaypoint.gameObject);
        currentWaypoint = FindClosestWaypoint(this.gameObject);
    }

    void Start() {
        ShorterPath(currentWaypoint);
    }

    // Définit la distance entre chaque waypoint
    void DefineWaypointHeuristic() {
        if(waypoints != null) {
            foreach(Waypoint waypoint in waypoints) {
                waypoint.heuristic = MathHelper.VectorDistance(waypoint.transform.position, finalWaypoint.transform.position);
            }
        }
    }

    void FixedUpdate() {
        Move();
        CheckPlayerPosition();
    }

    // Définit le parcours le plus court pour l'ennemi dans le cas où le joueur se déplace
    void CheckPlayerPosition() {
        Waypoint currentClosestWaypoint = FindClosestWaypoint(finalWaypoint.gameObject);
        if(FindClosestWaypoint(finalWaypoint.gameObject) != finalClosestWaypoint) {

            // Dans le cas où le joueur a changé de place,
            // le chemin que doit parcourir l'ennemi est rafraichi pour lui indiquer le nouveau chemin le plus court
            finalClosestWaypoint = currentClosestWaypoint;
            path.Clear();
            ShorterPath(currentWaypoint);
        }
    }

    // Définit le mouvement de l'ennemi à travers les différents waypoints
    void Move() {
        if(currentWaypoint != null) {
            Vector3 direction = currentWaypoint.gameObject.transform.position - transform.position;
            transform.rotation = Quaternion.LookRotation(direction, Vector3.up);

            // Permet de vérifier qu'on est arrivé à destination,
            // sans causer des tremblements de l'ennemi à cause de la trop grosse proximité.
            // Lorsque l'ennemi est toujours en chemin vers le joueur, le premier waypoint de la pile représentant le chemin à faire est retiré,
            // puis vérifie qu'il reste du chemin à parcourir et indique à l'ennemi d'aller en direction du premier gameobject de la pile.

            if (MathHelper.VectorDistance(currentWaypoint.gameObject.transform.position, transform.position) <= 0.3f) {
                path.Remove(currentWaypoint);
                if(path.Count > 0) {
                    currentWaypoint = path[0];
                    return;
                }
                else {
                    return;
                }
            }

            Vector3 velocite = speed * Time.deltaTime * direction.normalized;

            transform.Translate(velocite, Space.World);
        }
    }

    // Renvoie le waypoint le plus proche
    Waypoint FindClosestWaypoint(GameObject target) {
        Waypoint closestWaypoint = null;
        float distanceToCurrentClosestWaypoint = 0;
        foreach (Waypoint waypoint in waypoints)
        {
            if(closestWaypoint == null) {
                closestWaypoint = waypoint;
                distanceToCurrentClosestWaypoint = MathHelper.VectorDistance(target.transform.position, waypoint.transform.position);
            }
            else {
                float distanceToWaypoint = MathHelper.VectorDistance(target.transform.position, waypoint.transform.position);
                if(distanceToWaypoint < distanceToCurrentClosestWaypoint) {
                    closestWaypoint = waypoint;
                    distanceToCurrentClosestWaypoint = distanceToWaypoint;
                }
            }
            
        }
        return closestWaypoint;
    }

    // Définit le chemin le plus court, on empile les waypoints ouverts et on met dans une liste les waypoints fermés.
    void ShorterPath(Waypoint start) {
        Stack<Waypoint> openWaypoints = new Stack<Waypoint>();
        List<Waypoint> closedWaypoints = new List<Waypoint>();
        openWaypoints.Push(start);
        DefineWaypointHeuristic();
        int counter = 0;
        while(openWaypoints.Count > 0 && counter < 1000) {
            Waypoint currentWaypoint = openWaypoints.Pop();
            
            if(currentWaypoint == finalClosestWaypoint) {
                Stack<Waypoint> completePath = RebuildPath(currentWaypoint);
                while(completePath.Count > 0) {
                    path.Add(completePath.Pop());
                }
                path.Add(finalWaypoint);
                return;
            }

            foreach (Waypoint waypoint in currentWaypoint.closeWaypoints)
            {
                if (!closedWaypoints.Contains(waypoint) && waypoint.fNumber <= currentWaypoint.fNumber) {
                    waypoint.cost += 1;
                    waypoint.fNumber = waypoint.heuristic + waypoint.cost;
                    openWaypoints.Push(waypoint);
                    waypoint.opener = currentWaypoint;
                }
            }
            closedWaypoints.Add(currentWaypoint);
            counter++;
        }
        return;

    }

    // Renvoie une pile path qui contient le chemin le plus court pour atteindre sa cible si elle a changé de place. 
    Stack<Waypoint> RebuildPath(Waypoint finalWaypoint) {
        Stack<Waypoint> path = new Stack<Waypoint>();
        Waypoint currentWaypoint = finalWaypoint;
        path.Push(currentWaypoint);
        int counter = 0;
        while(currentWaypoint.opener != null && counter < 1000) {
            currentWaypoint = currentWaypoint.opener;
            path.Push(currentWaypoint);
            counter++;
        }
        return path;
    }
}
