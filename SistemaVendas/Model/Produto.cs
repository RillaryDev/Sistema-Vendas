using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SistemaVendas.Model
{
    public class Produto
    {
        public int id { get; set; }
        public string descricao { get; set; }
        public decimal preco  { get; set; }
        public int  quantidade_em_estoque { get; set; }
        public int for_id { get; set; }
    }
}
