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
    public class FornecedorDAO
    {
        private MySqlConnection connection;

        public FornecedorDAO()
        {
            this.connection = new ConnectionFactory().getConnection();
        }

        #region CadastrarFornecedor
        public void cadastrarFornecedor(Fornecedor obj)
        {
            try
            {
                // 1º - Definição do comando SQL - INSERT INTO na tabela fornecedores
                string sql = @"INSERT INTO tb_fornecedores(nome, cnpj, email, telefone, celular, cep, endereco, numero, complemento, bairro, cidade, estado) 
                               VALUES(@nome, @cnpj, @email, @telefone, @celular, @cep, @endereco, @numero, @complemento, @bairro, @cidade, @estado)";

                // 2º - Indicar variáveis do comando SQL
                MySqlCommand executaComando = new MySqlCommand(sql, connection);
                executaComando.Parameters.AddWithValue("@nome", obj.nome);
                executaComando.Parameters.AddWithValue("@cnpj", obj.rg);
                executaComando.Parameters.AddWithValue("@email", obj.email);
                executaComando.Parameters.AddWithValue("@telefone", obj.telefone);
                executaComando.Parameters.AddWithValue("@celular", obj.celular);
                executaComando.Parameters.AddWithValue("@cep", obj.cep);
                executaComando.Parameters.AddWithValue("@endereco", obj.endereco);
                executaComando.Parameters.AddWithValue("@numero", obj.numero);
                executaComando.Parameters.AddWithValue("@complemento", obj.complemento);
                executaComando.Parameters.AddWithValue("@bairro", obj.bairro);
                executaComando.Parameters.AddWithValue("@cidade", obj.cidade);
                executaComando.Parameters.AddWithValue("@estado", obj.estado);

                // 3º - Abrir a conexão e executar o comando SQL
                connection.Open();
                executaComando.ExecuteNonQuery();

                MessageBox.Show("Fornecedor cadastrado com sucesso!");

                connection.Close();
            }
            catch (Exception error)
            {

                MessageBox.Show("Aconteceu o seguinte erro: " + error);
            }

        }
        #endregion

        #region ListarFornecedores
        public DataTable listarFornecedores()
        {
            try
            {
                // º1 - Criar DataTable e comando SQL   
                DataTable tabelafornecedor = new DataTable();
                string sql = "SELECT * FROM tb_fornecedores";

                // 2º - Abrir conexão com o Banco de Dados
                MySqlCommand executaComando = new MySqlCommand(sql, connection);
                connection.Open();
                executaComando.ExecuteNonQuery();

                // 3º - Preencher os dados no DataTable 
                MySqlDataAdapter dataAdapter = new MySqlDataAdapter(executaComando);
                dataAdapter.Fill(tabelafornecedor);

                connection.Close();
                return tabelafornecedor;
            }
            catch (Exception error)
            {

                MessageBox.Show("Erro ao executar o comando SQL: " + error);
                return null;
            }
        }
        #endregion

        #region AlterarFornecedor
        public void alterarFornecedor(Fornecedor obj)
        {
            try
            {
                // 1º - Definição do comando SQL - UPDATE na tabela fornecedores
                string sql = @"UPDATE tb_fornecedores SET nome=@nome, cnpj=@cnpj, email=@email, telefone=@telefone, celular=@celular, cep=@cep, 
                               endereco=@endereco, numero=@numero, complemento=@complemento, bairro=@bairro, cidade=@cidade, estado=@estado
                               WHERE id=@id";

                // 2º - Indicar variáveis do comando SQL
                MySqlCommand executaComando = new MySqlCommand(sql, connection);
                executaComando.Parameters.AddWithValue("@nome", obj.nome);
                executaComando.Parameters.AddWithValue("@cnpj", obj.cnpj);
                executaComando.Parameters.AddWithValue("@email", obj.email);
                executaComando.Parameters.AddWithValue("@telefone", obj.telefone);
                executaComando.Parameters.AddWithValue("@celular", obj.celular);
                executaComando.Parameters.AddWithValue("@cep", obj.cep);
                executaComando.Parameters.AddWithValue("@endereco", obj.endereco);
                executaComando.Parameters.AddWithValue("@numero", obj.numero);
                executaComando.Parameters.AddWithValue("@complemento", obj.complemento);
                executaComando.Parameters.AddWithValue("@bairro", obj.bairro);
                executaComando.Parameters.AddWithValue("@cidade", obj.cidade);
                executaComando.Parameters.AddWithValue("@estado", obj.estado);
                executaComando.Parameters.AddWithValue("@id", obj.codigo);

                // 3º - Abrir a conexão e executar o comando SQL
                connection.Open();
                executaComando.ExecuteNonQuery();

                MessageBox.Show("Fornecedor alterado com sucesso!");

                connection.Close();
            }
            catch (Exception error)
            {

                MessageBox.Show("Aconteceu o seguinte erro: " + error);
            }

        }
        #endregion

        #region ExcluirFornecedor
        public void excluirFornecedor(Fornecedor obj)
        {
            try
            {
                // 1º - Definição do comando SQL - DELETE na tabela fornecedores
                string sql = @"DELETE FROM tb_fornecedores WHERE id=@id";

                // 2º - Indicar variáveis do comando SQL
                MySqlCommand executaComando = new MySqlCommand(sql, connection);
                executaComando.Parameters.AddWithValue("@id", obj.codigo);

                // 3º - Abrir a conexão e executar o comando SQL
                connection.Open();
                executaComando.ExecuteNonQuery();

                MessageBox.Show("Fornecedor excluído com sucesso!");

                connection.Close();
            }
            catch (Exception error)
            {

                MessageBox.Show("Aconteceu o seguinte erro: " + error);
            }
        }
        #endregion

        #region BuscarFornecedorPorNome
        public DataTable buscarFornecedorPorNome(string nome)
        {
            try
            {
                // º1 - Criar DataTable e comando SQL   
                DataTable tabelafornecedor = new DataTable();
                string sql = "SELECT * FROM tb_fornecedores WHERE nome=@nome";

                // 2º - Abrir conexão com o Banco de Dados
                MySqlCommand executaComando = new MySqlCommand(sql, connection);
                executaComando.Parameters.AddWithValue("@nome", nome);

                connection.Open();
                executaComando.ExecuteNonQuery();

                // 3º - Preencher os dados no DataTable 
                MySqlDataAdapter dataAdapter = new MySqlDataAdapter(executaComando);
                dataAdapter.Fill(tabelafornecedor);

                connection.Close();
                return tabelafornecedor;
            }
            catch (Exception error)
            {

                MessageBox.Show("Erro ao executar o comando SQL: " + error);
                return null;
            }
        }

        #endregion

        #region ListarFornecedoresPorNome
        public DataTable listarFornecedoresPorNome(string nome)
        {
            try
            {
                // º1 - Criar DataTable e comando SQL   
                DataTable tabelafornecedor = new DataTable();
                string sql = "SELECT * FROM tb_fornecedores WHERE nome like @nome";

                // 2º - Abrir conexão com o Banco de Dados
                MySqlCommand executaComando = new MySqlCommand(sql, connection);
                executaComando.Parameters.AddWithValue("@nome", nome);

                connection.Open();
                executaComando.ExecuteNonQuery();

                // 3º - Preencher os dados no DataTable 
                MySqlDataAdapter dataAdapter = new MySqlDataAdapter(executaComando);
                dataAdapter.Fill(tabelafornecedor);

                connection.Close();
                return tabelafornecedor;
            }
            catch (Exception error)
            {

                MessageBox.Show("Erro ao executar o comando SQL: " + error);
                return null;
            }
        }

        #endregion

    }
}
