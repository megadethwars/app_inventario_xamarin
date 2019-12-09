using System;
using System.Collections.Generic;
using System.Text;



namespace Inventario2
{
    
    public class InventDB
    {
        
        public string ID { get; set; }

        public string Codigo { get; set; }
        
        public string Nombre { get; set; }      

        public string marca { get; set; }

        public string Fecha { get; set; }

        public string modelo { get; set; }

        public string foto { get; set; } 

        public string cantidad { get; set; }

        public string observaciones { get; set; }

        public string lugar { get; set; }
    }
}
