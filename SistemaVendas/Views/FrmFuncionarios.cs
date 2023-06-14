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
    public partial class FrmFuncionarios : Form
    {
        public FrmFuncionarios()
        {
            InitializeComponent();
        }

        private void btn_salvar_Click(object sender, EventArgs e)
        {
            Funcionario obj = new Funcionario();

            obj.nome = txt_nome.Text;
            obj.rg = txt_rg.Text;
            obj.cpf = txt_cpf.Text; 
            obj.email = txt_email.Text;  
            obj.senha = txt_senha.Text;
            obj.cargo = cb_cargo.SelectedItem.ToString();
            obj.nivel_acesso = cb_nivel.SelectedItem.ToString();
            obj.telefone = txt_telefone.Text;
            obj.celular = txt_celular.Text;
            obj.cep = txt_cep.Text;
            obj.endereco = txt_endereco.Text;
            obj.numero = int.Parse(txt_numero.Text);
            obj.complemento= txt_complemento.Text;  
            obj.bairro = txt_bairro.Text;
            obj.cidade = txt_cidade.Text;
            obj.estado = cb_uf.SelectedItem.ToString();

            FuncionarioDAO dao = new FuncionarioDAO();
            dao.cadastrarFuncionario(obj);

            tabela_funcionarios.DataSource = dao.listarFuncionarios();

        }

        private void FrmFuncionarios_Load(object sender, EventArgs e)
        {
            FuncionarioDAO dao = new FuncionarioDAO();
            tabela_funcionarios.DataSource = dao.listarFuncionarios();
        }

        private void tabela_funcionarios_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            txt_codigo.Text = tabela_funcionarios.CurrentRow.Cells[0].Value.ToString();
            txt_nome.Text = tabela_funcionarios.CurrentRow.Cells[1].Value.ToString();
            txt_rg.Text = tabela_funcionarios.CurrentRow.Cells[2].Value.ToString();
            txt_cpf.Text = tabela_funcionarios.CurrentRow.Cells[3].Value.ToString();
            txt_email.Text = tabela_funcionarios.CurrentRow.Cells[4].Value.ToString();
            txt_senha.Text = tabela_funcionarios.CurrentRow.Cells[5].Value.ToString();
            cb_cargo.Text = tabela_funcionarios.CurrentRow.Cells[6].Value.ToString();
            cb_nivel.Text = tabela_funcionarios.CurrentRow.Cells[7].Value.ToString();
            txt_telefone.Text = tabela_funcionarios.CurrentRow.Cells[8].Value.ToString();
            txt_celular.Text = tabela_funcionarios.CurrentRow.Cells[9].Value.ToString();
            txt_cep.Text = tabela_funcionarios.CurrentRow.Cells[10].Value.ToString();
            txt_endereco.Text = tabela_funcionarios.CurrentRow.Cells[11].Value.ToString();
            txt_numero.Text = tabela_funcionarios.CurrentRow.Cells[12].Value.ToString();
            txt_complemento.Text = tabela_funcionarios.CurrentRow.Cells[13].Value.ToString();
            txt_bairro.Text = tabela_funcionarios.CurrentRow.Cells[14].Value.ToString();
            txt_cidade.Text = tabela_funcionarios.CurrentRow.Cells[15].Value.ToString();
            cb_uf.Text = tabela_funcionarios.CurrentRow.Cells[16].Value.ToString();

            tab_funcionarios.SelectedTab = tabPage1;
        }

        private void btn_excluir_Click(object sender, EventArgs e)
        {
            Funcionario obj = new Funcionario();
            obj.codigo = int.Parse(txt_codigo.Text);

            FuncionarioDAO dao = new FuncionarioDAO();
            dao.excluirFuncionario(obj);

            tabela_funcionarios.DataSource = dao.listarFuncionarios();
        }

        private void btn_editar_Click(object sender, EventArgs e)
        {
            Funcionario obj = new Funcionario();

            obj.nome = txt_nome.Text;
            obj.rg = txt_rg.Text;
            obj.cpf = txt_cpf.Text;
            obj.email = txt_email.Text;
            obj.senha = txt_senha.Text;
            obj.cargo = cb_cargo.SelectedItem.ToString();
            obj.nivel_acesso = cb_nivel.SelectedItem.ToString();
            obj.telefone = txt_telefone.Text;
            obj.celular = txt_celular.Text;
            obj.cep = txt_cep.Text;
            obj.endereco = txt_endereco.Text;
            obj.numero = int.Parse(txt_numero.Text);
            obj.complemento = txt_complemento.Text;
            obj.bairro = txt_bairro.Text;
            obj.cidade = txt_cidade.Text;
            obj.estado = cb_uf.SelectedItem.ToString();
            obj.codigo = int.Parse(txt_codigo.Text);

            FuncionarioDAO dao = new FuncionarioDAO();
            dao.alterarFuncionario(obj);

            tabela_funcionarios.DataSource = dao.listarFuncionarios();
        }

        private void btn_pesquisar_Click(object sender, EventArgs e)
        {
            string nome = txt_pesquisa.Text;

            FuncionarioDAO dao = new FuncionarioDAO();
            tabela_funcionarios.DataSource = dao.buscarFuncionarioPorNome(nome);

            if (tabela_funcionarios.Rows.Count == 0 || txt_pesquisa.Text == string.Empty) 
            {
                MessageBox.Show("Funcionário não encontrado");
                tabela_funcionarios.DataSource = dao.listarFuncionarios();
            }
        }

        private void txt_pesquisa_TextChanged(object sender, EventArgs e)
        {
            string nome = "%" + txt_pesquisa.Text + "%";

            FuncionarioDAO dao = new FuncionarioDAO();

            tabela_funcionarios.DataSource = dao.listarFuncionariosPorNome(nome);

        }

        private void btn_novo_Click(object sender, EventArgs e)
        {
            new Helpers().LimparTela(this);
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
    }
}
