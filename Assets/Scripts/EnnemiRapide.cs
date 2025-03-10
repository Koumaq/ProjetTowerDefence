using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assets.Scripts
{
    public class EnnemiRapide : Ennemi
    {
        void Awake()
        {
            vitesse = 3.5f;
            pv = 75;
        }
    }


}
