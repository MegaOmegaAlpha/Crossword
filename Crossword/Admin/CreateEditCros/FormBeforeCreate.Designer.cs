namespace Crossword.Admin
{
    partial class FormBeforeCreate
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FormBeforeCreate));
            this.labelWidth = new System.Windows.Forms.Label();
            this.textBoxFile = new System.Windows.Forms.TextBox();
            this.buttonGen = new System.Windows.Forms.Button();
            this.labelHeight = new System.Windows.Forms.Label();
            this.labelFile = new System.Windows.Forms.Label();
            this.buttonCreate = new System.Windows.Forms.Button();
            this.buttonDir = new System.Windows.Forms.Button();
            this.numericUpDownWidth = new System.Windows.Forms.NumericUpDown();
            this.numericUpDownHeight = new System.Windows.Forms.NumericUpDown();
            this.buttonBack = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownWidth)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownHeight)).BeginInit();
            this.SuspendLayout();
            // 
            // labelWidth
            // 
            this.labelWidth.AutoSize = true;
            this.labelWidth.Location = new System.Drawing.Point(80, 45);
            this.labelWidth.Name = "labelWidth";
            this.labelWidth.Size = new System.Drawing.Size(78, 13);
            this.labelWidth.TabIndex = 0;
            this.labelWidth.Text = "Ширина сетки";
            // 
            // textBoxFile
            // 
            this.textBoxFile.Location = new System.Drawing.Point(113, 129);
            this.textBoxFile.Name = "textBoxFile";
            this.textBoxFile.Size = new System.Drawing.Size(192, 20);
            this.textBoxFile.TabIndex = 2;
            // 
            // buttonGen
            // 
            this.buttonGen.Location = new System.Drawing.Point(12, 203);
            this.buttonGen.Name = "buttonGen";
            this.buttonGen.Size = new System.Drawing.Size(106, 23);
            this.buttonGen.TabIndex = 3;
            this.buttonGen.Text = "Сгенерировать";
            this.buttonGen.UseVisualStyleBackColor = true;
            // 
            // labelHeight
            // 
            this.labelHeight.AutoSize = true;
            this.labelHeight.Location = new System.Drawing.Point(81, 89);
            this.labelHeight.Name = "labelHeight";
            this.labelHeight.Size = new System.Drawing.Size(77, 13);
            this.labelHeight.TabIndex = 5;
            this.labelHeight.Text = "Высота сетки";
            // 
            // labelFile
            // 
            this.labelFile.AutoSize = true;
            this.labelFile.Location = new System.Drawing.Point(13, 132);
            this.labelFile.Name = "labelFile";
            this.labelFile.Size = new System.Drawing.Size(94, 13);
            this.labelFile.TabIndex = 6;
            this.labelFile.Text = "Словарь понятий";
            // 
            // buttonCreate
            // 
            this.buttonCreate.Location = new System.Drawing.Point(199, 203);
            this.buttonCreate.Name = "buttonCreate";
            this.buttonCreate.Size = new System.Drawing.Size(106, 23);
            this.buttonCreate.TabIndex = 7;
            this.buttonCreate.Text = "Создать вручную";
            this.buttonCreate.UseVisualStyleBackColor = true;
            this.buttonCreate.Click += new System.EventHandler(this.buttonCreate_Click);
            // 
            // buttonDir
            // 
            this.buttonDir.Location = new System.Drawing.Point(230, 155);
            this.buttonDir.Name = "buttonDir";
            this.buttonDir.Size = new System.Drawing.Size(75, 23);
            this.buttonDir.TabIndex = 8;
            this.buttonDir.Text = "Обзор...";
            this.buttonDir.UseVisualStyleBackColor = true;
            this.buttonDir.Click += new System.EventHandler(this.buttonDir_Click);
            // 
            // numericUpDownWidth
            // 
            this.numericUpDownWidth.Location = new System.Drawing.Point(164, 43);
            this.numericUpDownWidth.Maximum = new decimal(new int[] {
            20,
            0,
            0,
            0});
            this.numericUpDownWidth.Minimum = new decimal(new int[] {
            7,
            0,
            0,
            0});
            this.numericUpDownWidth.Name = "numericUpDownWidth";
            this.numericUpDownWidth.Size = new System.Drawing.Size(54, 20);
            this.numericUpDownWidth.TabIndex = 9;
            this.numericUpDownWidth.Value = new decimal(new int[] {
            7,
            0,
            0,
            0});
            // 
            // numericUpDownHeight
            // 
            this.numericUpDownHeight.Location = new System.Drawing.Point(164, 87);
            this.numericUpDownHeight.Maximum = new decimal(new int[] {
            20,
            0,
            0,
            0});
            this.numericUpDownHeight.Minimum = new decimal(new int[] {
            7,
            0,
            0,
            0});
            this.numericUpDownHeight.Name = "numericUpDownHeight";
            this.numericUpDownHeight.Size = new System.Drawing.Size(54, 20);
            this.numericUpDownHeight.TabIndex = 10;
            this.numericUpDownHeight.Value = new decimal(new int[] {
            7,
            0,
            0,
            0});
            // 
            // buttonBack
            // 
            this.buttonBack.Image = ((System.Drawing.Image)(resources.GetObject("buttonBack.Image")));
            this.buttonBack.Location = new System.Drawing.Point(12, 12);
            this.buttonBack.Name = "buttonBack";
            this.buttonBack.Size = new System.Drawing.Size(40, 32);
            this.buttonBack.TabIndex = 11;
            this.buttonBack.UseVisualStyleBackColor = true;
            this.buttonBack.Click += new System.EventHandler(this.buttonBack_Click);
            // 
            // FormBeforeCreate
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(317, 238);
            this.Controls.Add(this.buttonBack);
            this.Controls.Add(this.numericUpDownHeight);
            this.Controls.Add(this.numericUpDownWidth);
            this.Controls.Add(this.buttonDir);
            this.Controls.Add(this.buttonCreate);
            this.Controls.Add(this.labelFile);
            this.Controls.Add(this.labelHeight);
            this.Controls.Add(this.buttonGen);
            this.Controls.Add(this.textBoxFile);
            this.Controls.Add(this.labelWidth);
            this.Name = "FormBeforeCreate";
            this.Text = "СОЗДАНИЕ КРОССВОРДА";
            this.Load += new System.EventHandler(this.FormCreateCros_Load);
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownWidth)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownHeight)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label labelWidth;
        private System.Windows.Forms.TextBox textBoxFile;
        private System.Windows.Forms.Button buttonGen;
        private System.Windows.Forms.Label labelHeight;
        private System.Windows.Forms.Label labelFile;
        private System.Windows.Forms.Button buttonCreate;
        private System.Windows.Forms.Button buttonDir;
        private System.Windows.Forms.NumericUpDown numericUpDownWidth;
        private System.Windows.Forms.NumericUpDown numericUpDownHeight;
        private System.Windows.Forms.Button buttonBack;
    }
}