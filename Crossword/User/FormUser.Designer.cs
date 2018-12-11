namespace Crossword.User
{
    partial class FormUser
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FormUser));
            this.tableContainer = new System.Windows.Forms.Panel();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.label3 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.listBoxVert = new System.Windows.Forms.ListBox();
            this.listBoxHor = new System.Windows.Forms.ListBox();
            this.label1 = new System.Windows.Forms.Label();
            this.textBox1 = new System.Windows.Forms.TextBox();
            this.buttonBack = new System.Windows.Forms.Button();
            this.buttonSaveSolution = new System.Windows.Forms.Button();
            this.buttonHelp = new System.Windows.Forms.Button();
            this.buttonCheckSolution = new System.Windows.Forms.Button();
            this.pictureBoxUp = new System.Windows.Forms.PictureBox();
            this.pictureBoxDown = new System.Windows.Forms.PictureBox();
            this.groupBox1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBoxUp)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBoxDown)).BeginInit();
            this.SuspendLayout();
            // 
            // tableContainer
            // 
            this.tableContainer.Location = new System.Drawing.Point(12, 74);
            this.tableContainer.Name = "tableContainer";
            this.tableContainer.Size = new System.Drawing.Size(377, 364);
            this.tableContainer.TabIndex = 6;
            this.tableContainer.Paint += new System.Windows.Forms.PaintEventHandler(this.panel1_Paint);
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.label3);
            this.groupBox1.Controls.Add(this.label2);
            this.groupBox1.Controls.Add(this.listBoxVert);
            this.groupBox1.Controls.Add(this.listBoxHor);
            this.groupBox1.Location = new System.Drawing.Point(405, 74);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(320, 364);
            this.groupBox1.TabIndex = 7;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Вопросы";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(6, 182);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(80, 13);
            this.label3.TabIndex = 3;
            this.label3.Text = "По вертикали:";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(6, 16);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(91, 13);
            this.label2.TabIndex = 2;
            this.label2.Text = "По горизонтали:";
            // 
            // listBoxVert
            // 
            this.listBoxVert.FormattingEnabled = true;
            this.listBoxVert.HorizontalScrollbar = true;
            this.listBoxVert.Location = new System.Drawing.Point(7, 202);
            this.listBoxVert.Name = "listBoxVert";
            this.listBoxVert.Size = new System.Drawing.Size(307, 147);
            this.listBoxVert.TabIndex = 1;
            this.listBoxVert.SelectedIndexChanged += new System.EventHandler(this.listBoxVert_SelectedIndexChanged);
            // 
            // listBoxHor
            // 
            this.listBoxHor.FormattingEnabled = true;
            this.listBoxHor.HorizontalScrollbar = true;
            this.listBoxHor.Location = new System.Drawing.Point(7, 32);
            this.listBoxHor.Name = "listBoxHor";
            this.listBoxHor.Size = new System.Drawing.Size(307, 147);
            this.listBoxHor.TabIndex = 0;
            this.listBoxHor.SelectedIndexChanged += new System.EventHandler(this.listBoxHor_SelectedIndexChanged);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(402, 31);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(173, 13);
            this.label1.TabIndex = 8;
            this.label1.Text = "Текущее количество подсказок:";
            // 
            // textBox1
            // 
            this.textBox1.Location = new System.Drawing.Point(581, 28);
            this.textBox1.Name = "textBox1";
            this.textBox1.Size = new System.Drawing.Size(21, 20);
            this.textBox1.TabIndex = 9;
            this.textBox1.Text = "1";
            // 
            // buttonBack
            // 
            this.buttonBack.Image = ((System.Drawing.Image)(resources.GetObject("buttonBack.Image")));
            this.buttonBack.Location = new System.Drawing.Point(12, 9);
            this.buttonBack.Name = "buttonBack";
            this.buttonBack.Size = new System.Drawing.Size(64, 48);
            this.buttonBack.TabIndex = 10;
            this.buttonBack.UseVisualStyleBackColor = true;
            this.buttonBack.Click += new System.EventHandler(this.buttonBack_Click);
            // 
            // buttonSaveSolution
            // 
            this.buttonSaveSolution.Image = ((System.Drawing.Image)(resources.GetObject("buttonSaveSolution.Image")));
            this.buttonSaveSolution.Location = new System.Drawing.Point(91, 9);
            this.buttonSaveSolution.Name = "buttonSaveSolution";
            this.buttonSaveSolution.Size = new System.Drawing.Size(69, 51);
            this.buttonSaveSolution.TabIndex = 11;
            this.buttonSaveSolution.UseVisualStyleBackColor = true;
            // 
            // buttonHelp
            // 
            this.buttonHelp.Image = ((System.Drawing.Image)(resources.GetObject("buttonHelp.Image")));
            this.buttonHelp.Location = new System.Drawing.Point(622, 14);
            this.buttonHelp.Name = "buttonHelp";
            this.buttonHelp.Size = new System.Drawing.Size(48, 46);
            this.buttonHelp.TabIndex = 12;
            this.buttonHelp.UseVisualStyleBackColor = true;
            // 
            // buttonCheckSolution
            // 
            this.buttonCheckSolution.Image = ((System.Drawing.Image)(resources.GetObject("buttonCheckSolution.Image")));
            this.buttonCheckSolution.Location = new System.Drawing.Point(725, 236);
            this.buttonCheckSolution.Name = "buttonCheckSolution";
            this.buttonCheckSolution.Size = new System.Drawing.Size(65, 53);
            this.buttonCheckSolution.TabIndex = 14;
            this.buttonCheckSolution.UseVisualStyleBackColor = true;
            this.buttonCheckSolution.Click += new System.EventHandler(this.buttonCheckSolution_Click);
            // 
            // pictureBoxUp
            // 
            this.pictureBoxUp.Image = ((System.Drawing.Image)(resources.GetObject("pictureBoxUp.Image")));
            this.pictureBoxUp.Location = new System.Drawing.Point(731, 179);
            this.pictureBoxUp.Name = "pictureBoxUp";
            this.pictureBoxUp.Size = new System.Drawing.Size(50, 51);
            this.pictureBoxUp.TabIndex = 16;
            this.pictureBoxUp.TabStop = false;
            this.pictureBoxUp.Visible = false;
            // 
            // pictureBoxDown
            // 
            this.pictureBoxDown.Image = ((System.Drawing.Image)(resources.GetObject("pictureBoxDown.Image")));
            this.pictureBoxDown.Location = new System.Drawing.Point(731, 295);
            this.pictureBoxDown.Name = "pictureBoxDown";
            this.pictureBoxDown.Size = new System.Drawing.Size(50, 51);
            this.pictureBoxDown.TabIndex = 17;
            this.pictureBoxDown.TabStop = false;
            this.pictureBoxDown.Visible = false;
            // 
            // FormUser
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Controls.Add(this.pictureBoxDown);
            this.Controls.Add(this.pictureBoxUp);
            this.Controls.Add(this.buttonCheckSolution);
            this.Controls.Add(this.buttonHelp);
            this.Controls.Add(this.buttonSaveSolution);
            this.Controls.Add(this.buttonBack);
            this.Controls.Add(this.textBox1);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.tableContainer);
            this.Name = "FormUser";
            this.Text = "РАЗГАДЫВАНИЕ КРОССВОРДА";
            this.Load += new System.EventHandler(this.FormUser_Load);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBoxUp)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBoxDown)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.Panel tableContainer;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.ListBox listBoxVert;
        private System.Windows.Forms.ListBox listBoxHor;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox textBox1;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Button buttonBack;
        private System.Windows.Forms.Button buttonSaveSolution;
        private System.Windows.Forms.Button buttonHelp;
        private System.Windows.Forms.Button buttonCheckSolution;
        private System.Windows.Forms.PictureBox pictureBoxUp;
        private System.Windows.Forms.PictureBox pictureBoxDown;
    }
}