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
    public partial class FrmPagamentos : Form
    {
        Cliente cliente = new Cliente();
        DataTable carrinho = new DataTable();
        DateTime dataAtual;

        public FrmPagamentos(Cliente cliente, DataTable carrinho, DateTime dataAtual)
        {
            this.cliente = cliente;
            this.carrinho = carrinho;
            this.dataAtual = dataAtual;

            InitializeComponent();
        }

        private void btn_finalizar_Click(object sender, EventArgs e)
        {
            try
            {
                decimal dinheiro, cartao, troco, total_venda, totalpago;

                dinheiro = decimal.Parse(txt_dinheiro.Text);
                cartao = decimal.Parse(txt_cartao.Text);
                total_venda = decimal.Parse(total.Text);

                totalpago = dinheiro + cartao;

                if (totalpago < total_venda)
                {
                    MessageBox.Show("O total pago é menor que o valor Total da venda");
                }
                else
                {
                    troco = totalpago - total_venda;

                    Venda venda = new Venda();
                    venda.cliente_id = cliente.codigo;
                    venda.data_venda = dataAtual;
                    venda.total_venda = total_venda;

                    VendaDAO vdao = new VendaDAO();
                    txt_troco.Text = troco.ToString();
                    vdao.cadastrarVenda(venda);

                    foreach (DataRow linha in carrinho.Rows) 
                    {
                        ItemVenda item = new ItemVenda();
                        item.venda_id = vdao.retornaIdUltimaVenda();
                        item.produto_id = int.Parse(linha["Código"].ToString());
                        item.qtd = int.Parse(linha["Qtd"].ToString());
                        item.subtotal = decimal.Parse(linha["Subtotal"].ToString());

                        ItemVendaDAO itemdao = new ItemVendaDAO();
                        itemdao.cadastrarItemVenda(item);
                    }

                    MessageBox.Show("Venda finalizada com sucesso!");

                    this.Dispose();

                    new FrmVendas().ShowDialog();
                   
                }
            }
            catch (Exception)
            {

                throw;
            }
        }

        private void FrmPagamentos_Load(object sender, EventArgs e)
        {
            txt_troco.Text = "0,00";
            txt_dinheiro.Text = "0,00";
            txt_cartao.Text = "0,00";
        }
    }
}
