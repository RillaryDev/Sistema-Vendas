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
    public partial class FrmProdutos : Form
    {
        public FrmProdutos()
        {
            InitializeComponent();
        }

        private void FrmProdutos_Load(object sender, EventArgs e)
        {
            FornecedorDAO fornecedor_dao = new FornecedorDAO();
            cb_fornecedor.DataSource = fornecedor_dao.listarFornecedores();
            cb_fornecedor.DisplayMember = "nome";
            cb_fornecedor.ValueMember = "id";

            ProdutoDAO dao = new ProdutoDAO();
            tabela_produtos.DataSource = dao.listarProdutos();
        }

        private void btn_salvar_Click(object sender, EventArgs e)
        {
            // 1º - Configurar para receber dados dentro do objeto modelo de produto
            Produto obj = new Produto();

            obj.descricao = txt_descricao.Text;
            obj.preco = decimal.Parse(txt_preco.Text);
            obj.quantidade_em_estoque = int.Parse(txt_qtd_estoque.Text);
            obj.for_id = int.Parse(cb_fornecedor.Text);
      

            // 2º - Criar um objeto da classe ProdutoDAO e chamar o método cadastrarProduto()
            ProdutoDAO dao = new ProdutoDAO();
            dao.cadastrarProduto(obj);

            new Helpers().LimparTela(this);

            tabela_produtos.DataSource = dao.listarProdutos();
        }

        private void btn_novo_Click(object sender, EventArgs e)
        {
            new Helpers().LimparTela(this);
        }

        private void tabela_produtos_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            // 1º - Capturar os dados da linha selecionada
            txt_codigo.Text = tabela_produtos.CurrentRow.Cells[0].Value.ToString();
            txt_descricao.Text = tabela_produtos.CurrentRow.Cells[1].Value.ToString();
            txt_preco.Text = tabela_produtos.CurrentRow.Cells[2].Value.ToString();
            txt_qtd_estoque.Text = tabela_produtos.CurrentRow.Cells[3].Value.ToString();
            cb_fornecedor.Text = tabela_produtos.CurrentRow.Cells[4].Value.ToString();

            // 2º - Alterar a guia Dados Pessoais
            tab_produtos.SelectedTab = tabPage1;
        }

        private void btn_editar_Click(object sender, EventArgs e)
        {
            // 1º - Configurar para receber dados dentro do objeto modelo de produto
            Produto obj = new Produto();

            obj.descricao = txt_descricao.Text;
            obj.preco = decimal.Parse(txt_preco.Text);
            obj.quantidade_em_estoque = int.Parse(txt_qtd_estoque.Text);
            obj.for_id = int.Parse(cb_fornecedor.Text);
       
            // 2º - Criar um objeto da classe ProdutoDAO e chamar o método alterarProduto()
            ProdutoDAO dao = new ProdutoDAO();
            dao.alterarProduto(obj);

            new Helpers().LimparTela(this);

            // 3º - Regarregar o DataGridView
            tabela_produtos.DataSource = dao.listarProdutos();
        }

        private void btn_excluir_Click(object sender, EventArgs e)
        {
            Produto obj = new Produto();
            obj.id = int.Parse(txt_codigo.Text);

            ProdutoDAO dao = new ProdutoDAO();
            dao.excluirProduto(obj);

            new Helpers().LimparTela(this);

            // Atualiza o DataGridaView
            tabela_produtos.DataSource = dao.listarProdutos();
        }

        private void txt_pesquisa_TextChanged(object sender, EventArgs e)
        {
            string nome = "%" + txt_pesquisa.Text + "%";
            ProdutoDAO dao = new ProdutoDAO();

            tabela_produtos.DataSource = dao.listarProdutosPorNome(nome);
        }

        private void btn_pesquisar_Click(object sender, EventArgs e)
        {
            string nome = txt_pesquisa.Text;
            ProdutoDAO dao = new ProdutoDAO();

            tabela_produtos.DataSource = dao.buscarProdutoPorNome(nome);

            if (tabela_produtos.Rows.Count == 0) 
            {
                MessageBox.Show("Nenhum produto encontrado com esse nome");

                tabela_produtos.DataSource = dao.listarProdutos();
            }
        }

    
    }
}
