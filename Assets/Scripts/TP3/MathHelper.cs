using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class MathHelper 
{
    
    public static float VectorDistance(Vector3 a, Vector3 b) {
        return Mathf.Sqrt((Mathf.Pow(a.x - b.x, 2)) + 
                            (Mathf.Pow(a.y - b.y, 2)) + 
                                (Mathf.Pow(a.z - b.z, 2)));
    }
    // produit scalaire
    public static float DotDistance(Vector3 a, Vector3 b) { 
        return a.x * b.x + 
                a.y * b.y + 
                 a.z * b.z;
    }
    // produit vectoriel
    public static Vector3 CrossProduct(Vector3 a, Vector3 b) {
        Vector3 vector = new Vector3(a.y * b.z - a.z * b.y, 
                                        a.z * b.x - a.x * b.z, 
                                            a.x * b.y - a.y * b.x);
        return vector;
    }
    // calcul de l'angle entre deux vecteurs
    public static float AngleBetween(Vector3 a, Vector3 b) {

        return Mathf.Acos(DotDistance(a, b) / (a.magnitude * b.magnitude));
    }
}
