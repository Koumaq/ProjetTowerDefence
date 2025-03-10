using UnityEngine;

public class PointDepart : MonoBehaviour
{
    public static Vector3 GetPointDepartAleatoire(float largeur, float hauteur, float tailleCase)
    {
        // Génère un point de départ aléatoire dans les limites de la grille
        float x = Random.Range(0, largeur) * tailleCase;
        float y = Random.Range(0, hauteur) * tailleCase;

        // Ajustement pour ne pas sortir de la grille
        x = Mathf.Clamp(x, 0, largeur * tailleCase);
        y = Mathf.Clamp(y, 0, hauteur * tailleCase);

        return new Vector3(x, y, 0);
    }

}
