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
    public partial class FrmFornecedores : Form
    {
        public FrmFornecedores()
        {
            InitializeComponent();
        }

        private void btn_buscarcep_Click(object sender, EventArgs e)
        {
            try
            {
                string cep = txt_cep.Text;
                string xml = "https://viacep.com.br/ws/" + cep + "/xml/";

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

        private void btn_salvar_Click(object sender, EventArgs e)
        {
            // 1º - Configurar para receber dados dentro do objeto modelo de cliente
            Fornecedor obj = new Fornecedor();

            obj.nome = txt_nome.Text;
            obj.cnpj = txt_cnpj.Text;
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
            FornecedorDAO dao = new FornecedorDAO();
            dao.cadastrarFornecedor(obj);

            tabela_fornecedores.DataSource = dao.listarFornecedores();
        }

        private void FrmFornecedores_Load(object sender, EventArgs e)
        {
            FornecedorDAO dao = new FornecedorDAO();
            tabela_fornecedores.DataSource = dao.listarFornecedores();

        }

        private void tabela_fornecedores_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            // 1º - Capturar os dados da linha selecionada
            txt_codigo.Text = tabela_fornecedores.CurrentRow.Cells[0].Value.ToString();
            txt_nome.Text = tabela_fornecedores.CurrentRow.Cells[1].Value.ToString();
            txt_cnpj.Text = tabela_fornecedores.CurrentRow.Cells[2].Value.ToString();
            txt_email.Text = tabela_fornecedores.CurrentRow.Cells[3].Value.ToString();
            txt_telefone.Text = tabela_fornecedores.CurrentRow.Cells[4].Value.ToString();
            txt_celular.Text = tabela_fornecedores.CurrentRow.Cells[5].Value.ToString();
            txt_cep.Text = tabela_fornecedores.CurrentRow.Cells[6].Value.ToString();
            txt_endereco.Text = tabela_fornecedores.CurrentRow.Cells[7].Value.ToString();
            txt_numero.Text = tabela_fornecedores.CurrentRow.Cells[8].Value.ToString();
            txt_complemento.Text = tabela_fornecedores.CurrentRow.Cells[9].Value.ToString();
            txt_bairro.Text = tabela_fornecedores.CurrentRow.Cells[10].Value.ToString();
            txt_cidade.Text = tabela_fornecedores.CurrentRow.Cells[11].Value.ToString();
            cb_uf.Text = tabela_fornecedores.CurrentRow.Cells[12].Value.ToString();

            // 2º - Alterar a guia Dados Pessoais
            tab_fornecedores.SelectedTab = tabPage1;
        }

        private void btn_editar_Click(object sender, EventArgs e)
        {
            // 1º - Configurar para receber dados dentro do objeto modelo de fornecedor
            Fornecedor obj = new Fornecedor();

            obj.nome = txt_nome.Text;
            obj.cnpj = txt_cnpj.Text;
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

            // 2º - Criar um objeto da classe ClienteDAO e chamar o método alterarFornecedor()
            FornecedorDAO dao = new FornecedorDAO();
            dao.alterarFornecedor(obj);

            // 3º - Regarregar o DataGridView
            tabela_fornecedores.DataSource = dao.listarFornecedores();
        }

        private void btn_excluir_Click(object sender, EventArgs e)
        {
           Fornecedor obj = new Fornecedor();
            obj.codigo = int.Parse(txt_codigo.Text);

            FornecedorDAO dao = new FornecedorDAO();
            dao.excluirFornecedor(obj);

            // Atualiza o DataGridaView
            tabela_fornecedores.DataSource = dao.listarFornecedores();
        }

        private void btn_pesquisar_Click(object sender, EventArgs e)
        {
            string nome = txt_pesquisa.Text;

            FornecedorDAO dao = new FornecedorDAO();

            tabela_fornecedores.DataSource = dao.buscarFornecedorPorNome(nome);

            if (tabela_fornecedores.Rows.Count == 0)
            {
                MessageBox.Show("Nenhum fornecedor encontrado com esse nome.");
                // Regarregar o DataGridView
                tabela_fornecedores.DataSource = dao.listarFornecedores();
            }
        }

        private void txt_pesquisa_TextChanged(object sender, EventArgs e)
        {
            string nome = "%" + txt_pesquisa.Text + "%";

            FornecedorDAO dao = new FornecedorDAO();

            tabela_fornecedores.DataSource = dao.listarFornecedoresPorNome(nome);
        }
    }
}
