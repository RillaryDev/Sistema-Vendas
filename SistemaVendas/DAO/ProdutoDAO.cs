using MySql.Data.MySqlClient;
using SistemaVendas.Connection;
using SistemaVendas.Model;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SistemaVendas.DAO
{
    public class ProdutoDAO
    {
        private MySqlConnection connection;

        public ProdutoDAO()
        {
            this.connection = new ConnectionFactory().getConnection();
        }

        #region CadastrarProduto
        public void cadastrarProduto(Produto obj)
        {
            try
            {
                // 1º - Definição do comando SQL - INSERT INTO na tabela produtos
                string sql = @"INSERT INTO tb_produtos(descricao, preco, qtd_estoque, for_id) 
                               VALUES(@descricao, @preco, @qtd_estoque, @for_id)";

                // 2º - Indicar variáveis do comando SQL
                MySqlCommand executaComando = new MySqlCommand(sql, connection);
                executaComando.Parameters.AddWithValue("@descricao", obj.descricao);
                executaComando.Parameters.AddWithValue("@preco", obj.preco);
                executaComando.Parameters.AddWithValue("@qtd_estoque", obj.quantidade_em_estoque);
                executaComando.Parameters.AddWithValue("@for_id", obj.for_id);


                // 3º - Abrir a conexão e executar o comando SQL
                connection.Open();
                executaComando.ExecuteNonQuery();

                MessageBox.Show("Produto cadastrado com sucesso!");

                connection.Close();
            }
            catch (Exception error)
            {

                MessageBox.Show("Aconteceu o seguinte erro: " + error);
            }

        }
        #endregion

        #region ListarProdutos
        public DataTable listarProdutos()
        {
            try
            {
                // º1 - Criar DataTable e comando SQL   
                DataTable tabelaproduto = new DataTable();
                string sql = @"SELECT p.id AS 'Código',
                                      p.descricao AS 'Descrição',
                                      p.preco AS 'Preço',
                                      p.qtd_estoque AS 'Quantidade em Estoque',
                                      f.nome AS 'Fornecedor' FROM tb_produtos AS p
                                      JOIN tb_fornecedores AS f ON (p.for_id = f.id)";

                // 2º - Abrir conexão com o Banco de Dados
                MySqlCommand executaComando = new MySqlCommand(sql, connection);
                connection.Open();
                executaComando.ExecuteNonQuery();

                // 3º - Preencher os dados no DataTable 
                MySqlDataAdapter dataAdapter = new MySqlDataAdapter(executaComando);
                dataAdapter.Fill(tabelaproduto);

                connection.Close();
                return tabelaproduto;
            }
            catch (Exception error)
            {

                MessageBox.Show("Erro ao executar o comando SQL: " + error);
                return null;
            }
        }
        #endregion

        #region AlterarProduto
        public void alterarProduto(Produto obj)
        {
            try
            {
                // 1º - Definição do comando SQL - UPDATE na tabela produtos
                string sql = @"UPDATE tb_produtos SET descricao=@descricao, preco=@preco, qtd_estoque=@qtd_estoque, fornecedor=@for_id
                               WHERE id=@id";

                // 2º - Indicar variáveis do comando SQL
                MySqlCommand executaComando = new MySqlCommand(sql, connection);
                executaComando.Parameters.AddWithValue("@descricao", obj.descricao);
                executaComando.Parameters.AddWithValue("@preco", obj.preco);
                executaComando.Parameters.AddWithValue("@qtd_estoque", obj.quantidade_em_estoque);
                executaComando.Parameters.AddWithValue("@fornecedor", obj.for_id);

                // 3º - Abrir a conexão e executar o comando SQL
                connection.Open();
                executaComando.ExecuteNonQuery();

                MessageBox.Show("Produto alterado com sucesso!");

                connection.Close();
            }
            catch (Exception error)
            {

                MessageBox.Show("Aconteceu o seguinte erro: " + error);
            }

        }
        #endregion

        #region ExcluirProduto
        public void excluirProduto(Produto obj)
        {
            try
            {
                // 1º - Definição do comando SQL - DELETE na tabela produtos
                string sql = @"DELETE FROM tb_produtos WHERE id=@id";

                // 2º - Indicar variáveis do comando SQL
                MySqlCommand executaComando = new MySqlCommand(sql, connection);
                executaComando.Parameters.AddWithValue("@id", obj.id);

                // 3º - Abrir a conexão e executar o comando SQL
                connection.Open();
                executaComando.ExecuteNonQuery();

                MessageBox.Show("Produto excluído com sucesso!");

                connection.Close();
            }
            catch (Exception error)
            {

                MessageBox.Show("Aconteceu o seguinte erro: " + error);
            }
        }
        #endregion

        #region BuscarProdutoPorNome
        public DataTable buscarProdutoPorNome(string nome)
        {
            try
            {
                // º1 - Criar DataTable e comando SQL   
                DataTable tabelaproduto = new DataTable();
                string sql = @"SELECT p.id AS 'Código',
                                      p.descricao AS 'Descrição',
                                      p.preco AS 'Preço',
                                      p.qtd_estoque AS 'Quantidade em Estoque',
                                      f.nome AS 'Fornecedor' FROM tb_produtos AS p
                                      JOIN tb_fornecedores AS f ON (p.for_id = f.id) WHERE p.descricao=@nome";

                // 2º - Abrir conexão com o Banco de Dados
                MySqlCommand executaComando = new MySqlCommand(sql, connection);
                executaComando.Parameters.AddWithValue("@nome", nome);

                connection.Open();
                executaComando.ExecuteNonQuery();

                // 3º - Preencher os dados no DataTable 
                MySqlDataAdapter dataAdapter = new MySqlDataAdapter(executaComando);
                dataAdapter.Fill(tabelaproduto);

                connection.Close();
                return tabelaproduto;
            }
            catch (Exception error)
            {

                MessageBox.Show("Erro ao executar o comando SQL: " + error);
                return null;
            }
            
        }
        #endregion

        #region ListarProdutosPorNome
        public DataTable listarProdutosPorNome(string nome)
        {
            try
            {
                // º1 - Criar DataTable e comando SQL   
                DataTable tabelaproduto = new DataTable();
                string sql = @"SELECT p.id AS 'Código',
                                      p.descricao AS 'Descrição',
                                      p.preco AS 'Preço',
                                      p.qtd_estoque AS 'Quantidade em Estoque',
                                      f.nome AS 'Fornecedor' FROM tb_produtos AS p
                                      JOIN tb_fornecedores AS f ON (p.for_id = f.id) WHERE p.descricao like @nome";

                // 2º - Abrir conexão com o Banco de Dados
                MySqlCommand executaComando = new MySqlCommand(sql, connection);
                executaComando.Parameters.AddWithValue("@nome", nome);

                connection.Open();
                executaComando.ExecuteNonQuery();

                // 3º - Preencher os dados no DataTable 
                MySqlDataAdapter dataAdapter = new MySqlDataAdapter(executaComando);
                dataAdapter.Fill(tabelaproduto);

                connection.Close();
                return tabelaproduto;
            }
            catch (Exception error)
            {

                MessageBox.Show("Erro ao executar o comando SQL: " + error);
                return null;
            }
        }

        #endregion

        #region retornaProdutoPorCodigo
        public Produto retornaProdutoPorCodigo(int id)
        {
            try
            {
                // º1 - Criar o comando SQL   
                string sql = @"SELECT * FROM tb_produtos WHERE id=@id";

                // 2º - Abrir conexão com o Banco de Dados
                MySqlCommand executaComando = new MySqlCommand(sql, connection);
                executaComando.Parameters.AddWithValue("@id", id);
                connection.Open();

                // 3º - Criar o MySqlDataReader
                MySqlDataReader rs = executaComando.ExecuteReader();

                if (rs.Read())
                {
                    Produto p = new Produto();

                    p.id = rs.GetInt32("id");
                    p.descricao = rs.GetString("descricao");
                    p.preco = rs.GetDecimal("preco");

                    connection.Close();

                    return p;
                }
                else
                {
                    MessageBox.Show("Produto não encontrado!");
                    connection.Close();
                    return null;
                }

            }
            catch (Exception error)
            {

                MessageBox.Show("Aconteceu o seguinte erro: " + error);
                return null;
            }

        }
        #endregion

        #region BaixaEstoque
        public void baixaEstoque(int idproduto, int qtdestoque) 
        {
            try
            {
                string sql = @"UPDATE tb_produtos SET qtd_estoque=@qtd_estoque
                               WHERE id=@id";

                MySqlCommand executaComando = new MySqlCommand(sql, connection);
                executaComando.Parameters.AddWithValue("@qtd_estoque", qtdestoque);
                executaComando.Parameters.AddWithValue("@id", idproduto);

                connection.Open();
                executaComando.ExecuteNonQuery();

                connection.Close();
            }
            catch (Exception error)
            {

                MessageBox.Show("Aconteceu o seguinte erro: " + error);
                connection.Close();
            }
        }
        #endregion

        #region RetornaEstoqueAtual
        public int retornaEstoqueAtual(int idproduto) 
        {
            try
            {
                string sql = @"SELECT qtd_estoque FROM tb_produtos WHERE id = @id";
                int qtd_estoque = 0;

                MySqlCommand executaComando = new MySqlCommand(sql, connection);
                executaComando.Parameters.AddWithValue("@id", idproduto);

                connection.Open();

                MySqlDataReader rs = executaComando.ExecuteReader();

                if (rs.Read()) 
                {
                    qtd_estoque = rs.GetInt32("qtd_estoque");
                    connection.Close();
                }

                return qtd_estoque;
            }
            catch (Exception error)
            {

                MessageBox.Show("Aconteceu o seguinte erro: " + error);
                connection.Close();
                return 0;
            }
        }
        #endregion
    }
}
