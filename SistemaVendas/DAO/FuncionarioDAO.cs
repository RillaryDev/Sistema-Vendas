using MySql.Data.MySqlClient;
using SistemaVendas.Connection;
using SistemaVendas.Model;
using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SistemaVendas.DAO
{
    public class FuncionarioDAO
    {
        private MySqlConnection connection;

        public FuncionarioDAO()
        {
            this.connection = new ConnectionFactory().getConnection();
        }

        #region CadastrarFuncionario
        public void cadastrarFuncionario(Funcionario obj) 
        {
            try
            {
                // 1º - Criar o comando SQL - INSERT INTO na tb_funcionarios
                string sql = @"INSERT INTO tb_funcionarios(nome, rg, cpf, email, senha, cargo, nivel_acesso, telefone, celular, cep, endereco, numero, complemento, bairro, cidade, estado)
                               VALUES(@nome, @rg, @cpf, @email, @senha, @cargo, @nivel_acesso, @telefone, @celular, @cep, @endereco, @numero, @complemento, @bairro, @cidade, @estado)";

                // 2º - Indicar variáveis do comando SQL
                MySqlCommand executaComando = new MySqlCommand(sql, connection);
                executaComando.Parameters.AddWithValue("@nome", obj.nome);
                executaComando.Parameters.AddWithValue("@rg", obj.rg);
                executaComando.Parameters.AddWithValue("@cpf", obj.cpf);
                executaComando.Parameters.AddWithValue("@email", obj.email);
                executaComando.Parameters.AddWithValue("@senha", obj.senha);
                executaComando.Parameters.AddWithValue("@cargo", obj.cargo);
                executaComando.Parameters.AddWithValue("@nivel_acesso", obj.nivel_acesso);
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

                MessageBox.Show("Funcionário cadastrado com sucesso!");

                connection.Close();
            }
            catch (Exception error)
            {

                MessageBox.Show("Aconteceu o seguinte erro: " + error);
            }
        }
        #endregion

        #region ListarFuncionarios
        public DataTable listarFuncionarios()
        {
            try
            {
                // º1 - Criar DataTable e comando SQL   
                DataTable tabelafuncionario = new DataTable();
                string sql = "SELECT * FROM tb_funcionarios";

                // 2º - Abrir conexão com o Banco de Dados
                MySqlCommand executaComando = new MySqlCommand(sql, connection);
                connection.Open();
                executaComando.ExecuteNonQuery();

                // 3º - Preencher os dados no DataTable 
                MySqlDataAdapter dataAdapter = new MySqlDataAdapter(executaComando);
                dataAdapter.Fill(tabelafuncionario);

                connection.Close();
                return tabelafuncionario;
            }
            catch (Exception error)
            {

                MessageBox.Show("Erro ao executar o comando SQL: " + error);
                return null;
            }
        }
        #endregion

        #region AlterarFuncionario
        public void alterarFuncionario(Funcionario obj)
        {
            try
            {
                // 1º - Definição do comando SQL - UPDATE na tabela funcionarios
                string sql = @"UPDATE tb_funcionarios SET nome=@nome, rg=@rg, cpf=@cpf, email=@email, senha=@senha, cargo=@cargo, nivel_acesso=@nivel_acesso, telefone=@telefone, celular=@celular, cep=@cep, 
                               endereco=@endereco, numero=@numero, complemento=@complemento, bairro=@bairro, cidade=@cidade, estado=@estado WHERE id=@id";

                // 2º - Indicar variáveis do comando SQL
                MySqlCommand executaComando = new MySqlCommand(sql, connection);
                executaComando.Parameters.AddWithValue("@nome", obj.nome);
                executaComando.Parameters.AddWithValue("@rg", obj.rg);
                executaComando.Parameters.AddWithValue("@cpf", obj.cpf);
                executaComando.Parameters.AddWithValue("@email", obj.email);
                executaComando.Parameters.AddWithValue("@senha", obj.senha);
                executaComando.Parameters.AddWithValue("@cargo", obj.cargo);
                executaComando.Parameters.AddWithValue("@nivel_acesso", obj.nivel_acesso);
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

                MessageBox.Show("Funcionário alterado com sucesso!");

                connection.Close();
            }
            catch (Exception error)
            {

                MessageBox.Show("Aconteceu o seguinte erro: " + error);
            }

        }
        #endregion

        #region ExcluirFuncionario
        public void excluirFuncionario(Funcionario obj)
        {
            try
            {
                // 1º - Definição do comando SQL - UPDATE na tabela funcionarios
                string sql = @"DELETE FROM tb_funcionarios WHERE id=@id";

                // 2º - Indicar variáveis do comando SQL
                MySqlCommand executaComando = new MySqlCommand(sql, connection);
                executaComando.Parameters.AddWithValue("@id", obj.codigo);

                // 3º - Abrir a conexão e executar o comando SQL
                connection.Open();
                executaComando.ExecuteNonQuery();

                MessageBox.Show("Funcionário excluído com sucesso!");

                connection.Close();
            }
            catch (Exception error)
            {

                MessageBox.Show("Aconteceu o seguinte erro: " + error);
            }

        }
        #endregion

        #region BuscarFuncionarioPorNome
        public DataTable buscarFuncionarioPorNome(string nome)
        {
            try
            {
                // º1 - Criar DataTable e comando SQL   
                DataTable tabelafuncionario = new DataTable();
                string sql = "SELECT * FROM tb_funcionarios WHERE nome=@nome";

                // 2º - Abrir conexão com o Banco de Dados
                MySqlCommand executaComando = new MySqlCommand(sql, connection);
                executaComando.Parameters.AddWithValue("@nome", nome);
                connection.Open();
                executaComando.ExecuteNonQuery();

                // 3º - Preencher os dados no DataTable 
                MySqlDataAdapter dataAdapter = new MySqlDataAdapter(executaComando);
                dataAdapter.Fill(tabelafuncionario);

                connection.Close();
                return tabelafuncionario;
            }
            catch (Exception error)
            {

                MessageBox.Show("Erro ao executar o comando SQL: " + error);
                return null;
            }
        }
        #endregion

        #region ListarFuncionarioPorNome
        public DataTable listarFuncionariosPorNome(string nome)
        {
            try
            {
                // º1 - Criar DataTable e comando SQL   
                DataTable tabelafuncionario = new DataTable();
                string sql = "SELECT * FROM tb_funcionarios WHERE nome LIKE @nome";

                // 2º - Abrir conexão com o Banco de Dados
                MySqlCommand executaComando = new MySqlCommand(sql, connection);
                executaComando.Parameters.AddWithValue("@nome", nome);
                connection.Open();
                executaComando.ExecuteNonQuery();

                // 3º - Preencher os dados no DataTable 
                MySqlDataAdapter dataAdapter = new MySqlDataAdapter(executaComando);
                dataAdapter.Fill(tabelafuncionario);

                connection.Close();
                return tabelafuncionario;
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
