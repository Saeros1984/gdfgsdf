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
            this.learning = new System.Windows.Forms.TabPage();
            this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
            this.label8 = new System.Windows.Forms.Label();
            this.label9 = new System.Windows.Forms.Label();
            this.label10 = new System.Windows.Forms.Label();
            this.TBegin = new System.Windows.Forms.Label();
            this.TCurrent = new System.Windows.Forms.Label();
            this.StepVar = new System.Windows.Forms.Label();
            this.TBeginNum = new System.Windows.Forms.NumericUpDown();
            this.visualButton = new System.Windows.Forms.Button();
            this.koshi = new System.Windows.Forms.Button();
            this.StartLearning = new System.Windows.Forms.Button();
            this.gradientLearningTable = new System.Windows.Forms.TableLayoutPanel();
            this.label3 = new System.Windows.Forms.Label();
            this.epochpassed = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.maxmistake = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.midmistake = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.interrupted = new System.Windows.Forms.Label();
            this.label7 = new System.Windows.Forms.Label();
            this.speed = new System.Windows.Forms.Label();
            this.speedNum = new System.Windows.Forms.NumericUpDown();
            this.multilearning = new System.Windows.Forms.TabPage();
            this.workdata = new System.Windows.Forms.TabPage();
            this.userOutput = new System.Windows.Forms.DataGridView();
            this.userInput = new System.Windows.Forms.DataGridView();
            this.label2 = new System.Windows.Forms.Label();
            this.brainList = new System.Windows.Forms.ListBox();
            this.label11 = new System.Windows.Forms.Label();
            this.label12 = new System.Windows.Forms.Label();
            this.gradientPercent = new System.Windows.Forms.Label();
            this.Pspeed = new System.Windows.Forms.Label();
            this.gradientPercentNum = new System.Windows.Forms.NumericUpDown();
            this.PspeedNum = new System.Windows.Forms.NumericUpDown();
            this.Result = new System.Windows.Forms.Label();
            this.button2 = new System.Windows.Forms.Button();
            this.networks.SuspendLayout();
            this.Brain.SuspendLayout();
            this.NetworkInfoPanel.SuspendLayout();
            this.learning.SuspendLayout();
            this.tableLayoutPanel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.TBeginNum)).BeginInit();
            this.gradientLearningTable.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.speedNum)).BeginInit();
            this.multilearning.SuspendLayout();
            this.workdata.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.userOutput)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.userInput)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gradientPercentNum)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.PspeedNum)).BeginInit();
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
            this.networks.Controls.Add(this.workdata);
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
            // learning
            // 
            this.learning.BackColor = System.Drawing.Color.WhiteSmoke;
            this.learning.Controls.Add(this.tableLayoutPanel1);
            this.learning.Controls.Add(this.visualButton);
            this.learning.Controls.Add(this.koshi);
            this.learning.Controls.Add(this.StartLearning);
            this.learning.Controls.Add(this.gradientLearningTable);
            this.learning.Location = new System.Drawing.Point(4, 22);
            this.learning.Name = "learning";
            this.learning.Padding = new System.Windows.Forms.Padding(3);
            this.learning.Size = new System.Drawing.Size(993, 354);
            this.learning.TabIndex = 2;
            this.learning.Text = "Обучение";
            // 
            // tableLayoutPanel1
            // 
            this.tableLayoutPanel1.ColumnCount = 3;
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 33.22259F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 49.83389F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 16.94352F));
            this.tableLayoutPanel1.Controls.Add(this.label8, 0, 0);
            this.tableLayoutPanel1.Controls.Add(this.label9, 0, 1);
            this.tableLayoutPanel1.Controls.Add(this.label10, 0, 2);
            this.tableLayoutPanel1.Controls.Add(this.TBegin, 1, 0);
            this.tableLayoutPanel1.Controls.Add(this.TCurrent, 1, 1);
            this.tableLayoutPanel1.Controls.Add(this.StepVar, 1, 2);
            this.tableLayoutPanel1.Controls.Add(this.TBeginNum, 2, 0);
            this.tableLayoutPanel1.Controls.Add(this.label11, 0, 3);
            this.tableLayoutPanel1.Controls.Add(this.label12, 0, 4);
            this.tableLayoutPanel1.Controls.Add(this.gradientPercent, 1, 3);
            this.tableLayoutPanel1.Controls.Add(this.Pspeed, 1, 4);
            this.tableLayoutPanel1.Controls.Add(this.gradientPercentNum, 2, 3);
            this.tableLayoutPanel1.Controls.Add(this.PspeedNum, 2, 4);
            this.tableLayoutPanel1.Location = new System.Drawing.Point(503, 40);
            this.tableLayoutPanel1.Name = "tableLayoutPanel1";
            this.tableLayoutPanel1.RowCount = 5;
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 22F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 22F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 22F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 22F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 22F));
            this.tableLayoutPanel1.Size = new System.Drawing.Size(459, 149);
            this.tableLayoutPanel1.TabIndex = 3;
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Location = new System.Drawing.Point(3, 0);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(72, 13);
            this.label8.TabIndex = 0;
            this.label8.Text = "Начальная Т";
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Location = new System.Drawing.Point(3, 22);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(62, 13);
            this.label9.TabIndex = 1;
            this.label9.Text = "Текущая Т";
            // 
            // label10
            // 
            this.label10.AutoSize = true;
            this.label10.Location = new System.Drawing.Point(3, 44);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(100, 13);
            this.label10.TabIndex = 2;
            this.label10.Text = "Вероятность шага";
            // 
            // TBegin
            // 
            this.TBegin.AutoSize = true;
            this.TBegin.Location = new System.Drawing.Point(155, 0);
            this.TBegin.Name = "TBegin";
            this.TBegin.Size = new System.Drawing.Size(41, 13);
            this.TBegin.TabIndex = 3;
            this.TBegin.Text = "label11";
            // 
            // TCurrent
            // 
            this.TCurrent.AutoSize = true;
            this.TCurrent.Location = new System.Drawing.Point(155, 22);
            this.TCurrent.Name = "TCurrent";
            this.TCurrent.Size = new System.Drawing.Size(41, 13);
            this.TCurrent.TabIndex = 4;
            this.TCurrent.Text = "label11";
            // 
            // StepVar
            // 
            this.StepVar.AutoSize = true;
            this.StepVar.Location = new System.Drawing.Point(155, 44);
            this.StepVar.Name = "StepVar";
            this.StepVar.Size = new System.Drawing.Size(41, 13);
            this.StepVar.TabIndex = 5;
            this.StepVar.Text = "label11";
            // 
            // TBeginNum
            // 
            this.TBeginNum.DecimalPlaces = 3;
            this.TBeginNum.Location = new System.Drawing.Point(383, 3);
            this.TBeginNum.Maximum = new decimal(new int[] {
            9999999,
            0,
            0,
            0});
            this.TBeginNum.Name = "TBeginNum";
            this.TBeginNum.Size = new System.Drawing.Size(49, 20);
            this.TBeginNum.TabIndex = 6;
            this.TBeginNum.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.TBeginNum_KeyPress);
            // 
            // visualButton
            // 
            this.visualButton.Location = new System.Drawing.Point(29, 251);
            this.visualButton.Name = "visualButton";
            this.visualButton.Size = new System.Drawing.Size(137, 37);
            this.visualButton.TabIndex = 2;
            this.visualButton.Text = "Визуализировать";
            this.visualButton.UseVisualStyleBackColor = true;
            this.visualButton.Click += new System.EventHandler(this.visualButton_Click);
            // 
            // koshi
            // 
            this.koshi.Location = new System.Drawing.Point(172, 294);
            this.koshi.Name = "koshi";
            this.koshi.Size = new System.Drawing.Size(137, 37);
            this.koshi.TabIndex = 1;
            this.koshi.Text = "Коши";
            this.koshi.UseVisualStyleBackColor = true;
            this.koshi.Click += new System.EventHandler(this.koshi_Click);
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
            // gradientLearningTable
            // 
            this.gradientLearningTable.ColumnCount = 3;
            this.gradientLearningTable.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 33.22259F));
            this.gradientLearningTable.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 49.83389F));
            this.gradientLearningTable.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 16.94352F));
            this.gradientLearningTable.Controls.Add(this.label3, 0, 0);
            this.gradientLearningTable.Controls.Add(this.epochpassed, 1, 0);
            this.gradientLearningTable.Controls.Add(this.label4, 0, 1);
            this.gradientLearningTable.Controls.Add(this.maxmistake, 1, 1);
            this.gradientLearningTable.Controls.Add(this.label5, 0, 2);
            this.gradientLearningTable.Controls.Add(this.midmistake, 1, 2);
            this.gradientLearningTable.Controls.Add(this.label6, 0, 3);
            this.gradientLearningTable.Controls.Add(this.interrupted, 1, 3);
            this.gradientLearningTable.Controls.Add(this.label7, 0, 4);
            this.gradientLearningTable.Controls.Add(this.speed, 1, 4);
            this.gradientLearningTable.Controls.Add(this.speedNum, 2, 4);
            this.gradientLearningTable.Location = new System.Drawing.Point(29, 40);
            this.gradientLearningTable.Name = "gradientLearningTable";
            this.gradientLearningTable.RowCount = 5;
            this.gradientLearningTable.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 22F));
            this.gradientLearningTable.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 22F));
            this.gradientLearningTable.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 22F));
            this.gradientLearningTable.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 22F));
            this.gradientLearningTable.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 22F));
            this.gradientLearningTable.Size = new System.Drawing.Size(459, 149);
            this.gradientLearningTable.TabIndex = 0;
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
            this.epochpassed.Location = new System.Drawing.Point(155, 0);
            this.epochpassed.Name = "epochpassed";
            this.epochpassed.Size = new System.Drawing.Size(35, 13);
            this.epochpassed.TabIndex = 1;
            this.epochpassed.Text = "label4";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(3, 22);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(128, 13);
            this.label4.TabIndex = 2;
            this.label4.Text = "Максимальная ошибка ";
            // 
            // maxmistake
            // 
            this.maxmistake.AutoSize = true;
            this.maxmistake.Location = new System.Drawing.Point(155, 22);
            this.maxmistake.Name = "maxmistake";
            this.maxmistake.Size = new System.Drawing.Size(35, 13);
            this.maxmistake.TabIndex = 3;
            this.maxmistake.Text = "label5";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(3, 44);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(91, 13);
            this.label5.TabIndex = 4;
            this.label5.Text = "Средняя ошибка";
            // 
            // midmistake
            // 
            this.midmistake.AutoSize = true;
            this.midmistake.Location = new System.Drawing.Point(155, 44);
            this.midmistake.Name = "midmistake";
            this.midmistake.Size = new System.Drawing.Size(35, 13);
            this.midmistake.TabIndex = 5;
            this.midmistake.Text = "label6";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(3, 66);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(116, 13);
            this.label6.TabIndex = 6;
            this.label6.Text = "Прервано по причине";
            // 
            // interrupted
            // 
            this.interrupted.AutoSize = true;
            this.interrupted.Location = new System.Drawing.Point(155, 66);
            this.interrupted.Name = "interrupted";
            this.interrupted.Size = new System.Drawing.Size(35, 13);
            this.interrupted.TabIndex = 7;
            this.interrupted.Text = "label7";
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(3, 88);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(55, 13);
            this.label7.TabIndex = 8;
            this.label7.Text = "Скорость";
            // 
            // speed
            // 
            this.speed.AutoSize = true;
            this.speed.Location = new System.Drawing.Point(155, 88);
            this.speed.Name = "speed";
            this.speed.Size = new System.Drawing.Size(35, 13);
            this.speed.TabIndex = 9;
            this.speed.Text = "label8";
            // 
            // speedNum
            // 
            this.speedNum.DecimalPlaces = 3;
            this.speedNum.Location = new System.Drawing.Point(383, 91);
            this.speedNum.Maximum = new decimal(new int[] {
            9999999,
            0,
            0,
            0});
            this.speedNum.Name = "speedNum";
            this.speedNum.Size = new System.Drawing.Size(61, 20);
            this.speedNum.TabIndex = 10;
            this.speedNum.Value = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.speedNum.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.speedNum_KeyPress);
            // 
            // multilearning
            // 
            this.multilearning.BackColor = System.Drawing.Color.WhiteSmoke;
            this.multilearning.Controls.Add(this.button2);
            this.multilearning.Controls.Add(this.Result);
            this.multilearning.Location = new System.Drawing.Point(4, 22);
            this.multilearning.Name = "multilearning";
            this.multilearning.Padding = new System.Windows.Forms.Padding(3);
            this.multilearning.Size = new System.Drawing.Size(993, 354);
            this.multilearning.TabIndex = 1;
            this.multilearning.Text = "Множественное обучение";
            // 
            // workdata
            // 
            this.workdata.Controls.Add(this.userOutput);
            this.workdata.Controls.Add(this.userInput);
            this.workdata.Location = new System.Drawing.Point(4, 22);
            this.workdata.Name = "workdata";
            this.workdata.Size = new System.Drawing.Size(993, 354);
            this.workdata.TabIndex = 3;
            this.workdata.Text = "Рабочая выборка";
            this.workdata.UseVisualStyleBackColor = true;
            // 
            // userOutput
            // 
            this.userOutput.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.userOutput.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.userOutput.Location = new System.Drawing.Point(20, 166);
            this.userOutput.Name = "userOutput";
            this.userOutput.Size = new System.Drawing.Size(949, 103);
            this.userOutput.TabIndex = 0;
            // 
            // userInput
            // 
            this.userInput.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.userInput.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.userInput.Location = new System.Drawing.Point(20, 15);
            this.userInput.Name = "userInput";
            this.userInput.Size = new System.Drawing.Size(949, 103);
            this.userInput.TabIndex = 0;
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
            // label11
            // 
            this.label11.AutoSize = true;
            this.label11.Location = new System.Drawing.Point(3, 66);
            this.label11.Name = "label11";
            this.label11.Size = new System.Drawing.Size(105, 13);
            this.label11.TabIndex = 7;
            this.label11.Text = "Процент градиента";
            // 
            // label12
            // 
            this.label12.AutoSize = true;
            this.label12.Location = new System.Drawing.Point(3, 88);
            this.label12.Name = "label12";
            this.label12.Size = new System.Drawing.Size(83, 13);
            this.label12.TabIndex = 8;
            this.label12.Text = "Скорость стох.";
            // 
            // gradientPercent
            // 
            this.gradientPercent.AutoSize = true;
            this.gradientPercent.Location = new System.Drawing.Point(155, 66);
            this.gradientPercent.Name = "gradientPercent";
            this.gradientPercent.Size = new System.Drawing.Size(41, 13);
            this.gradientPercent.TabIndex = 9;
            this.gradientPercent.Text = "label13";
            // 
            // Pspeed
            // 
            this.Pspeed.AutoSize = true;
            this.Pspeed.Location = new System.Drawing.Point(155, 88);
            this.Pspeed.Name = "Pspeed";
            this.Pspeed.Size = new System.Drawing.Size(41, 13);
            this.Pspeed.TabIndex = 10;
            this.Pspeed.Text = "label13";
            // 
            // gradientPercentNum
            // 
            this.gradientPercentNum.DecimalPlaces = 3;
            this.gradientPercentNum.Location = new System.Drawing.Point(383, 69);
            this.gradientPercentNum.Name = "gradientPercentNum";
            this.gradientPercentNum.Size = new System.Drawing.Size(49, 20);
            this.gradientPercentNum.TabIndex = 11;
            this.gradientPercentNum.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.gradientPercentNum_KeyPress);
            // 
            // PspeedNum
            // 
            this.PspeedNum.DecimalPlaces = 3;
            this.PspeedNum.Location = new System.Drawing.Point(383, 91);
            this.PspeedNum.Name = "PspeedNum";
            this.PspeedNum.Size = new System.Drawing.Size(50, 20);
            this.PspeedNum.TabIndex = 12;
            this.PspeedNum.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.PspeedNum_KeyPress);
            // 
            // Result
            // 
            this.Result.AutoSize = true;
            this.Result.Location = new System.Drawing.Point(42, 35);
            this.Result.Name = "Result";
            this.Result.Size = new System.Drawing.Size(41, 13);
            this.Result.TabIndex = 0;
            this.Result.Text = "label13";
            // 
            // button2
            // 
            this.button2.Location = new System.Drawing.Point(197, 286);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(221, 48);
            this.button2.TabIndex = 1;
            this.button2.Text = "button2";
            this.button2.UseVisualStyleBackColor = true;
            this.button2.Click += new System.EventHandler(this.button2_Click);
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
            ((System.ComponentModel.ISupportInitialize)(this.TBeginNum)).EndInit();
            this.gradientLearningTable.ResumeLayout(false);
            this.gradientLearningTable.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.speedNum)).EndInit();
            this.multilearning.ResumeLayout(false);
            this.multilearning.PerformLayout();
            this.workdata.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.userOutput)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.userInput)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gradientPercentNum)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.PspeedNum)).EndInit();
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
        private System.Windows.Forms.TableLayoutPanel gradientLearningTable;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label epochpassed;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label maxmistake;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label midmistake;
        private System.Windows.Forms.Button StartLearning;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label interrupted;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Label speed;
        private System.Windows.Forms.NumericUpDown speedNum;
        private System.Windows.Forms.Button visualButton;
        private System.Windows.Forms.TabPage workdata;
        private System.Windows.Forms.DataGridView userInput;
        private System.Windows.Forms.DataGridView userOutput;
        private System.Windows.Forms.Button koshi;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.Label TBegin;
        private System.Windows.Forms.Label TCurrent;
        private System.Windows.Forms.Label StepVar;
        private System.Windows.Forms.NumericUpDown TBeginNum;
        private System.Windows.Forms.Label label11;
        private System.Windows.Forms.Label label12;
        private System.Windows.Forms.Label gradientPercent;
        private System.Windows.Forms.Label Pspeed;
        private System.Windows.Forms.NumericUpDown gradientPercentNum;
        private System.Windows.Forms.NumericUpDown PspeedNum;
        private System.Windows.Forms.Label Result;
        private System.Windows.Forms.Button button2;
    }
}

