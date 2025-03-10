using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assets.Scripts
{
    public class EnnemiResistant : Ennemi
    {
        void Awake()
        {
            vitesse = 1.5f;
            pv = 200;
        }
    }



}
