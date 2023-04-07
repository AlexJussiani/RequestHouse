using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Reflection;
using System.Runtime.ConstrainedExecution;
using System.Text;
using System.Threading.Tasks;

namespace RH.Pedidos.Domain
{
    public class Roteiro
    {
        /* DESENVOLVIMENTO DO DOMINIO DE PEDIDOS */

        /* PEDIDO - ITEM PEDIDO - */

        /*     
        Um item de um pedido representa um produto e pode conter mais de uma unidade
            Independente da ação, um item precisa ser sempre valido:
                Possuir: Id e Nome do produto, quantidade, valor unitário

        Um pedido iniciado deve estar com o status de rascunho
            e deve pertencer a um cliente.

        1 - Adicionar Item                
                1.1 - Ao adicionar um item é necessário calcular o valor total do pedido 
                1.2 - Se um item já está na lista então deve acrescer a quantidade do item no pedido        
        
         - Atualizacao de Item
                2.1 - O item precisa estar na lista para ser atualizado
                2.2 - Um item pode ser atualizado contendo mais ou menos unidades do que anteriormente                
                2.3 - Ao atualizar um item é necessário calcular o valor total do pedido 
                2.4 - Um item deve ser maior ou igual a 1 unidade
                2.5 - Ao atualizar o item é necessário calcular o valor total do pedido

        3 - Remoção de Item
                3.1 - O item precisa estar na lista para ser removido
                3.2 - Ao remover um item é necessário calcular o valor total do pedido 

        */

    }
}
