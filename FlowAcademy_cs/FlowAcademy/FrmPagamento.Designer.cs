namespace FlowAcademy
{
    partial class FrmPagamento
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            dgvPagamento = new DataGridView();
            label1 = new Label();
            txtAluno = new TextBox();
            cmbStatus = new ComboBox();
            nudValor = new NumericUpDown();
            dtpVencimento = new DateTimePicker();
            label2 = new Label();
            label3 = new Label();
            label4 = new Label();
            label5 = new Label();
            txtIdPagamento = new TextBox();
            btnEditar = new Button();
            btnSalvar = new Button();
            button4 = new Button();
            button5 = new Button();
            lblPesquisa = new Label();
            txtPesquisa = new TextBox();
            btnPesquisar = new Button();
            ((System.ComponentModel.ISupportInitialize)dgvPagamento).BeginInit();
            ((System.ComponentModel.ISupportInitialize)nudValor).BeginInit();
            SuspendLayout();
            // 
            // dgvPagamento
            // 
            dgvPagamento.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dgvPagamento.Location = new Point(435, 41);
            dgvPagamento.Margin = new Padding(3, 4, 3, 4);
            dgvPagamento.Name = "dgvPagamento";
            dgvPagamento.RowHeadersWidth = 51;
            dgvPagamento.Size = new Size(507, 543);
            dgvPagamento.TabIndex = 0;
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Location = new Point(50, 117);
            label1.Name = "label1";
            label1.Size = new Size(51, 20);
            label1.TabIndex = 1;
            label1.Text = "Aluno:";
            // 
            // txtAluno
            // 
            txtAluno.Location = new Point(105, 115);
            txtAluno.Margin = new Padding(3, 4, 3, 4);
            txtAluno.Name = "txtAluno";
            txtAluno.Size = new Size(301, 27);
            txtAluno.TabIndex = 2;
            // 
            // cmbStatus
            // 
            cmbStatus.FormattingEnabled = true;
            cmbStatus.Location = new Point(105, 327);
            cmbStatus.Margin = new Padding(3, 4, 3, 4);
            cmbStatus.Name = "cmbStatus";
            cmbStatus.Size = new Size(180, 28);
            cmbStatus.TabIndex = 3;
            // 
            // nudValor
            // 
            nudValor.Location = new Point(105, 256);
            nudValor.Margin = new Padding(3, 4, 3, 4);
            nudValor.Maximum = new decimal(new int[] { 1000000, 0, 0, 0 });
            nudValor.Name = "nudValor";
            nudValor.Size = new Size(181, 27);
            nudValor.TabIndex = 4;
            // 
            // dtpVencimento
            // 
            dtpVencimento.Location = new Point(105, 185);
            dtpVencimento.Margin = new Padding(3, 4, 3, 4);
            dtpVencimento.Name = "dtpVencimento";
            dtpVencimento.Size = new Size(300, 27);
            dtpVencimento.TabIndex = 5;
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.Location = new Point(50, 325);
            label2.Name = "label2";
            label2.Size = new Size(52, 20);
            label2.TabIndex = 1;
            label2.Text = "Status:";
            // 
            // label3
            // 
            label3.AutoSize = true;
            label3.Location = new Point(30, 256);
            label3.Name = "label3";
            label3.Size = new Size(77, 20);
            label3.TabIndex = 1;
            label3.Text = "Valor (R$):";
            // 
            // label4
            // 
            label4.AutoSize = true;
            label4.Location = new Point(15, 187);
            label4.Name = "label4";
            label4.Size = new Size(90, 20);
            label4.TabIndex = 1;
            label4.Text = "Vencimento:";
            // 
            // label5
            // 
            label5.AutoSize = true;
            label5.Location = new Point(2, 48);
            label5.Name = "label5";
            label5.Size = new Size(104, 20);
            label5.TabIndex = 1;
            label5.Text = "Id Pagamento:";
            // 
            // txtIdPagamento
            // 
            txtIdPagamento.Location = new Point(105, 44);
            txtIdPagamento.Margin = new Padding(3, 4, 3, 4);
            txtIdPagamento.Name = "txtIdPagamento";
            txtIdPagamento.ReadOnly = true;
            txtIdPagamento.Size = new Size(301, 27);
            txtIdPagamento.TabIndex = 2;
            // 
            // btnEditar
            // 
            btnEditar.Location = new Point(221, 408);
            btnEditar.Margin = new Padding(3, 4, 3, 4);
            btnEditar.Name = "btnEditar";
            btnEditar.Size = new Size(111, 85);
            btnEditar.TabIndex = 56;
            btnEditar.Text = "&Editar";
            btnEditar.UseVisualStyleBackColor = true;
            // 
            // btnSalvar
            // 
            btnSalvar.Location = new Point(55, 408);
            btnSalvar.Margin = new Padding(3, 4, 3, 4);
            btnSalvar.Name = "btnSalvar";
            btnSalvar.Size = new Size(111, 85);
            btnSalvar.TabIndex = 55;
            btnSalvar.Text = "&Salvar";
            btnSalvar.UseVisualStyleBackColor = true;
            // 
            // button4
            // 
            button4.Location = new Point(59, 543);
            button4.Margin = new Padding(3, 4, 3, 4);
            button4.Name = "button4";
            button4.Size = new Size(106, 89);
            button4.TabIndex = 54;
            button4.Text = "&Cancelar";
            button4.UseVisualStyleBackColor = true;
            // 
            // button5
            // 
            button5.Location = new Point(221, 543);
            button5.Margin = new Padding(3, 4, 3, 4);
            button5.Name = "button5";
            button5.Size = new Size(106, 89);
            button5.TabIndex = 53;
            button5.Text = "E&xcluir";
            button5.UseVisualStyleBackColor = true;
            // 
            // lblPesquisa
            // 
            lblPesquisa.AutoSize = true;
            lblPesquisa.Location = new Point(435, 603);
            lblPesquisa.Name = "lblPesquisa";
            lblPesquisa.Size = new Size(111, 20);
            lblPesquisa.TabIndex = 57;
            lblPesquisa.Text = "Pesquisar aluno";
            // 
            // txtPesquisa
            // 
            txtPesquisa.Location = new Point(545, 599);
            txtPesquisa.Margin = new Padding(3, 4, 3, 4);
            txtPesquisa.Name = "txtPesquisa";
            txtPesquisa.Size = new Size(253, 27);
            txtPesquisa.TabIndex = 58;
            // 
            // btnPesquisar
            // 
            btnPesquisar.Location = new Point(817, 597);
            btnPesquisar.Margin = new Padding(3, 4, 3, 4);
            btnPesquisar.Name = "btnPesquisar";
            btnPesquisar.Size = new Size(126, 33);
            btnPesquisar.TabIndex = 59;
            btnPesquisar.Text = "Pesquisar";
            btnPesquisar.UseVisualStyleBackColor = true;
            btnPesquisar.Click += btnPesquisar_Click;
            // 
            // FrmPagamento
            // 
            AutoScaleDimensions = new SizeF(8F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(1038, 749);
            Controls.Add(btnPesquisar);
            Controls.Add(txtPesquisa);
            Controls.Add(lblPesquisa);
            Controls.Add(btnEditar);
            Controls.Add(btnSalvar);
            Controls.Add(button4);
            Controls.Add(button5);
            Controls.Add(dtpVencimento);
            Controls.Add(nudValor);
            Controls.Add(cmbStatus);
            Controls.Add(txtIdPagamento);
            Controls.Add(txtAluno);
            Controls.Add(label4);
            Controls.Add(label3);
            Controls.Add(label5);
            Controls.Add(label2);
            Controls.Add(label1);
            Controls.Add(dgvPagamento);
            Margin = new Padding(3, 4, 3, 4);
            Name = "FrmPagamento";
            Text = "FrmPagamento";
            Load += FrmPagamento_Load;
            ((System.ComponentModel.ISupportInitialize)dgvPagamento).EndInit();
            ((System.ComponentModel.ISupportInitialize)nudValor).EndInit();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private DataGridView dgvPagamento;
        private Label label1;
        private TextBox txtAluno;
        private ComboBox cmbStatus;
        private NumericUpDown nudValor;
        private DateTimePicker dtpVencimento;
        private Label label2;
        private Label label3;
        private Label label4;
        private Label label5;
        private TextBox txtIdPagamento;
        private Button btnEditar;
        private Button btnSalvar;
        private Button button4;
        private Button button5;
        private Label lblPesquisa;
        private TextBox txtPesquisa;
        private Button btnPesquisar;
    }
}
