using Assets.Scripts;
using UnityEngine;

public class PlacementTour : MonoBehaviour
{
    public Carte carte;
    public GameObject prefabTourNormale;
    public GameObject prefabTourGlace;
    public GameObject prefabTourFeu;
    public int coutTour = 50;
    private GestionnaireDeJeu gestionnaireDeJeu;

    private SelectionTour selectionTour;

    private void Start()
    {
        gestionnaireDeJeu = GestionnaireDeJeu.Instance;
        selectionTour = FindObjectOfType<SelectionTour>();  // Trouver le script SelectionTour
    }

    private void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            Vector3 positionSouris = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            int x = Mathf.FloorToInt(positionSouris.x / carte.tailleCase);
            int y = Mathf.FloorToInt(positionSouris.y / carte.tailleCase);

            // Choisir le type de tour à placer en fonction de ce qui est sélectionné
            if (gestionnaireDeJeu.Monnie >= coutTour && carte.PlacerTour(x, y))
            {
                GameObject tour = null;
                ITypeTour typeTour = selectionTour.ObtenirTypeTourSelectionne();  // Obtenir le type sélectionné

                // Créer la tour correspondante
                if (typeTour is TourNormale)
                {
                    tour = Instantiate(prefabTourNormale, new Vector3(x * carte.tailleCase, y * carte.tailleCase, -1), Quaternion.identity);
                }
                else if (typeTour is TourGlace)
                {
                    tour = Instantiate(prefabTourGlace, new Vector3(x * carte.tailleCase, y * carte.tailleCase, -1), Quaternion.identity);
                }
                else if (typeTour is TourFeu)
                {
                    tour = Instantiate(prefabTourFeu, new Vector3(x * carte.tailleCase, y * carte.tailleCase, -1), Quaternion.identity);
                }

                // Initialiser la tour avec le comportement sélectionné
                tour.GetComponent<Tour>().Initialiser(typeTour);

                // Déduire le coût de la monnaie
                gestionnaireDeJeu.Monnie -= coutTour;
                UIManager.Instance.MettreAJourMonnaie(gestionnaireDeJeu.Monnie);
            }
        }
    }
}
