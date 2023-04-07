using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RH.Pedidos.Domain
{
    public enum PedidoStatus
    {
        Rascunho = 1,
        Criado = 2,
        Autorizado = 3,
        Percurso = 4,
        Entregue = 5,
        Cancelado = 6,
    }
}
