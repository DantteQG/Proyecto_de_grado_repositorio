using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Sistema.Web.Models.Credinet.Aprobador
{
    public class AprobadorVM
    {
        public string idc { get; set; }//string IDC del usuario(firmante) autorizador. Ejmplo: IDC-TIPO DOCUMENTO-EXTENSION-COMPLEMENTO
        public int type { get; set; } //int Tipo de firmante: 1,2,...
    }
}
