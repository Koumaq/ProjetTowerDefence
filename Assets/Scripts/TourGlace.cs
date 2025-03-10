using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assets.Scripts
{
    public class TourGlace : ITypeTour
    {
        public void Attaquer(Ennemi cible, Tour tour)
        {
            cible.RecevoirDegats(tour.degats);
            cible.Ralentir(0.5f);  // Exemple : Ralentir à 50% la vitesse de l'ennemi
        }
    }

}
