using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assets.Scripts
{
    public class TourNormale : ITypeTour
    {
        public void Attaquer(Ennemi cible, Tour tour)
        {
            cible.RecevoirDegats(tour.degats);
        }
    }
}
