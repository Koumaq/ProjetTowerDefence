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

        // Par d�faut, la premi�re tour s�lectionn�e est une tour normale
        typeTourSelectionne = new TourNormale();
    }

    // Ces m�thodes sont appel�es lors du clic sur un bouton
    private void ChoisirTourNormale()
    {
        typeTourSelectionne = new TourNormale();
        Debug.Log("Tour normale s�lectionn�e");
    }

    private void ChoisirTourGlace()
    {
        typeTourSelectionne = new TourGlace();
        Debug.Log("Tour de glace s�lectionn�e");
    }

    private void ChoisirTourFeu()
    {
        typeTourSelectionne = new TourFeu();
        Debug.Log("Tour de feu s�lectionn�e");
    }

    public ITypeTour ObtenirTypeTourSelectionne()
    {
        return typeTourSelectionne;
    }
}
