using MySql.Data.MySqlClient;
using SistemaVendas.Connection;
using SistemaVendas.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SistemaVendas.DAO
{
    public class ItemVendaDAO
    {
        private MySqlConnection connection;

        public ItemVendaDAO()
        {
            this.connection = new ConnectionFactory().getConnection();
        }

        #region CadastrarItemVenda

        public void cadastrarItemVenda(ItemVenda obj) 
        {
            try
            {
                string sql = @"INSERT INTO tb_itensvendas(venda_id, produto_id, qtd, subtotal)
                               VALUES (@venda_id, @produto_id, @qtd, @subtotal)";

                MySqlCommand executaComando = new MySqlCommand(sql, connection);
                executaComando.Parameters.AddWithValue("@venda_id", obj.venda_id);
                executaComando.Parameters.AddWithValue("@produto_id", obj.produto_id);
                executaComando.Parameters.AddWithValue("@qtd", obj.qtd);
                executaComando.Parameters.AddWithValue("@subtotal", obj.subtotal);

                connection.Open();
                executaComando.ExecuteNonQuery();

                MessageBox.Show("Item cadastrado com sucesso!");

                connection.Close();
            }
            catch (Exception error)
            {

                MessageBox.Show("Aconteceu o seguinte erro: " + error);
            }
        }
        #endregion
    }
}
