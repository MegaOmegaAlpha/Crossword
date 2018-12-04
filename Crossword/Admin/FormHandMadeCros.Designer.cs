namespace Crossword.Admin
{
    partial class FormHandMadeCros
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FormHandMadeCros));
            this.TableContainer = new System.Windows.Forms.Panel();
            this.groupBoxTasks = new System.Windows.Forms.GroupBox();
            this.buttonDelNotion = new System.Windows.Forms.Button();
            this.labelHor = new System.Windows.Forms.Label();
            this.labelVert = new System.Windows.Forms.Label();
            this.listBoxHor = new System.Windows.Forms.ListBox();
            this.listBoxVert = new System.Windows.Forms.ListBox();
            this.groupBoxDict = new System.Windows.Forms.GroupBox();
            this.textBoxSearch = new System.Windows.Forms.TextBox();
            this.buttonSortLen = new System.Windows.Forms.Button();
            this.buttonSort = new System.Windows.Forms.Button();
            this.buttonAddNotion = new System.Windows.Forms.Button();
            this.listBoxDict = new System.Windows.Forms.ListBox();
            this.buttonSaveCros = new System.Windows.Forms.Button();
            this.buttonBack = new System.Windows.Forms.Button();
            this.groupBoxTasks.SuspendLayout();
            this.groupBoxDict.SuspendLayout();
            this.SuspendLayout();
            // 
            // TableContainer
            // 
            this.TableContainer.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.TableContainer.Location = new System.Drawing.Point(12, 53);
            this.TableContainer.Name = "TableContainer";
            this.TableContainer.Size = new System.Drawing.Size(443, 443);
            this.TableContainer.TabIndex = 0;
            // 
            // groupBoxTasks
            // 
            this.groupBoxTasks.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.groupBoxTasks.Controls.Add(this.buttonDelNotion);
            this.groupBoxTasks.Controls.Add(this.labelHor);
            this.groupBoxTasks.Controls.Add(this.labelVert);
            this.groupBoxTasks.Controls.Add(this.listBoxHor);
            this.groupBoxTasks.Controls.Add(this.listBoxVert);
            this.groupBoxTasks.Location = new System.Drawing.Point(461, 53);
            this.groupBoxTasks.Name = "groupBoxTasks";
            this.groupBoxTasks.Size = new System.Drawing.Size(298, 443);
            this.groupBoxTasks.TabIndex = 1;
            this.groupBoxTasks.TabStop = false;
            this.groupBoxTasks.Text = "Редактирование кроссворда";
            // 
            // buttonDelNotion
            // 
            this.buttonDelNotion.Image = ((System.Drawing.Image)(resources.GetObject("buttonDelNotion.Image")));
            this.buttonDelNotion.Location = new System.Drawing.Point(122, 387);
            this.buttonDelNotion.Name = "buttonDelNotion";
            this.buttonDelNotion.Size = new System.Drawing.Size(49, 45);
            this.buttonDelNotion.TabIndex = 5;
            this.buttonDelNotion.UseVisualStyleBackColor = true;
            // 
            // labelHor
            // 
            this.labelHor.AutoSize = true;
            this.labelHor.Location = new System.Drawing.Point(151, 28);
            this.labelHor.Name = "labelHor";
            this.labelHor.Size = new System.Drawing.Size(88, 13);
            this.labelHor.TabIndex = 3;
            this.labelHor.Text = "По горизонтали";
            // 
            // labelVert
            // 
            this.labelVert.AutoSize = true;
            this.labelVert.Location = new System.Drawing.Point(6, 28);
            this.labelVert.Name = "labelVert";
            this.labelVert.Size = new System.Drawing.Size(77, 13);
            this.labelVert.TabIndex = 2;
            this.labelVert.Text = "По вертикали";
            // 
            // listBoxHor
            // 
            this.listBoxHor.FormattingEnabled = true;
            this.listBoxHor.Location = new System.Drawing.Point(154, 44);
            this.listBoxHor.Name = "listBoxHor";
            this.listBoxHor.Size = new System.Drawing.Size(138, 329);
            this.listBoxHor.TabIndex = 1;
            // 
            // listBoxVert
            // 
            this.listBoxVert.FormattingEnabled = true;
            this.listBoxVert.Location = new System.Drawing.Point(6, 44);
            this.listBoxVert.Name = "listBoxVert";
            this.listBoxVert.Size = new System.Drawing.Size(138, 329);
            this.listBoxVert.TabIndex = 0;
            // 
            // groupBoxDict
            // 
            this.groupBoxDict.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.groupBoxDict.Controls.Add(this.textBoxSearch);
            this.groupBoxDict.Controls.Add(this.buttonSortLen);
            this.groupBoxDict.Controls.Add(this.buttonSort);
            this.groupBoxDict.Controls.Add(this.buttonAddNotion);
            this.groupBoxDict.Controls.Add(this.listBoxDict);
            this.groupBoxDict.Location = new System.Drawing.Point(765, 53);
            this.groupBoxDict.Name = "groupBoxDict";
            this.groupBoxDict.Size = new System.Drawing.Size(283, 443);
            this.groupBoxDict.TabIndex = 2;
            this.groupBoxDict.TabStop = false;
            this.groupBoxDict.Text = "Работа со словарем";
            // 
            // textBoxSearch
            // 
            this.textBoxSearch.Location = new System.Drawing.Point(139, 38);
            this.textBoxSearch.Name = "textBoxSearch";
            this.textBoxSearch.Size = new System.Drawing.Size(138, 20);
            this.textBoxSearch.TabIndex = 10;
            // 
            // buttonSortLen
            // 
            this.buttonSortLen.Image = ((System.Drawing.Image)(resources.GetObject("buttonSortLen.Image")));
            this.buttonSortLen.Location = new System.Drawing.Point(91, 179);
            this.buttonSortLen.Name = "buttonSortLen";
            this.buttonSortLen.Size = new System.Drawing.Size(42, 43);
            this.buttonSortLen.TabIndex = 9;
            this.buttonSortLen.UseVisualStyleBackColor = true;
            this.buttonSortLen.Click += new System.EventHandler(this.buttonSortLen_Click);
            // 
            // buttonSort
            // 
            this.buttonSort.Image = ((System.Drawing.Image)(resources.GetObject("buttonSort.Image")));
            this.buttonSort.Location = new System.Drawing.Point(6, 179);
            this.buttonSort.Name = "buttonSort";
            this.buttonSort.Size = new System.Drawing.Size(42, 43);
            this.buttonSort.TabIndex = 8;
            this.buttonSort.UseVisualStyleBackColor = true;
            this.buttonSort.Click += new System.EventHandler(this.buttonSort_Click);
            // 
            // buttonAddNotion
            // 
            this.buttonAddNotion.Image = ((System.Drawing.Image)(resources.GetObject("buttonAddNotion.Image")));
            this.buttonAddNotion.Location = new System.Drawing.Point(46, 239);
            this.buttonAddNotion.Name = "buttonAddNotion";
            this.buttonAddNotion.Size = new System.Drawing.Size(49, 45);
            this.buttonAddNotion.TabIndex = 6;
            this.buttonAddNotion.UseVisualStyleBackColor = true;
            // 
            // listBoxDict
            // 
            this.listBoxDict.FormattingEnabled = true;
            this.listBoxDict.Location = new System.Drawing.Point(139, 64);
            this.listBoxDict.Name = "listBoxDict";
            this.listBoxDict.Size = new System.Drawing.Size(138, 368);
            this.listBoxDict.TabIndex = 4;
            // 
            // buttonSaveCros
            // 
            this.buttonSaveCros.Image = ((System.Drawing.Image)(resources.GetObject("buttonSaveCros.Image")));
            this.buttonSaveCros.Location = new System.Drawing.Point(75, 2);
            this.buttonSaveCros.Name = "buttonSaveCros";
            this.buttonSaveCros.Size = new System.Drawing.Size(57, 45);
            this.buttonSaveCros.TabIndex = 3;
            this.buttonSaveCros.UseVisualStyleBackColor = true;
            // 
            // buttonBack
            // 
            this.buttonBack.Image = ((System.Drawing.Image)(resources.GetObject("buttonBack.Image")));
            this.buttonBack.Location = new System.Drawing.Point(12, 2);
            this.buttonBack.Name = "buttonBack";
            this.buttonBack.Size = new System.Drawing.Size(57, 45);
            this.buttonBack.TabIndex = 4;
            this.buttonBack.UseVisualStyleBackColor = true;
            this.buttonBack.Click += new System.EventHandler(this.buttonBack_Click);
            // 
            // FormHandMadeCros
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1060, 508);
            this.Controls.Add(this.buttonBack);
            this.Controls.Add(this.buttonSaveCros);
            this.Controls.Add(this.groupBoxDict);
            this.Controls.Add(this.groupBoxTasks);
            this.Controls.Add(this.TableContainer);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.Name = "FormHandMadeCros";
            this.Text = "СОЗДАНИЕ/РЕДАКТИРОВАНИЕ КРОССВОРДА";
            this.Load += new System.EventHandler(this.FormHandMadeCros_Load);
            this.groupBoxTasks.ResumeLayout(false);
            this.groupBoxTasks.PerformLayout();
            this.groupBoxDict.ResumeLayout(false);
            this.groupBoxDict.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel TableContainer;
        private System.Windows.Forms.GroupBox groupBoxTasks;
        private System.Windows.Forms.Button buttonDelNotion;
        private System.Windows.Forms.Label labelHor;
        private System.Windows.Forms.Label labelVert;
        private System.Windows.Forms.ListBox listBoxHor;
        private System.Windows.Forms.ListBox listBoxVert;
        private System.Windows.Forms.GroupBox groupBoxDict;
        private System.Windows.Forms.Button buttonSortLen;
        private System.Windows.Forms.Button buttonSort;
        private System.Windows.Forms.Button buttonAddNotion;
        private System.Windows.Forms.ListBox listBoxDict;
        private System.Windows.Forms.Button buttonSaveCros;
        private System.Windows.Forms.Button buttonBack;
        private System.Windows.Forms.TextBox textBoxSearch;
    }
}