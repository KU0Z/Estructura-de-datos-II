using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lab_no._1_1085716
{
    class Cancion
    {
        public string nombre { get; set; }

        public string ruta { get; set; }
        public string Artista { get; set; }
        public string Album { get; set; }
        public string Genero { get; set; }
        public int año { get; set; }


    }
    class MyComparer : IComparer<Cancion>

    {

        #region IComparer Members

        public int Compare(Cancion x, Cancion y)

        {

            return (new System.Collections.CaseInsensitiveComparer().Compare(x.nombre, y.nombre));

        }

        #endregion

    }
}



 
