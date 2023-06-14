using SistemaVendas.DAO;
using SistemaVendas.Model;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SistemaVendas.Views
{
    public partial class FrmClientes : Form
    {
        public FrmClientes()
        {
            InitializeComponent();
        }


        # region Botão Salvar
        private void btn_salvar_Click(object sender, EventArgs e)
        {
            // 1º - Configurar para receber dados dentro do objeto modelo de cliente
            Cliente obj = new Cliente();

            obj.nome = txt_nome.Text;
            obj.rg = txt_rg.Text;
            obj.cpf = txt_cpf.Text;
            obj.email = txt_email.Text;
            obj.telefone = txt_telefone.Text;
            obj.celular = txt_celular.Text;
            obj.cep = txt_cep.Text;
            obj.endereco = txt_endereco.Text;
            obj.numero = int.Parse(txt_numero.Text);
            obj.complemento = txt_complemento.Text;
            obj.bairro = txt_bairro.Text;
            obj.cidade = txt_cidade.Text;
            obj.estado = cb_uf.Text;

            // 2º - Criar um objeto da classe ClienteDAO e chamar o método cadastrarCliente()
            ClienteDAO dao = new ClienteDAO();
            dao.cadastrarCliente(obj);

            tabela_cliente.DataSource = dao.listarClientes();
        }
        #endregion

        private void FrmClientes_Load(object sender, EventArgs e)
        {
            ClienteDAO dao = new ClienteDAO();
            tabela_cliente.DataSource = dao.listarClientes(); 
        }

        private void tabela_cliente_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            // 1º - Capturar os dados da linha selecionada
            txt_codigo.Text = tabela_cliente.CurrentRow.Cells[0].Value.ToString();
            txt_nome.Text = tabela_cliente.CurrentRow.Cells[1].Value.ToString();
            txt_rg.Text = tabela_cliente.CurrentRow.Cells[2].Value.ToString();
            txt_cpf.Text = tabela_cliente.CurrentRow.Cells[3].Value.ToString();
            txt_email.Text = tabela_cliente.CurrentRow.Cells[4].Value.ToString();
            txt_telefone.Text = tabela_cliente.CurrentRow.Cells[5].Value.ToString();
            txt_celular.Text = tabela_cliente.CurrentRow.Cells[6].Value.ToString();
            txt_cep.Text = tabela_cliente.CurrentRow.Cells[7].Value.ToString();
            txt_endereco.Text = tabela_cliente.CurrentRow.Cells[8].Value.ToString();
            txt_numero.Text = tabela_cliente.CurrentRow.Cells[9].Value.ToString();
            txt_complemento.Text = tabela_cliente.CurrentRow.Cells[10].Value.ToString();
            txt_bairro.Text = tabela_cliente.CurrentRow.Cells[11].Value.ToString();
            txt_cidade.Text = tabela_cliente.CurrentRow.Cells[12].Value.ToString();
            cb_uf.Text = tabela_cliente.CurrentRow.Cells[13].Value.ToString();

            // 2º - Alterar a guia Dados Pessoais
            tab_clientes.SelectedTab = tabPage1;
        }

        private void btn_excluir_Click(object sender, EventArgs e)
        {
            Cliente obj = new Cliente();
            obj.codigo = int.Parse(txt_codigo.Text);

            ClienteDAO dao = new ClienteDAO();
            dao.excluirCliente(obj);

            // Atualiza o DataGridaView
            tabela_cliente.DataSource = dao.listarClientes();

        }

        private void btn_editar_Click(object sender, EventArgs e)
        {
            // 1º - Configurar para receber dados dentro do objeto modelo de cliente
            Cliente obj = new Cliente();

            obj.nome = txt_nome.Text;
            obj.rg = txt_rg.Text;
            obj.cpf = txt_cpf.Text;
            obj.email = txt_email.Text;
            obj.telefone = txt_telefone.Text;
            obj.celular = txt_celular.Text;
            obj.cep = txt_cep.Text;
            obj.endereco = txt_endereco.Text;
            obj.numero = int.Parse(txt_numero.Text);
            obj.complemento = txt_complemento.Text;
            obj.bairro = txt_bairro.Text;
            obj.cidade = txt_cidade.Text;
            obj.estado = cb_uf.Text;
            obj.codigo = int.Parse(txt_codigo.Text);

            // 2º - Criar um objeto da classe ClienteDAO e chamar o método alterarCliente()
            ClienteDAO dao = new ClienteDAO();
            dao.alterarCliente(obj);

            // 3º - Regarregar o DataGridView
            tabela_cliente.DataSource = dao.listarClientes();
        }

        private void btn_pesquisar_Click(object sender, EventArgs e)
        {
            string nome = txt_pesquisa.Text;

            ClienteDAO dao = new ClienteDAO();

            tabela_cliente.DataSource = dao.buscarClientePorNome(nome);

            if (tabela_cliente.Rows.Count == 0) 
            {
                // Regarregar o DataGridView
                tabela_cliente.DataSource = dao.listarClientes();
            }
        }

        private void txt_pesquisa_KeyPress(object sender, KeyPressEventArgs e)
        {
            string nome = "%" + txt_pesquisa.Text + "%";
            ClienteDAO dao = new ClienteDAO();

            tabela_cliente.DataSource = dao.listarClientesPorNome(nome);

        }

        private void btn_buscarcep_Click(object sender, EventArgs e)
        {
            try
            {
                string cep = txt_cep.Text;
                string xml = "https://viacep.com.br/ws/"+cep+"/xml/";

                DataSet dados = new DataSet();

                dados.ReadXml(xml);

                txt_endereco.Text = dados.Tables[0].Rows[0]["logradouro"].ToString();
                txt_bairro.Text = dados.Tables[0].Rows[0]["bairro"].ToString();
                txt_cidade.Text = dados.Tables[0].Rows[0]["localidade"].ToString();
                txt_complemento.Text = dados.Tables[0].Rows[0]["complemento"].ToString();
                cb_uf.Text = dados.Tables[0].Rows[0]["uf"].ToString();

            }
            catch (Exception)
            {

                MessageBox.Show("Endereço não encontrado, por favor insira os dados manualmente.");
            }
        }

        private void btn_novo_Click(object sender, EventArgs e)
        {
            new Helpers().LimparTela(this);
        }

        private void label1_Click(object sender, EventArgs e)
        {

        }
    }
}
