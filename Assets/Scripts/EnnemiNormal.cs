using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assets.Scripts
{
    public class EnnemiNormal : Ennemi
    {
        void Awake()
        {
            vitesse = 1;
            pv = 100;
        }
    }

}
