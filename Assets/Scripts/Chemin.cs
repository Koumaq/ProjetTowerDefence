using Assets.Scripts;
using System.Collections.Generic;
using UnityEngine;

public class Chemin
{
    public List<Vector3> CalculerChemin(Vector3 depart, Vector3 arrivee, float tailleCase, Carte carte)
    {
        List<Vector3> chemin = new List<Vector3>();
        Vector3 positionActuelle = depart;

        // Assurer que la destination est � l'int�rieur de la grille
        arrivee.x = Mathf.Clamp(arrivee.x, 0, carte.largeur * tailleCase - tailleCase); // Limiter en X
        arrivee.y = Mathf.Clamp(arrivee.y, 0, carte.hauteur * tailleCase - tailleCase); // Limiter en Y

        // Ajouter la position de d�part au chemin (arrondi aux coordonn�es de la grille)
        chemin.Add(new Vector3(Mathf.Round(positionActuelle.x / tailleCase) * tailleCase, Mathf.Round(positionActuelle.y / tailleCase) * tailleCase, 0));

        // Calculer le chemin en suivant les cases de la grille
        while (Vector3.Distance(positionActuelle, arrivee) > tailleCase)
        {
            // D�placement horizontal
            if (Mathf.Abs(arrivee.x - positionActuelle.x) > Mathf.Abs(arrivee.y - positionActuelle.y))
            {
                positionActuelle.x += (arrivee.x > positionActuelle.x) ? tailleCase : -tailleCase;
            }
            else // D�placement vertical
            {
                positionActuelle.y += (arrivee.y > positionActuelle.y) ? tailleCase : -tailleCase;
            }

            // Limiter la position � l'int�rieur de la grille
            positionActuelle.x = Mathf.Clamp(positionActuelle.x, 0, carte.largeur * tailleCase - tailleCase);
            positionActuelle.y = Mathf.Clamp(positionActuelle.y, 0, carte.hauteur * tailleCase - tailleCase);

            // Ajouter l'�tape au chemin, arrondie � la taille de la case
            chemin.Add(new Vector3(Mathf.Round(positionActuelle.x / tailleCase) * tailleCase, Mathf.Round(positionActuelle.y / tailleCase) * tailleCase, 0));
        }

        // Ajouter la position finale au chemin
        chemin.Add(new Vector3(Mathf.Round(arrivee.x / tailleCase) * tailleCase, Mathf.Round(arrivee.y / tailleCase) * tailleCase, 0));
        return chemin;
    }
}
