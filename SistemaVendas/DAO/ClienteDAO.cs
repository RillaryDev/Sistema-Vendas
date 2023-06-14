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
    public class ClienteDAO
    {
        private MySqlConnection connection;

        public ClienteDAO() 
        {
            this.connection = new ConnectionFactory().getConnection();
        }

        #region CadastrarCliente
        public void cadastrarCliente(Cliente obj) 
        {
            try
            {
                // 1º - Definição do comando SQL - INSERT INTO na tabela clientes
                string sql = @"INSERT INTO tb_clientes(nome, rg, cpf, email, telefone, celular, cep, endereco, numero, complemento, bairro, cidade, estado) 
                               VALUES(@nome, @rg, @cpf, @email, @telefone, @celular, @cep, @endereco, @numero, @complemento, @bairro, @cidade, @estado)";

                // 2º - Indicar variáveis do comando SQL
                MySqlCommand executaComando = new MySqlCommand(sql, connection);
                executaComando.Parameters.AddWithValue("@nome", obj.nome);
                executaComando.Parameters.AddWithValue("@rg", obj.rg);
                executaComando.Parameters.AddWithValue("@cpf", obj.cpf);
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

                MessageBox.Show("Cliente cadastrado com sucesso!");

                connection.Close();
            }
            catch (Exception error)
            {

                MessageBox.Show("Aconteceu o seguinte erro: " + error);
            }

        }
        #endregion

        #region ListarClientes
        public DataTable listarClientes() 
        {
            try
            {
                // º1 - Criar DataTable e comando SQL   
                DataTable tabelacliente = new DataTable();
                string sql = "SELECT * FROM tb_clientes";

                // 2º - Abrir conexão com o Banco de Dados
                MySqlCommand executaComando = new MySqlCommand(sql, connection);
                connection.Open();
                executaComando.ExecuteNonQuery();

                // 3º - Preencher os dados no DataTable 
                MySqlDataAdapter dataAdapter= new MySqlDataAdapter(executaComando);
                dataAdapter.Fill(tabelacliente);

                connection.Close();
                return tabelacliente;
            }
            catch (Exception error)
            {

                MessageBox.Show("Erro ao executar o comando SQL: " + error);
                return null;
            }
        }
        #endregion

        #region AlterarCliente
        public void alterarCliente(Cliente obj)
        {
            try
            {
                // 1º - Definição do comando SQL - UPDATE na tabela clientes
                string sql = @"UPDATE tb_clientes SET nome=@nome, rg=@rg, cpf=@cpf, email=@email, telefone=@telefone, celular=@celular, cep=@cep, 
                               endereco=@endereco, numero=@numero, complemento=@complemento, bairro=@bairro, cidade=@cidade, estado=@estado
                               WHERE id=@id";

                // 2º - Indicar variáveis do comando SQL
                MySqlCommand executaComando = new MySqlCommand(sql, connection);
                executaComando.Parameters.AddWithValue("@nome", obj.nome);
                executaComando.Parameters.AddWithValue("@rg", obj.rg);
                executaComando.Parameters.AddWithValue("@cpf", obj.cpf);
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

                MessageBox.Show("Cliente alterado com sucesso!");

                connection.Close();
            }
            catch (Exception error)
            {

                MessageBox.Show("Aconteceu o seguinte erro: " + error);
            }

        }
        #endregion

        #region ExcluirCliente
        public void excluirCliente(Cliente obj)
        {
            try
            {
                // 1º - Definição do comando SQL - DELETE na tabela clientes
                string sql = @"DELETE FROM tb_clientes WHERE id=@id";

                // 2º - Indicar variáveis do comando SQL
                MySqlCommand executaComando = new MySqlCommand(sql, connection);
                executaComando.Parameters.AddWithValue("@id", obj.codigo);

                // 3º - Abrir a conexão e executar o comando SQL
                connection.Open();
                executaComando.ExecuteNonQuery();

                MessageBox.Show("Cliente excluído com sucesso!");

                connection.Close();
            }
            catch (Exception error)
            {

                MessageBox.Show("Aconteceu o seguinte erro: " + error);
            }
        }
        #endregion

        #region BuscarClientePorNome
        public DataTable buscarClientePorNome(string nome)
        {
            try
            {
                // º1 - Criar DataTable e comando SQL   
                DataTable tabelacliente = new DataTable();
                string sql = "SELECT * FROM tb_clientes WHERE nome=@nome";

                // 2º - Abrir conexão com o Banco de Dados
                MySqlCommand executaComando = new MySqlCommand(sql, connection);
                executaComando.Parameters.AddWithValue("@nome", nome);

                connection.Open();
                executaComando.ExecuteNonQuery();

                // 3º - Preencher os dados no DataTable 
                MySqlDataAdapter dataAdapter = new MySqlDataAdapter(executaComando);
                dataAdapter.Fill(tabelacliente);

                connection.Close();
                return tabelacliente;
            }
            catch (Exception error)
            {

                MessageBox.Show("Erro ao executar o comando SQL: " + error);
                return null;
            }
        }

        #endregion

        #region ListarClientesPorNome
        public DataTable listarClientesPorNome(string nome)
        {
            try
            {
                // º1 - Criar DataTable e comando SQL   
                DataTable tabelacliente = new DataTable();
                string sql = "SELECT * FROM tb_clientes WHERE nome like @nome";

                // 2º - Abrir conexão com o Banco de Dados
                MySqlCommand executaComando = new MySqlCommand(sql, connection);
                executaComando.Parameters.AddWithValue("@nome", nome);

                connection.Open();
                executaComando.ExecuteNonQuery();

                // 3º - Preencher os dados no DataTable 
                MySqlDataAdapter dataAdapter = new MySqlDataAdapter(executaComando);
                dataAdapter.Fill(tabelacliente);

                connection.Close();
                return tabelacliente;
            }
            catch (Exception error)
            {

                MessageBox.Show("Erro ao executar o comando SQL: " + error);
                return null;
            }
        }

        #endregion

        #region RetornaClientePorCPF
        public Cliente retornaClientePorCpf(string cpf) 
        {
            try
            {
                // 1º - Definição do comando SQL - INSERT INTO na tabela clientes
                Cliente obj = new Cliente();
                string sql = @"SELECT * FROM tb_clientes where cpf=@cpf";

                // 2º - Abrir conexão com o Banco de Dados
                MySqlCommand executaComando = new MySqlCommand(sql, connection);
                executaComando.Parameters.AddWithValue("@cpf", cpf);

                connection.Open();

                MySqlDataReader rs = executaComando.ExecuteReader();

                if (rs.Read())
                {
                    obj.codigo = rs.GetInt32("id");
                    obj.nome = rs.GetString("nome");

                    return obj;
                }
                else 
                {
                    MessageBox.Show("Cliente não encontrado!");
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

    }
}
