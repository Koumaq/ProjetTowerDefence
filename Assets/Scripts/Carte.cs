using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assets.Scripts
{
    using System.Collections.Generic;
    using UnityEngine;

    public class Carte : MonoBehaviour
    {
        public int largeur; // Nombre de colonnes
        public int hauteur; // Nombre de lignes
        public float tailleCase = 1f; // Taille d'une case en unités Unity

        private bool[,] grille; // True = case bloquée, False = case libre

        void Start()
        {
            // Assure-toi que les dimensions sont définies avant de créer la grille
            if (largeur <= 0 || hauteur <= 0)
            {
                Debug.LogError("La largeur et la hauteur de la carte doivent être définies correctement.");
                return; // Ne continue pas si les dimensions sont invalides
            }

            grille = new bool[largeur, hauteur];
            MarquerPointsDeDepart(); // Marquer les points de départ (optionnel, à personnaliser)
        }


        public bool PlacerTour(int x, int y)
        {
            if (x >= 0 && x < largeur && y >= 0 && y < hauteur && !grille[x, y])
            {
                grille[x, y] = true; // Bloque la case
                return true;
            }
            return false; // Placement invalide
        }

        public List<Vector2> TrouverChemin(Vector2 depart, Vector2 arrivee)
        {
            // Appel de l'algorithme A*
            return AStar(depart, arrivee);
        }
        private List<Vector2> AStar(Vector2 depart, Vector2 arrivee)
        {
            List<Vector2> chemin = new List<Vector2>();
            Vector2 positionActuelle = depart;

            // Convertir les positions en Vector2Int (pour travailler sur la grille)
            Vector2Int departInt = new Vector2Int(Mathf.FloorToInt(depart.x / tailleCase), Mathf.FloorToInt(depart.y / tailleCase));
            Vector2Int arriveeInt = new Vector2Int(Mathf.FloorToInt(arrivee.x / tailleCase), Mathf.FloorToInt(arrivee.y / tailleCase));

            while (departInt != arriveeInt)
            {
                // Calculer la direction entre le point actuel et la destination
                int directionX = arriveeInt.x - departInt.x;
                int directionY = arriveeInt.y - departInt.y;

                // On ne normalise pas, mais on déplace d'une case dans la direction de la destination
                // Déplacement vers la destination en x ou y
                if (Mathf.Abs(directionX) > Mathf.Abs(directionY))
                {
                    departInt.x += directionX > 0 ? 1 : -1; // Se déplace horizontalement
                }
                else
                {
                    departInt.y += directionY > 0 ? 1 : -1; // Se déplace verticalement
                }

                // Ajouter à la liste du chemin avec la taille de la case
                chemin.Add(new Vector2(departInt.x * tailleCase, departInt.y * tailleCase));
            }

            // Ajouter la destination
            chemin.Add(new Vector2(arriveeInt.x * tailleCase, arriveeInt.y * tailleCase));

            return chemin;
        }




        private List<Vector2> GetVoisins(Vector2 position)
        {
            List<Vector2> voisins = new List<Vector2>();

            // Les 4 voisins voisins (haut, bas, gauche, droite)
            Vector2[] directions = { Vector2.up, Vector2.down, Vector2.left, Vector2.right };

            foreach (var dir in directions)
            {
                Vector2 voisin = position + dir * tailleCase;
                voisins.Add(voisin);
            }

            return voisins;
        }


        public Vector2 GetPointDepartAleatoire()
        {
            // Crée une liste de points valides
            List<Vector2> pointsValides = new List<Vector2>();

            for (int x = 0; x < largeur; x++)
            {
                for (int y = 0; y < hauteur; y++)
                {
                    if (!grille[x, y]) // Si la case n'est pas bloquée par une tour
                    {
                        pointsValides.Add(new Vector2(x * tailleCase, y * tailleCase));
                    }
                }
            }

            // Choisir un point aléatoire parmi les points valides
            if (pointsValides.Count > 0)
            {
                int indexAleatoire = UnityEngine.Random.Range(0, pointsValides.Count);
                return pointsValides[indexAleatoire];
            }

            return Vector2.zero; // Si aucun point valide n'est trouvé
        }


        public void MarquerPointsDeDepart()
        {
            for (int x = 0; x < largeur; x++)
            {
                for (int y = 0; y < hauteur; y++)
                {
                    if (!grille[x, y]) // Si la case n'est pas bloquée
                    {
                        Vector3 position = new Vector3(x * tailleCase, y * tailleCase, 0);
                        // Créer un petit objet à cette position, par exemple un cube ou une sphère
                        GameObject point = GameObject.CreatePrimitive(PrimitiveType.Sphere);
                        point.transform.position = position;
                        point.transform.localScale = new Vector3(0.3f, 0.3f, 0.3f); // Taille de l'icône
                        point.GetComponent<Renderer>().material.color = Color.red; // Couleur du point de départ
                    }
                }
            }
        }


        public bool EstAccessible(Vector2 position)
        {
            Vector2Int posInt = new Vector2Int((int)position.x, (int)position.y);
            if (!EstDansLaGrille(posInt))
            {
                return false;
            }

            // Vérifie si la case est bloquée, sauf si c'est la case de la base
            if (grille[posInt.x, posInt.y] && posInt != GestionnaireDeJeu.Instance.GetBasePosition())
            {
                return false;
            }

            return true;
        }





        public bool EstDansLaGrille(Vector2Int position)
        {
            return position.x >= 0 && position.x < largeur && position.y >= 0 && position.y < hauteur;
        }

        public Vector2Int ChoisirBaseAleatoire()
        {
            // Recherche une position libre sur la grille
            List<Vector2Int> positionsLibres = new List<Vector2Int>();

            for (int x = 0; x < largeur; x++)
            {
                for (int y = 0; y < hauteur; y++)
                {
                    if (!grille[x, y]) // La case est libre
                    {
                        positionsLibres.Add(new Vector2Int(x, y));
                    }
                }
            }

            if (positionsLibres.Count > 0)
            {
                // Choisir une position aléatoire parmi les cases libres
                int indexAleatoire = Random.Range(0, positionsLibres.Count);
                return positionsLibres[indexAleatoire];
            }
            return Vector2Int.zero; // Si aucune case n'est libre
        }

        public bool EstCaseOccupee(Vector2Int position)
        {
            if (!EstDansLaGrille(position)) return true;

            // Permet aux ennemis d'atteindre la base mais interdit le placement des tours
            if (position == GestionnaireDeJeu.Instance.GetBasePosition())
            {
                return false; // La base est une exception : elle est accessible aux ennemis
            }

            return grille[position.x, position.y];
        }


        public void BloquerCase(Vector2Int position)
        {
            if (EstDansLaGrille(position))
            {
                grille[position.x, position.y] = true; // Marquer la case comme bloquée
            }
        }


    }
}
