namespace WIN_Cliente
{
    partial class Form1
    {
        /// <summary>
        /// Variable del diseñador requerida.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Limpiar los recursos que se estén utilizando.
        /// </summary>
        /// <param name="disposing">true si los recursos administrados se deben desechar; false en caso contrario.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Código generado por el Diseñador de Windows Forms

        /// <summary>
        /// Método necesario para admitir el Diseñador. No se puede modificar
        /// el contenido del método con el editor de código.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            this.dataGridView1 = new System.Windows.Forms.DataGridView();
            this.bdTiendaDataSet = new WIN_Cliente.BdTiendaDataSet();
            this.fmacliBindingSource = new System.Windows.Forms.BindingSource(this.components);
            this.fmacliTableAdapter = new WIN_Cliente.BdTiendaDataSetTableAdapters.FmacliTableAdapter();
            this.fccodiDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.fcrucDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.fcncliDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.fcnombDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.fcapepDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.fcapemDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.fcdcliDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.fcteleDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.fcmailDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.fccubiDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.fcqtotDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.fctotalDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.fcfcreDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.fcfmodDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.fcmuseDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.fctiendaDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.bdTiendaDataSet)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.fmacliBindingSource)).BeginInit();
            this.SuspendLayout();
            // 
            // dataGridView1
            // 
            this.dataGridView1.AllowUserToAddRows = false;
            this.dataGridView1.AutoGenerateColumns = false;
            this.dataGridView1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridView1.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.fccodiDataGridViewTextBoxColumn,
            this.fcrucDataGridViewTextBoxColumn,
            this.fcncliDataGridViewTextBoxColumn,
            this.fcnombDataGridViewTextBoxColumn,
            this.fcapepDataGridViewTextBoxColumn,
            this.fcapemDataGridViewTextBoxColumn,
            this.fcdcliDataGridViewTextBoxColumn,
            this.fcteleDataGridViewTextBoxColumn,
            this.fcmailDataGridViewTextBoxColumn,
            this.fccubiDataGridViewTextBoxColumn,
            this.fcqtotDataGridViewTextBoxColumn,
            this.fctotalDataGridViewTextBoxColumn,
            this.fcfcreDataGridViewTextBoxColumn,
            this.fcfmodDataGridViewTextBoxColumn,
            this.fcmuseDataGridViewTextBoxColumn,
            this.fctiendaDataGridViewTextBoxColumn});
            this.dataGridView1.DataSource = this.fmacliBindingSource;
            this.dataGridView1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dataGridView1.Location = new System.Drawing.Point(0, 0);
            this.dataGridView1.Name = "dataGridView1";
            this.dataGridView1.Size = new System.Drawing.Size(977, 457);
            this.dataGridView1.TabIndex = 0;
            // 
            // bdTiendaDataSet
            // 
            this.bdTiendaDataSet.DataSetName = "BdTiendaDataSet";
            this.bdTiendaDataSet.SchemaSerializationMode = System.Data.SchemaSerializationMode.IncludeSchema;
            // 
            // fmacliBindingSource
            // 
            this.fmacliBindingSource.DataMember = "Fmacli";
            this.fmacliBindingSource.DataSource = this.bdTiendaDataSet;
            // 
            // fmacliTableAdapter
            // 
            this.fmacliTableAdapter.ClearBeforeFill = true;
            // 
            // fccodiDataGridViewTextBoxColumn
            // 
            this.fccodiDataGridViewTextBoxColumn.DataPropertyName = "fc_codi";
            this.fccodiDataGridViewTextBoxColumn.HeaderText = "fc_codi";
            this.fccodiDataGridViewTextBoxColumn.Name = "fccodiDataGridViewTextBoxColumn";
            // 
            // fcrucDataGridViewTextBoxColumn
            // 
            this.fcrucDataGridViewTextBoxColumn.DataPropertyName = "fc_ruc";
            this.fcrucDataGridViewTextBoxColumn.HeaderText = "fc_ruc";
            this.fcrucDataGridViewTextBoxColumn.Name = "fcrucDataGridViewTextBoxColumn";
            // 
            // fcncliDataGridViewTextBoxColumn
            // 
            this.fcncliDataGridViewTextBoxColumn.DataPropertyName = "fc_ncli";
            this.fcncliDataGridViewTextBoxColumn.HeaderText = "fc_ncli";
            this.fcncliDataGridViewTextBoxColumn.Name = "fcncliDataGridViewTextBoxColumn";
            // 
            // fcnombDataGridViewTextBoxColumn
            // 
            this.fcnombDataGridViewTextBoxColumn.DataPropertyName = "fc_nomb";
            this.fcnombDataGridViewTextBoxColumn.HeaderText = "fc_nomb";
            this.fcnombDataGridViewTextBoxColumn.Name = "fcnombDataGridViewTextBoxColumn";
            // 
            // fcapepDataGridViewTextBoxColumn
            // 
            this.fcapepDataGridViewTextBoxColumn.DataPropertyName = "fc_apep";
            this.fcapepDataGridViewTextBoxColumn.HeaderText = "fc_apep";
            this.fcapepDataGridViewTextBoxColumn.Name = "fcapepDataGridViewTextBoxColumn";
            // 
            // fcapemDataGridViewTextBoxColumn
            // 
            this.fcapemDataGridViewTextBoxColumn.DataPropertyName = "fc_apem";
            this.fcapemDataGridViewTextBoxColumn.HeaderText = "fc_apem";
            this.fcapemDataGridViewTextBoxColumn.Name = "fcapemDataGridViewTextBoxColumn";
            // 
            // fcdcliDataGridViewTextBoxColumn
            // 
            this.fcdcliDataGridViewTextBoxColumn.DataPropertyName = "fc_dcli";
            this.fcdcliDataGridViewTextBoxColumn.HeaderText = "fc_dcli";
            this.fcdcliDataGridViewTextBoxColumn.Name = "fcdcliDataGridViewTextBoxColumn";
            // 
            // fcteleDataGridViewTextBoxColumn
            // 
            this.fcteleDataGridViewTextBoxColumn.DataPropertyName = "fc_tele";
            this.fcteleDataGridViewTextBoxColumn.HeaderText = "fc_tele";
            this.fcteleDataGridViewTextBoxColumn.Name = "fcteleDataGridViewTextBoxColumn";
            // 
            // fcmailDataGridViewTextBoxColumn
            // 
            this.fcmailDataGridViewTextBoxColumn.DataPropertyName = "fc_mail";
            this.fcmailDataGridViewTextBoxColumn.HeaderText = "fc_mail";
            this.fcmailDataGridViewTextBoxColumn.Name = "fcmailDataGridViewTextBoxColumn";
            // 
            // fccubiDataGridViewTextBoxColumn
            // 
            this.fccubiDataGridViewTextBoxColumn.DataPropertyName = "fc_cubi";
            this.fccubiDataGridViewTextBoxColumn.HeaderText = "fc_cubi";
            this.fccubiDataGridViewTextBoxColumn.Name = "fccubiDataGridViewTextBoxColumn";
            // 
            // fcqtotDataGridViewTextBoxColumn
            // 
            this.fcqtotDataGridViewTextBoxColumn.DataPropertyName = "fc_qtot";
            this.fcqtotDataGridViewTextBoxColumn.HeaderText = "fc_qtot";
            this.fcqtotDataGridViewTextBoxColumn.Name = "fcqtotDataGridViewTextBoxColumn";
            // 
            // fctotalDataGridViewTextBoxColumn
            // 
            this.fctotalDataGridViewTextBoxColumn.DataPropertyName = "fc_total";
            this.fctotalDataGridViewTextBoxColumn.HeaderText = "fc_total";
            this.fctotalDataGridViewTextBoxColumn.Name = "fctotalDataGridViewTextBoxColumn";
            // 
            // fcfcreDataGridViewTextBoxColumn
            // 
            this.fcfcreDataGridViewTextBoxColumn.DataPropertyName = "fc_fcre";
            this.fcfcreDataGridViewTextBoxColumn.HeaderText = "fc_fcre";
            this.fcfcreDataGridViewTextBoxColumn.Name = "fcfcreDataGridViewTextBoxColumn";
            // 
            // fcfmodDataGridViewTextBoxColumn
            // 
            this.fcfmodDataGridViewTextBoxColumn.DataPropertyName = "fc_fmod";
            this.fcfmodDataGridViewTextBoxColumn.HeaderText = "fc_fmod";
            this.fcfmodDataGridViewTextBoxColumn.Name = "fcfmodDataGridViewTextBoxColumn";
            // 
            // fcmuseDataGridViewTextBoxColumn
            // 
            this.fcmuseDataGridViewTextBoxColumn.DataPropertyName = "fc_muse";
            this.fcmuseDataGridViewTextBoxColumn.HeaderText = "fc_muse";
            this.fcmuseDataGridViewTextBoxColumn.Name = "fcmuseDataGridViewTextBoxColumn";
            // 
            // fctiendaDataGridViewTextBoxColumn
            // 
            this.fctiendaDataGridViewTextBoxColumn.DataPropertyName = "fc_tienda";
            this.fctiendaDataGridViewTextBoxColumn.HeaderText = "fc_tienda";
            this.fctiendaDataGridViewTextBoxColumn.Name = "fctiendaDataGridViewTextBoxColumn";
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(977, 457);
            this.Controls.Add(this.dataGridView1);
            this.Name = "Form1";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Cliente - Bata Peru";
            this.Load += new System.EventHandler(this.Form1_Load);
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.bdTiendaDataSet)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.fmacliBindingSource)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.DataGridView dataGridView1;
        private BdTiendaDataSet bdTiendaDataSet;
        private System.Windows.Forms.BindingSource fmacliBindingSource;
        private BdTiendaDataSetTableAdapters.FmacliTableAdapter fmacliTableAdapter;
        private System.Windows.Forms.DataGridViewTextBoxColumn fccodiDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn fcrucDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn fcncliDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn fcnombDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn fcapepDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn fcapemDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn fcdcliDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn fcteleDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn fcmailDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn fccubiDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn fcqtotDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn fctotalDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn fcfcreDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn fcfmodDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn fcmuseDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn fctiendaDataGridViewTextBoxColumn;
    }
}

