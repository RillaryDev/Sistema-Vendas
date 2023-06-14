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
    public class VendaDAO
    {
        private MySqlConnection connection;

        public VendaDAO()
        {
            this.connection = new ConnectionFactory().getConnection();
        }

        #region CadastrarVenda
        public void cadastrarVenda(Venda obj) 
        {
            try
            {
                string sql = @"INSERT INTO tb_vendas (cliente_id, data_venda, total_venda, obs)
                               VALUES (@cliente_id, @data_venda, @total_venda, @obs)";

                MySqlCommand executaComando = new MySqlCommand(sql, connection);
                executaComando.Parameters.AddWithValue("@cliente_id", obj.cliente_id);
                executaComando.Parameters.AddWithValue("@data_venda", obj.data_venda);
                executaComando.Parameters.AddWithValue("@total_venda", obj.total_venda);
                executaComando.Parameters.AddWithValue("@obs", obj.observacoes);

                connection.Open();
                executaComando.ExecuteNonQuery();

                MessageBox.Show("Venda cadastrado com sucesso!");

                connection.Close();
            }
            catch (Exception error)
            {

                MessageBox.Show("Aconteceu o seguinte erro: " + error);
            }
        }
        #endregion

        #region RetornaIdUltimaVenda
        public int retornaIdUltimaVenda() 
        {
            try
            {
                int idVenda = 0;
                string sql = "SELECT MAX(id) FROM tb_vendas";

                MySqlCommand executaComando = new MySqlCommand(sql, connection);

                connection.Open();

                MySqlDataReader rs = executaComando.ExecuteReader();

                if (rs.Read()) 
                {
                    idVenda = rs.GetInt32("id");
                    
                    
                }
                connection.Close();
                return idVenda;


            }
            catch (Exception error)
            {

                MessageBox.Show("Error ao retornar ID da última venda: " + error);
                connection.Close();
                return 0;
            }
        }
        #endregion
    }
}
