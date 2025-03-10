using Assets.Scripts;
using UnityEngine;
using UnityEngine.UI;

public class SelectionTour : MonoBehaviour
{
    public Button buttonTourNormale;
    public Button buttonTourGlace;
    public Button buttonTourFeu;

    private ITypeTour typeTourSelectionne;

    private void Start()
    {
        // Ajouter les actions aux boutons
        buttonTourNormale.onClick.AddListener(ChoisirTourNormale);
        buttonTourGlace.onClick.AddListener(ChoisirTourGlace);
        buttonTourFeu.onClick.AddListener(ChoisirTourFeu);

        // Par défaut, la première tour sélectionnée est une tour normale
        typeTourSelectionne = new TourNormale();
    }

    // Ces méthodes sont appelées lors du clic sur un bouton
    private void ChoisirTourNormale()
    {
        typeTourSelectionne = new TourNormale();
        Debug.Log("Tour normale sélectionnée");
    }

    private void ChoisirTourGlace()
    {
        typeTourSelectionne = new TourGlace();
        Debug.Log("Tour de glace sélectionnée");
    }

    private void ChoisirTourFeu()
    {
        typeTourSelectionne = new TourFeu();
        Debug.Log("Tour de feu sélectionnée");
    }

    public ITypeTour ObtenirTypeTourSelectionne()
    {
        return typeTourSelectionne;
    }
}
