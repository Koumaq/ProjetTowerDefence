using Assets.Scripts;
using UnityEngine;

public class CarteVisuelle : MonoBehaviour
{
    public GameObject casePrefab;
    public Carte carte;

    private void Start()
    {
        for (int x = 0; x < carte.largeur; x++)
        {
            for (int y = 0; y < carte.hauteur; y++)
            {
                Vector3 position = new Vector3(x * carte.tailleCase, y * carte.tailleCase, 0);
                Instantiate(casePrefab, position, Quaternion.identity, transform);
            }
        }

        // Centrer la caméra sur la grille
        CentrerCamera();
    }

    private void CentrerCamera()
    {
        float centreX = (carte.largeur * carte.tailleCase) / 2f - carte.tailleCase / 2f;
        float centreY = (carte.hauteur * carte.tailleCase) / 2f - carte.tailleCase / 2f;

        Camera.main.transform.position = new Vector3(centreX, centreY, Camera.main.transform.position.z);
    }
}
