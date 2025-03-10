using System.Collections;
using UnityEngine;

namespace Assets.Scripts
{
    public class TourFeu : ITypeTour
    {
        public void Attaquer(Ennemi cible, Tour tour)
        {
            cible.RecevoirDegats(tour.degats);
            cible.InfligerBrulure(10, 2f);  // Exemple : 5 dégâts toutes les 2 secondes
        }
    }
}