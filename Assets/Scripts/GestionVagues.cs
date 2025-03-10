using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using Assets.Scripts;

public class GestionVagues : MonoBehaviour
{
    public Carte carte;
    public float tempsEntreVagues = 5f;
    private bool vagueEnCours;
    private int numeroVague = 1;
    public GestionnaireDeJeu GestionnaireDeJeu;


    void Start()
    {
        vagueEnCours = false;
        if (carte == null)
        {
            carte = FindObjectOfType<Carte>();
        }
    }

    void Update()
    {
        if (!vagueEnCours)
        {
            StartCoroutine(GererVague());
        }
    }

    private IEnumerator GererVague()
    {
        vagueEnCours = true;
        int nombreEnnemis = 5 + numeroVague; // Plus d'ennemis à chaque vague

        for (int i = 0; i < nombreEnnemis; i++)
        {
            // Définir un point de départ unique pour chaque ennemi
            Vector2Int basePosition = GestionnaireDeJeu.basePosition;

            Vector2 pointDepart = carte.GetPointDepartAleatoire();
            pointDepart.x = Mathf.Clamp(pointDepart.x, 0, carte.largeur * carte.tailleCase);
            pointDepart.y = Mathf.Clamp(pointDepart.y, 0, carte.hauteur * carte.tailleCase);

            // Trouver le chemin depuis le point de départ vers la base
            List<Vector2> chemin2D = carte.TrouverChemin(pointDepart, new Vector2(basePosition.x * carte.tailleCase, basePosition.y * carte.tailleCase));

            // Vérifier que le chemin est valide (qu'il contient des points)
            if (chemin2D.Count == 0)
            {
                Debug.LogError("Aucun chemin trouvé entre le départ et la base !");
                yield break;
            }

            // Convertir le chemin 2D en chemin 3D
            List<Vector3> chemin3D = chemin2D.ConvertAll(v2 => new Vector3(v2.x, v2.y, 0f));

            string typeEnnemi = ChoisirTypeEnnemi();
            GameObject ennemiObj = EnnemiFactory.CreerEnnemi(typeEnnemi, pointDepart);

            if (ennemiObj != null)
            {
                Ennemi ennemiScript = ennemiObj.GetComponent<Ennemi>();
                if (ennemiScript != null)
                {
                    ennemiScript.Initialiser(chemin3D, carte); // Chaque ennemi suit un chemin individuel
                }
            }

            yield return new WaitForSeconds(1f); // Attendre avant de créer le prochain ennemi
        }

        numeroVague++;
        vagueEnCours = false;
    }





    private string ChoisirTypeEnnemi()
    {
        int rand = Random.Range(0, 100);
        if (rand < 50) return "normal"; // 50% des ennemis sont normaux
        if (rand < 80) return "rapide"; // 30% des ennemis sont rapides
        return "resistant"; // 20% des ennemis sont résistants
    }
}
