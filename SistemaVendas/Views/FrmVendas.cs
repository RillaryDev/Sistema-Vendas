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
    public partial class FrmVendas : Form
    {

        Cliente cliente = new Cliente();
        ClienteDAO cDao = new ClienteDAO();

        Produto produto = new Produto();
        ProdutoDAO pDao = new ProdutoDAO();

        int qtd;
        decimal preco;
        decimal subtotal, total;

        DataTable carrinho = new DataTable();

        public FrmVendas()
        {
            InitializeComponent();
            carrinho.Columns.Add("Código", typeof(int));
            carrinho.Columns.Add("Produto", typeof(string));
            carrinho.Columns.Add("Qtd", typeof(int));
            carrinho.Columns.Add("Preço", typeof(decimal));
            carrinho.Columns.Add("Subtotal", typeof(decimal));

            tabela_produtos.DataSource = carrinho;
        }

        private void txt_cpf_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == 13) 
            {
                cliente = cDao.retornaClientePorCpf(txt_cpf.Text);

                if (cliente != null)
                {
                    txt_nome.Text = cliente.nome;
                }
                else 
                {
                    txt_cpf.Clear();
                    txt_cpf.Focus();
                }
                
            }
        }

        private void btn_adicionarItem_Click(object sender, EventArgs e)
        {
            try
            {
                qtd = int.Parse(txt_qtd_estoque.Text);
                preco = decimal.Parse(txt_preco.Text);

                subtotal = qtd * preco;
                total += subtotal;

                carrinho.Rows.Add(int.Parse(txt_codigo.Text), txt_descricao, qtd, preco, subtotal);

                txt_codigo.Clear();
                txt_descricao.Clear();
                txt_qtd_estoque.Clear();
                txt_preco.Clear();
                txt_codigo.Focus();
            }
            catch (Exception error)
            {

                MessageBox.Show("Digite o código do Produto!");
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            decimal subproduto = decimal.Parse(tabela_produtos.CurrentRow.Cells[4].Value.ToString());
            int indice = tabela_produtos.CurrentRow.Index;
            DataRow linha = carrinho.Rows[indice];

            carrinho.Rows.Remove(linha);
            carrinho.AcceptChanges();

            total -= subproduto;
            txt_total.Text = total.ToString();

            MessageBox.Show("Item removido do carrinho com sucesso!");

        }

        private void FrmVendas_Load(object sender, EventArgs e)
        {
            txt_data.Text = DateTime.Now.ToShortDateString();
        }

        private void btn_pagamento_Click(object sender, EventArgs e)
        {
            DateTime dataAtual = DateTime.Parse(txt_data.Text);
            FrmPagamentos view = new FrmPagamentos(cliente, carrinho, dataAtual);

            // Passando o total para a tela de pagamentos
            view.total.Text = total.ToString();
            view.ShowDialog();
        }

        private void txt_codigo_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == 13) 
            {
                produto = pDao.retornaProdutoPorCodigo(int.Parse(txt_codigo.Text));

                if (produto != null)
                {
                    txt_descricao.Text = produto.descricao;
                    txt_preco.Text = produto.preco.ToString();
                }
                else 
                {
                    txt_codigo.Clear();
                    txt_codigo.Focus();
                }
                
            }
        }


    }
}
