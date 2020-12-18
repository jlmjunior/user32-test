using BotTeste.Util;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Xml.XPath;

namespace BotTeste
{
    public partial class Atom : Form
    {
        public Atom()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            this.WindowState = FormWindowState.Minimized;

            foreach (string comando in listComandos.Items)
            {
                Evento.ExecutaComando(comando);
            }
        }
        private void textBox1_KeyPress(object sender, KeyPressEventArgs e)
        {
            //if (Evento.currentType == 0) return;

            if (e.KeyChar.ToString().ToUpper() == "Q")
            {
                Evento.x = Cursor.Position.X;
                Evento.y = Cursor.Position.Y;
                
                txtConfigText.Text = string.Empty;
                txtConfigText.Text = $"[{lblEventoSelecionado.Text}]({Evento.x},{Evento.y})";
            }
        }

        private void sairToolStripMenuItem_Click(object sender, EventArgs e)
        {
            DialogResult result = MessageBox.Show("Tem certeza que deseja sair?", "Sair", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);

            if (result == DialogResult.Yes) Application.Exit();
        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (boxComandos.SelectedItem.ToString().ToLower() == "sleep")
            {
                txtConfigText.Text = "[Sleep]";
            }

            txtConfig.Focus();
            lblEventoSelecionado.Text = boxComandos.SelectedItem.ToString();
        }

        private void txtConfig_Leave(object sender, EventArgs e)
        {
            lblEscaner.Text = "Escaneamento desligado!";
        }

        private void txtConfig_Enter(object sender, EventArgs e)
        {
            //if (Evento.currentType == 0) return;

            lblEscaner.Text = "Escaneando...";
        }

        private void btnAdicionar_Click(object sender, EventArgs e)
        {
            listComandos.Items.Add(txtConfigText.Text);
        }

        private void btnRemover_Click(object sender, EventArgs e)
        {
            listComandos.Items.Remove(listComandos.SelectedItem);
        }

        private void salvarToolStripMenuItem_Click(object sender, EventArgs e)
        {
            string path = string.Empty;

            using (var file = new SaveFileDialog())
            {
                file.Filter = "txt files (*.txt)|*.txt|All files (*.*)|*.*";

                DialogResult resultado = file.ShowDialog();

                if (resultado == DialogResult.OK && !string.IsNullOrWhiteSpace(file.FileName))
                {
                    path = file.FileName;
                }
                else if (resultado == DialogResult.Cancel)
                {
                    return;
                }
            }

            if (string.IsNullOrWhiteSpace(path))
            {
                AlertaErro("Não foi possível salvar o documento!");
                return;
            }

            try
            {
                Util.Saves save = new Util.Saves();
                List<string> comandos = new List<string>();

                foreach (var comando in listComandos.Items)
                {
                    comandos.Add(comando.ToString());
                }

                save.SalvarArquivo(path, comandos);
            }
            catch (Exception ex)
            {
                AlertaErro(ex.ToString());
            }
            finally
            {
                AlertaSucesso("Documento salvo!");
            }

        }

        private void carregarToolStripMenuItem_Click(object sender, EventArgs e)
        {
            List<string> comandos = new List<string>();

            using (var file = new OpenFileDialog())
            {
                file.Filter = "txt files (*.txt)|*.txt";

                DialogResult resultado = file.ShowDialog();

                if (resultado == DialogResult.OK && !string.IsNullOrWhiteSpace(file.FileName))
                {
                    Util.Saves save = new Util.Saves();

                    comandos = save.LerArquivo(file.FileName);
                }
                else if (resultado == DialogResult.Cancel)
                {
                    return;
                }
            }

            try
            {
                listComandos.Items.Clear();

                foreach (string comando in comandos)
                {
                    listComandos.Items.Add(comando);
                }
            }
            catch (Exception ex)
            {
                AlertaErro(ex.ToString());
            }
        }

        private void novoToolStripMenuItem_Click(object sender, EventArgs e)
        {
            listComandos.Items.Clear();
        }

        #region ALERTAS
        private void AlertaErro(string erro)
        {
            MessageBox.Show(erro, "Erro", MessageBoxButtons.OK, MessageBoxIcon.Error);
        }

        private void AlertaSucesso(string sucesso)
        {
            MessageBox.Show(sucesso, "Sucesso", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        private void AlertaAtencao(string atencao)
        {
            MessageBox.Show(atencao, "Sucesso", MessageBoxButtons.OK, MessageBoxIcon.Warning);
        }
        #endregion
    }
}
