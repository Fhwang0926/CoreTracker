using System;

namespace CoreTracker
{
    partial class Form1
    {
        /// <summary>
        /// 필수 디자이너 변수입니다.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// 사용 중인 모든 리소스를 정리합니다.
        /// </summary>
        /// <param name="disposing">관리되는 리소스를 삭제해야 하면 true이고, 그렇지 않으면 false입니다.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form 디자이너에서 생성한 코드

        /// <summary>
        /// 디자이너 지원에 필요한 메서드입니다. 
        /// 이 메서드의 내용을 코드 편집기로 수정하지 마세요.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Form1));
            this.l_core = new System.Windows.Forms.Label();
            this.l_th = new System.Windows.Forms.Label();
            this.l_th_value = new System.Windows.Forms.Label();
            this.l_core_value = new System.Windows.Forms.Label();
            this.btn_tray = new System.Windows.Forms.Button();
            this.ti_main = new System.Windows.Forms.NotifyIcon(this.components);
            this.ti_menu = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.groupBox3 = new System.Windows.Forms.GroupBox();
            this.rb_slow = new System.Windows.Forms.RadioButton();
            this.rb_normal = new System.Windows.Forms.RadioButton();
            this.rb_fast = new System.Windows.Forms.RadioButton();
            this.ch_auto_start = new System.Windows.Forms.CheckBox();
            this.ch_auto_bugreport = new System.Windows.Forms.CheckBox();
            this.ch_auto_update = new System.Windows.Forms.CheckBox();
            this.label2 = new System.Windows.Forms.Label();
            this.linkLabel1 = new System.Windows.Forms.LinkLabel();
            this.groupBox4 = new System.Windows.Forms.GroupBox();
            this.label1 = new System.Windows.Forms.Label();
            this.pic_status = new System.Windows.Forms.PictureBox();
            this.groupBox2.SuspendLayout();
            this.groupBox3.SuspendLayout();
            this.groupBox4.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pic_status)).BeginInit();
            this.SuspendLayout();
            // 
            // l_core
            // 
            this.l_core.AutoSize = true;
            this.l_core.Font = new System.Drawing.Font("굴림", 28F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.l_core.ForeColor = System.Drawing.Color.White;
            this.l_core.Location = new System.Drawing.Point(20, 35);
            this.l_core.Name = "l_core";
            this.l_core.Size = new System.Drawing.Size(218, 38);
            this.l_core.TabIndex = 0;
            this.l_core.Text = "Core      : ";
            // 
            // l_th
            // 
            this.l_th.AutoSize = true;
            this.l_th.Font = new System.Drawing.Font("굴림", 28F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.l_th.ForeColor = System.Drawing.Color.White;
            this.l_th.Location = new System.Drawing.Point(20, 96);
            this.l_th.Name = "l_th";
            this.l_th.Size = new System.Drawing.Size(216, 38);
            this.l_th.TabIndex = 1;
            this.l_th.Text = "Thread   : ";
            // 
            // l_th_value
            // 
            this.l_th_value.AutoSize = true;
            this.l_th_value.Font = new System.Drawing.Font("굴림", 28F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.l_th_value.ForeColor = System.Drawing.Color.White;
            this.l_th_value.Location = new System.Drawing.Point(216, 96);
            this.l_th_value.Name = "l_th_value";
            this.l_th_value.Size = new System.Drawing.Size(39, 38);
            this.l_th_value.TabIndex = 3;
            this.l_th_value.Text = "0";
            // 
            // l_core_value
            // 
            this.l_core_value.AutoSize = true;
            this.l_core_value.Font = new System.Drawing.Font("굴림", 28F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.l_core_value.ForeColor = System.Drawing.Color.White;
            this.l_core_value.Location = new System.Drawing.Point(216, 35);
            this.l_core_value.Name = "l_core_value";
            this.l_core_value.Size = new System.Drawing.Size(39, 38);
            this.l_core_value.TabIndex = 2;
            this.l_core_value.Text = "0";
            // 
            // btn_tray
            // 
            this.btn_tray.BackColor = System.Drawing.Color.DodgerBlue;
            this.btn_tray.FlatAppearance.BorderSize = 0;
            this.btn_tray.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btn_tray.Font = new System.Drawing.Font("굴림", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.btn_tray.ForeColor = System.Drawing.Color.White;
            this.btn_tray.Location = new System.Drawing.Point(12, 606);
            this.btn_tray.Name = "btn_tray";
            this.btn_tray.Size = new System.Drawing.Size(344, 44);
            this.btn_tray.TabIndex = 4;
            this.btn_tray.Text = "Start!! :D";
            this.btn_tray.UseVisualStyleBackColor = false;
            this.btn_tray.Click += new System.EventHandler(this.btn_tray_Click);
            // 
            // ti_main
            // 
            this.ti_main.Icon = ((System.Drawing.Icon)(resources.GetObject("ti_main.Icon")));
            this.ti_main.Text = "notifyIcon1";
            this.ti_main.Visible = true;
            this.ti_main.MouseDoubleClick += new System.Windows.Forms.MouseEventHandler(this.notifyIcon1_MouseDoubleClick);
            // 
            // ti_menu
            // 
            this.ti_menu.Name = "ti_menu";
            this.ti_menu.Size = new System.Drawing.Size(61, 4);
            // 
            // groupBox1
            // 
            this.groupBox1.Font = new System.Drawing.Font("굴림", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.groupBox1.ForeColor = System.Drawing.Color.White;
            this.groupBox1.Location = new System.Drawing.Point(12, 12);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(344, 144);
            this.groupBox1.TabIndex = 5;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Information of System";
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.groupBox3);
            this.groupBox2.Controls.Add(this.ch_auto_start);
            this.groupBox2.Controls.Add(this.ch_auto_bugreport);
            this.groupBox2.Controls.Add(this.ch_auto_update);
            this.groupBox2.Font = new System.Drawing.Font("굴림", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.groupBox2.ForeColor = System.Drawing.Color.White;
            this.groupBox2.Location = new System.Drawing.Point(12, 162);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(344, 254);
            this.groupBox2.TabIndex = 6;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "Setting";
            // 
            // groupBox3
            // 
            this.groupBox3.Controls.Add(this.rb_slow);
            this.groupBox3.Controls.Add(this.rb_normal);
            this.groupBox3.Controls.Add(this.rb_fast);
            this.groupBox3.Font = new System.Drawing.Font("굴림", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.groupBox3.ForeColor = System.Drawing.Color.White;
            this.groupBox3.Location = new System.Drawing.Point(20, 168);
            this.groupBox3.Name = "groupBox3";
            this.groupBox3.Size = new System.Drawing.Size(305, 65);
            this.groupBox3.TabIndex = 7;
            this.groupBox3.TabStop = false;
            this.groupBox3.Text = "Refresh speed";
            // 
            // rb_slow
            // 
            this.rb_slow.AutoSize = true;
            this.rb_slow.Location = new System.Drawing.Point(200, 29);
            this.rb_slow.Name = "rb_slow";
            this.rb_slow.Size = new System.Drawing.Size(55, 16);
            this.rb_slow.TabIndex = 2;
            this.rb_slow.Text = "Slow";
            this.rb_slow.UseVisualStyleBackColor = true;
            // 
            // rb_normal
            // 
            this.rb_normal.AutoSize = true;
            this.rb_normal.Location = new System.Drawing.Point(109, 29);
            this.rb_normal.Name = "rb_normal";
            this.rb_normal.Size = new System.Drawing.Size(70, 16);
            this.rb_normal.TabIndex = 1;
            this.rb_normal.TabStop = true;
            this.rb_normal.Text = "Nomarl";
            this.rb_normal.UseVisualStyleBackColor = true;
            // 
            // rb_fast
            // 
            this.rb_fast.AutoSize = true;
            this.rb_fast.Checked = true;
            this.rb_fast.Location = new System.Drawing.Point(11, 29);
            this.rb_fast.Name = "rb_fast";
            this.rb_fast.Size = new System.Drawing.Size(51, 16);
            this.rb_fast.TabIndex = 0;
            this.rb_fast.TabStop = true;
            this.rb_fast.Text = "Fast";
            this.rb_fast.UseVisualStyleBackColor = true;
            // 
            // ch_auto_start
            // 
            this.ch_auto_start.AutoSize = true;
            this.ch_auto_start.Location = new System.Drawing.Point(20, 105);
            this.ch_auto_start.Name = "ch_auto_start";
            this.ch_auto_start.Size = new System.Drawing.Size(88, 16);
            this.ch_auto_start.TabIndex = 2;
            this.ch_auto_start.Text = "Auto Start";
            this.ch_auto_start.UseVisualStyleBackColor = true;
            this.ch_auto_start.CheckedChanged += new System.EventHandler(this.ch_auto_start_CheckedChanged);
            // 
            // ch_auto_bugreport
            // 
            this.ch_auto_bugreport.AutoSize = true;
            this.ch_auto_bugreport.Location = new System.Drawing.Point(20, 69);
            this.ch_auto_bugreport.Name = "ch_auto_bugreport";
            this.ch_auto_bugreport.Size = new System.Drawing.Size(130, 16);
            this.ch_auto_bugreport.TabIndex = 1;
            this.ch_auto_bugreport.Text = "Auto Bug Report";
            this.ch_auto_bugreport.UseVisualStyleBackColor = true;
            // 
            // ch_auto_update
            // 
            this.ch_auto_update.AutoSize = true;
            this.ch_auto_update.Location = new System.Drawing.Point(20, 34);
            this.ch_auto_update.Name = "ch_auto_update";
            this.ch_auto_update.Size = new System.Drawing.Size(103, 16);
            this.ch_auto_update.TabIndex = 0;
            this.ch_auto_update.Text = "Auto Update";
            this.ch_auto_update.UseVisualStyleBackColor = true;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(78, 28);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(196, 48);
            this.label2.TabIndex = 8;
            this.label2.Text = "Auther : hdh0926@naver.com\r\n\r\nWelcome your feedback.\r\n\r\n";
            // 
            // linkLabel1
            // 
            this.linkLabel1.AutoSize = true;
            this.linkLabel1.LinkColor = System.Drawing.SystemColors.Info;
            this.linkLabel1.Location = new System.Drawing.Point(78, 85);
            this.linkLabel1.Name = "linkLabel1";
            this.linkLabel1.Size = new System.Drawing.Size(172, 12);
            this.linkLabel1.TabIndex = 9;
            this.linkLabel1.TabStop = true;
            this.linkLabel1.Text = "CoreTracker@github.com";
            this.linkLabel1.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.linkLabel1_LinkClicked);
            // 
            // groupBox4
            // 
            this.groupBox4.Controls.Add(this.label2);
            this.groupBox4.Controls.Add(this.linkLabel1);
            this.groupBox4.Font = new System.Drawing.Font("굴림", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.groupBox4.ForeColor = System.Drawing.Color.White;
            this.groupBox4.Location = new System.Drawing.Point(12, 422);
            this.groupBox4.Name = "groupBox4";
            this.groupBox4.Size = new System.Drawing.Size(344, 119);
            this.groupBox4.TabIndex = 8;
            this.groupBox4.TabStop = false;
            this.groupBox4.Text = "Refresh speed";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("굴림", 20F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.label1.ForeColor = System.Drawing.Color.White;
            this.label1.Location = new System.Drawing.Point(86, 558);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(125, 27);
            this.label1.TabIndex = 9;
            this.label1.Text = "Status : ";
            // 
            // pic_status
            // 
            this.pic_status.Location = new System.Drawing.Point(218, 558);
            this.pic_status.Name = "pic_status";
            this.pic_status.Size = new System.Drawing.Size(25, 27);
            this.pic_status.TabIndex = 10;
            this.pic_status.TabStop = false;
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.WindowFrame;
            this.ClientSize = new System.Drawing.Size(368, 662);
            this.Controls.Add(this.pic_status);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.btn_tray);
            this.Controls.Add(this.l_th_value);
            this.Controls.Add(this.l_core_value);
            this.Controls.Add(this.l_th);
            this.Controls.Add(this.l_core);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.groupBox4);
            this.ForeColor = System.Drawing.Color.Black;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "Form1";
            this.Text = "CoreTracker";
            this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.Form1_FormClosing);
            this.Load += new System.EventHandler(this.Form1_Load);
            this.Resize += new System.EventHandler(this.Form1_Resize);
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            this.groupBox3.ResumeLayout(false);
            this.groupBox3.PerformLayout();
            this.groupBox4.ResumeLayout(false);
            this.groupBox4.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pic_status)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label l_core;
        private System.Windows.Forms.Label l_th;
        private System.Windows.Forms.Label l_th_value;
        private System.Windows.Forms.Label l_core_value;
        private System.Windows.Forms.Button btn_tray;
        private System.Windows.Forms.NotifyIcon ti_main;
        private System.Windows.Forms.ContextMenuStrip ti_menu;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.GroupBox groupBox3;
        private System.Windows.Forms.RadioButton rb_slow;
        private System.Windows.Forms.RadioButton rb_normal;
        private System.Windows.Forms.RadioButton rb_fast;
        private System.Windows.Forms.CheckBox ch_auto_start;
        private System.Windows.Forms.CheckBox ch_auto_bugreport;
        private System.Windows.Forms.CheckBox ch_auto_update;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.LinkLabel linkLabel1;
        private System.Windows.Forms.GroupBox groupBox4;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.PictureBox pic_status;
    }
}

