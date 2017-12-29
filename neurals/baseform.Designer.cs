namespace neurals
{
    partial class Baseform
    {
        /// <summary>
        /// Требуется переменная конструктора.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Освободить все используемые ресурсы.
        /// </summary>
        /// <param name="disposing">истинно, если управляемый ресурс должен быть удален; иначе ложно.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Код, автоматически созданный конструктором форм Windows

        /// <summary>
        /// Обязательный метод для поддержки конструктора - не изменяйте
        /// содержимое данного метода при помощи редактора кода.
        /// </summary>
        private void InitializeComponent()
        {
            this.button1 = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.networks = new System.Windows.Forms.TabControl();
            this.Brain = new System.Windows.Forms.TabPage();
            this.NetworkInfoPanel = new System.Windows.Forms.Panel();
            this.networkName = new System.Windows.Forms.Label();
            this.networkDescr = new System.Windows.Forms.Label();
            this.NetwokName = new System.Windows.Forms.Label();
            this.multilearning = new System.Windows.Forms.TabPage();
            this.label2 = new System.Windows.Forms.Label();
            this.brainList = new System.Windows.Forms.ListBox();
            this.learning = new System.Windows.Forms.TabPage();
            this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
            this.label3 = new System.Windows.Forms.Label();
            this.epochpassed = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.maxmistake = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.midmistake = new System.Windows.Forms.Label();
            this.StartLearning = new System.Windows.Forms.Button();
            this.label6 = new System.Windows.Forms.Label();
            this.interrupted = new System.Windows.Forms.Label();
            this.networks.SuspendLayout();
            this.Brain.SuspendLayout();
            this.NetworkInfoPanel.SuspendLayout();
            this.learning.SuspendLayout();
            this.tableLayoutPanel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // button1
            // 
            this.button1.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.button1.Location = new System.Drawing.Point(364, 441);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(214, 85);
            this.button1.TabIndex = 0;
            this.button1.Text = "button1";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(152, 496);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(35, 13);
            this.label1.TabIndex = 1;
            this.label1.Text = "label1";
            // 
            // networks
            // 
            this.networks.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
            this.networks.Controls.Add(this.Brain);
            this.networks.Controls.Add(this.learning);
            this.networks.Controls.Add(this.multilearning);
            this.networks.Location = new System.Drawing.Point(155, 44);
            this.networks.Name = "networks";
            this.networks.SelectedIndex = 0;
            this.networks.Size = new System.Drawing.Size(1001, 380);
            this.networks.TabIndex = 2;
            // 
            // Brain
            // 
            this.Brain.BackColor = System.Drawing.Color.WhiteSmoke;
            this.Brain.Controls.Add(this.NetworkInfoPanel);
            this.Brain.Location = new System.Drawing.Point(4, 22);
            this.Brain.Name = "Brain";
            this.Brain.Padding = new System.Windows.Forms.Padding(3);
            this.Brain.Size = new System.Drawing.Size(993, 354);
            this.Brain.TabIndex = 0;
            this.Brain.Text = "Сеть";
            // 
            // NetworkInfoPanel
            // 
            this.NetworkInfoPanel.Controls.Add(this.networkName);
            this.NetworkInfoPanel.Controls.Add(this.networkDescr);
            this.NetworkInfoPanel.Controls.Add(this.NetwokName);
            this.NetworkInfoPanel.Location = new System.Drawing.Point(12, 12);
            this.NetworkInfoPanel.Name = "NetworkInfoPanel";
            this.NetworkInfoPanel.Size = new System.Drawing.Size(966, 326);
            this.NetworkInfoPanel.TabIndex = 0;
            // 
            // networkName
            // 
            this.networkName.AutoSize = true;
            this.networkName.Location = new System.Drawing.Point(14, 19);
            this.networkName.Name = "networkName";
            this.networkName.Size = new System.Drawing.Size(35, 13);
            this.networkName.TabIndex = 2;
            this.networkName.Text = "label3";
            // 
            // networkDescr
            // 
            this.networkDescr.AutoSize = true;
            this.networkDescr.Location = new System.Drawing.Point(14, 43);
            this.networkDescr.Name = "networkDescr";
            this.networkDescr.Size = new System.Drawing.Size(35, 13);
            this.networkDescr.TabIndex = 1;
            this.networkDescr.Text = "label3";
            // 
            // NetwokName
            // 
            this.NetwokName.AutoSize = true;
            this.NetwokName.Location = new System.Drawing.Point(13, 12);
            this.NetwokName.Name = "NetwokName";
            this.NetwokName.Size = new System.Drawing.Size(0, 13);
            this.NetwokName.TabIndex = 0;
            // 
            // multilearning
            // 
            this.multilearning.Location = new System.Drawing.Point(4, 22);
            this.multilearning.Name = "multilearning";
            this.multilearning.Padding = new System.Windows.Forms.Padding(3);
            this.multilearning.Size = new System.Drawing.Size(993, 354);
            this.multilearning.TabIndex = 1;
            this.multilearning.Text = "Множественное обучение";
            this.multilearning.UseVisualStyleBackColor = true;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(1, 77);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(98, 13);
            this.label2.TabIndex = 1;
            this.label2.Text = "Все текущие сети";
            // 
            // brainList
            // 
            this.brainList.FormattingEnabled = true;
            this.brainList.Location = new System.Drawing.Point(4, 104);
            this.brainList.Name = "brainList";
            this.brainList.Size = new System.Drawing.Size(145, 316);
            this.brainList.TabIndex = 0;
            this.brainList.SelectedIndexChanged += new System.EventHandler(this.brainList_SelectedIndexChanged);
            // 
            // learning
            // 
            this.learning.BackColor = System.Drawing.Color.WhiteSmoke;
            this.learning.Controls.Add(this.StartLearning);
            this.learning.Controls.Add(this.tableLayoutPanel1);
            this.learning.Location = new System.Drawing.Point(4, 22);
            this.learning.Name = "learning";
            this.learning.Padding = new System.Windows.Forms.Padding(3);
            this.learning.Size = new System.Drawing.Size(993, 354);
            this.learning.TabIndex = 2;
            this.learning.Text = "Обучение";
            // 
            // tableLayoutPanel1
            // 
            this.tableLayoutPanel1.ColumnCount = 2;
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel1.Controls.Add(this.label3, 0, 0);
            this.tableLayoutPanel1.Controls.Add(this.epochpassed, 1, 0);
            this.tableLayoutPanel1.Controls.Add(this.label4, 0, 1);
            this.tableLayoutPanel1.Controls.Add(this.maxmistake, 1, 1);
            this.tableLayoutPanel1.Controls.Add(this.label5, 0, 2);
            this.tableLayoutPanel1.Controls.Add(this.midmistake, 1, 2);
            this.tableLayoutPanel1.Controls.Add(this.label6, 0, 3);
            this.tableLayoutPanel1.Controls.Add(this.interrupted, 1, 3);
            this.tableLayoutPanel1.Location = new System.Drawing.Point(29, 40);
            this.tableLayoutPanel1.Name = "tableLayoutPanel1";
            this.tableLayoutPanel1.RowCount = 4;
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 25F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 25F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 25F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 25F));
            this.tableLayoutPanel1.Size = new System.Drawing.Size(360, 126);
            this.tableLayoutPanel1.TabIndex = 0;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(3, 0);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(75, 13);
            this.label3.TabIndex = 0;
            this.label3.Text = "Эпох прошло:";
            // 
            // epochpassed
            // 
            this.epochpassed.AutoSize = true;
            this.epochpassed.Location = new System.Drawing.Point(183, 0);
            this.epochpassed.Name = "epochpassed";
            this.epochpassed.Size = new System.Drawing.Size(35, 13);
            this.epochpassed.TabIndex = 1;
            this.epochpassed.Text = "label4";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(3, 31);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(128, 13);
            this.label4.TabIndex = 2;
            this.label4.Text = "Максимальная ошибка ";
            // 
            // maxmistake
            // 
            this.maxmistake.AutoSize = true;
            this.maxmistake.Location = new System.Drawing.Point(183, 31);
            this.maxmistake.Name = "maxmistake";
            this.maxmistake.Size = new System.Drawing.Size(35, 13);
            this.maxmistake.TabIndex = 3;
            this.maxmistake.Text = "label5";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(3, 62);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(91, 13);
            this.label5.TabIndex = 4;
            this.label5.Text = "Средняя ошибка";
            // 
            // midmistake
            // 
            this.midmistake.AutoSize = true;
            this.midmistake.Location = new System.Drawing.Point(183, 62);
            this.midmistake.Name = "midmistake";
            this.midmistake.Size = new System.Drawing.Size(35, 13);
            this.midmistake.TabIndex = 5;
            this.midmistake.Text = "label6";
            // 
            // StartLearning
            // 
            this.StartLearning.Location = new System.Drawing.Point(29, 294);
            this.StartLearning.Name = "StartLearning";
            this.StartLearning.Size = new System.Drawing.Size(137, 37);
            this.StartLearning.TabIndex = 1;
            this.StartLearning.Text = "Начать обучение";
            this.StartLearning.UseVisualStyleBackColor = true;
            this.StartLearning.Click += new System.EventHandler(this.StartLearning_Click);
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(3, 93);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(116, 13);
            this.label6.TabIndex = 6;
            this.label6.Text = "Прервано по причине";
            // 
            // interrupted
            // 
            this.interrupted.AutoSize = true;
            this.interrupted.Location = new System.Drawing.Point(183, 93);
            this.interrupted.Name = "interrupted";
            this.interrupted.Size = new System.Drawing.Size(35, 13);
            this.interrupted.TabIndex = 7;
            this.interrupted.Text = "label7";
            // 
            // Baseform
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1168, 623);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.brainList);
            this.Controls.Add(this.networks);
            this.Controls.Add(this.button1);
            this.Name = "Baseform";
            this.Text = "Главное окно";
            this.networks.ResumeLayout(false);
            this.Brain.ResumeLayout(false);
            this.NetworkInfoPanel.ResumeLayout(false);
            this.NetworkInfoPanel.PerformLayout();
            this.learning.ResumeLayout(false);
            this.tableLayoutPanel1.ResumeLayout(false);
            this.tableLayoutPanel1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button button1;
        public System.Windows.Forms.Label label1;
        private System.Windows.Forms.TabControl networks;
        private System.Windows.Forms.TabPage Brain;
        private System.Windows.Forms.TabPage multilearning;
        private System.Windows.Forms.ListBox brainList;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Panel NetworkInfoPanel;
        private System.Windows.Forms.Label networkDescr;
        private System.Windows.Forms.Label NetwokName;
        private System.Windows.Forms.Label networkName;
        private System.Windows.Forms.TabPage learning;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label epochpassed;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label maxmistake;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label midmistake;
        private System.Windows.Forms.Button StartLearning;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label interrupted;
    }
}

