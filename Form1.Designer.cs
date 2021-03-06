﻿namespace CoreTracker
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
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.chk_disable_alert = new System.Windows.Forms.CheckBox();
            this.ch_graphic_temperature = new System.Windows.Forms.CheckBox();
            this.ch_ram_temperature = new System.Windows.Forms.CheckBox();
            this.ch_board_temperature = new System.Windows.Forms.CheckBox();
            this.ch_cpu_temperature = new System.Windows.Forms.CheckBox();
            this.groupBox3 = new System.Windows.Forms.GroupBox();
            this.rb_slow = new System.Windows.Forms.RadioButton();
            this.rb_normal = new System.Windows.Forms.RadioButton();
            this.rb_fast = new System.Windows.Forms.RadioButton();
            this.ch_auto_start = new System.Windows.Forms.CheckBox();
            this.ch_trayicon_setting = new System.Windows.Forms.CheckBox();
            this.ch_auto_update = new System.Windows.Forms.CheckBox();
            this.label2 = new System.Windows.Forms.Label();
            this.linkLabel1 = new System.Windows.Forms.LinkLabel();
            this.groupBox4 = new System.Windows.Forms.GroupBox();
            this.label1 = new System.Windows.Forms.Label();
            this.pic_status = new System.Windows.Forms.PictureBox();
            this.l_close = new System.Windows.Forms.Label();
            this.l_hide = new System.Windows.Forms.Label();
            this.l_version = new System.Windows.Forms.Label();
            this.btn_notch = new System.Windows.Forms.Button();
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
            this.l_core.Location = new System.Drawing.Point(20, 72);
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
            this.l_th.Location = new System.Drawing.Point(20, 133);
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
            this.l_th_value.Location = new System.Drawing.Point(216, 133);
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
            this.l_core_value.Location = new System.Drawing.Point(216, 72);
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
            this.btn_tray.Location = new System.Drawing.Point(12, 699);
            this.btn_tray.Name = "btn_tray";
            this.btn_tray.Size = new System.Drawing.Size(344, 44);
            this.btn_tray.TabIndex = 4;
            this.btn_tray.Text = "Start!! :D";
            this.btn_tray.UseVisualStyleBackColor = false;
            this.btn_tray.Click += new System.EventHandler(this.btn_tray_Click);
            // 
            // ti_main
            // 
            this.ti_main.BalloonTipIcon = System.Windows.Forms.ToolTipIcon.Info;
            this.ti_main.BalloonTipText = "CoreTracker";
            this.ti_main.BalloonTipTitle = "Core Status Tracker";
            this.ti_main.Icon = ((System.Drawing.Icon)(resources.GetObject("ti_main.Icon")));
            this.ti_main.Text = "CoreTracker";
            this.ti_main.MouseDoubleClick += new System.Windows.Forms.MouseEventHandler(this.notifyIcon1_MouseDoubleClick);
            // 
            // groupBox1
            // 
            this.groupBox1.Font = new System.Drawing.Font("굴림", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.groupBox1.ForeColor = System.Drawing.Color.White;
            this.groupBox1.Location = new System.Drawing.Point(12, 46);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(344, 144);
            this.groupBox1.TabIndex = 5;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Information of System";
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.chk_disable_alert);
            this.groupBox2.Controls.Add(this.ch_graphic_temperature);
            this.groupBox2.Controls.Add(this.ch_ram_temperature);
            this.groupBox2.Controls.Add(this.ch_board_temperature);
            this.groupBox2.Controls.Add(this.ch_cpu_temperature);
            this.groupBox2.Controls.Add(this.groupBox3);
            this.groupBox2.Controls.Add(this.ch_auto_start);
            this.groupBox2.Controls.Add(this.ch_trayicon_setting);
            this.groupBox2.Controls.Add(this.ch_auto_update);
            this.groupBox2.Font = new System.Drawing.Font("굴림", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.groupBox2.ForeColor = System.Drawing.Color.White;
            this.groupBox2.Location = new System.Drawing.Point(12, 196);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(344, 299);
            this.groupBox2.TabIndex = 6;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "Setting";
            // 
            // chk_disable_alert
            // 
            this.chk_disable_alert.AutoSize = true;
            this.chk_disable_alert.Location = new System.Drawing.Point(172, 160);
            this.chk_disable_alert.Name = "chk_disable_alert";
            this.chk_disable_alert.Size = new System.Drawing.Size(154, 16);
            this.chk_disable_alert.TabIndex = 12;
            this.chk_disable_alert.Text = "Disabled Busy Alert";
            this.chk_disable_alert.UseVisualStyleBackColor = true;
            this.chk_disable_alert.CheckedChanged += new System.EventHandler(this.chk_disable_alert_CheckedChanged);
            // 
            // ch_graphic_temperature
            // 
            this.ch_graphic_temperature.AutoSize = true;
            this.ch_graphic_temperature.Location = new System.Drawing.Point(172, 117);
            this.ch_graphic_temperature.Name = "ch_graphic_temperature";
            this.ch_graphic_temperature.Size = new System.Drawing.Size(158, 16);
            this.ch_graphic_temperature.TabIndex = 11;
            this.ch_graphic_temperature.Text = "Graphic temperature";
            this.ch_graphic_temperature.UseVisualStyleBackColor = true;
            this.ch_graphic_temperature.CheckedChanged += new System.EventHandler(this.ch_graphic_temperature_CheckedChanged);
            // 
            // ch_ram_temperature
            // 
            this.ch_ram_temperature.AutoSize = true;
            this.ch_ram_temperature.Location = new System.Drawing.Point(20, 117);
            this.ch_ram_temperature.Name = "ch_ram_temperature";
            this.ch_ram_temperature.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.ch_ram_temperature.Size = new System.Drawing.Size(100, 16);
            this.ch_ram_temperature.TabIndex = 10;
            this.ch_ram_temperature.Text = "RAM Usage";
            this.ch_ram_temperature.UseVisualStyleBackColor = true;
            this.ch_ram_temperature.CheckedChanged += new System.EventHandler(this.ch_ram_temperature_CheckedChanged);
            // 
            // ch_board_temperature
            // 
            this.ch_board_temperature.AutoSize = true;
            this.ch_board_temperature.Location = new System.Drawing.Point(172, 73);
            this.ch_board_temperature.Name = "ch_board_temperature";
            this.ch_board_temperature.Size = new System.Drawing.Size(145, 16);
            this.ch_board_temperature.TabIndex = 9;
            this.ch_board_temperature.Text = "Board temperature";
            this.ch_board_temperature.UseVisualStyleBackColor = true;
            this.ch_board_temperature.CheckedChanged += new System.EventHandler(this.ch_board_temperature_CheckedChanged);
            // 
            // ch_cpu_temperature
            // 
            this.ch_cpu_temperature.AutoSize = true;
            this.ch_cpu_temperature.Location = new System.Drawing.Point(20, 73);
            this.ch_cpu_temperature.Name = "ch_cpu_temperature";
            this.ch_cpu_temperature.Size = new System.Drawing.Size(140, 16);
            this.ch_cpu_temperature.TabIndex = 8;
            this.ch_cpu_temperature.Text = "CPU Temperature";
            this.ch_cpu_temperature.UseVisualStyleBackColor = true;
            this.ch_cpu_temperature.CheckedChanged += new System.EventHandler(this.ch_cpu_temperature_CheckedChanged);
            // 
            // groupBox3
            // 
            this.groupBox3.Controls.Add(this.rb_slow);
            this.groupBox3.Controls.Add(this.rb_normal);
            this.groupBox3.Controls.Add(this.rb_fast);
            this.groupBox3.Font = new System.Drawing.Font("굴림", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.groupBox3.ForeColor = System.Drawing.Color.White;
            this.groupBox3.Location = new System.Drawing.Point(20, 200);
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
            this.rb_normal.Text = "Normal";
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
            this.ch_auto_start.Location = new System.Drawing.Point(172, 34);
            this.ch_auto_start.Name = "ch_auto_start";
            this.ch_auto_start.Size = new System.Drawing.Size(88, 16);
            this.ch_auto_start.TabIndex = 2;
            this.ch_auto_start.Text = "Auto Start";
            this.ch_auto_start.UseVisualStyleBackColor = true;
            this.ch_auto_start.CheckedChanged += new System.EventHandler(this.ch_auto_start_CheckedChanged);
            // 
            // ch_trayicon_setting
            // 
            this.ch_trayicon_setting.AutoSize = true;
            this.ch_trayicon_setting.Location = new System.Drawing.Point(20, 160);
            this.ch_trayicon_setting.Name = "ch_trayicon_setting";
            this.ch_trayicon_setting.Size = new System.Drawing.Size(123, 16);
            this.ch_trayicon_setting.TabIndex = 1;
            this.ch_trayicon_setting.Text = "Show TrayIcon";
            this.ch_trayicon_setting.UseVisualStyleBackColor = true;
            this.ch_trayicon_setting.Click += new System.EventHandler(this.ch_trayicon_setting_Click);
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
            this.ch_auto_update.CheckedChanged += new System.EventHandler(this.ch_auto_update_CheckedChanged);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(97, 37);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(137, 12);
            this.label2.TabIndex = 8;
            this.label2.Text = "Welcome Feedback.";
            // 
            // linkLabel1
            // 
            this.linkLabel1.AutoSize = true;
            this.linkLabel1.LinkColor = System.Drawing.SystemColors.Info;
            this.linkLabel1.Location = new System.Drawing.Point(77, 68);
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
            this.groupBox4.Location = new System.Drawing.Point(12, 515);
            this.groupBox4.Name = "groupBox4";
            this.groupBox4.Size = new System.Drawing.Size(344, 119);
            this.groupBox4.TabIndex = 8;
            this.groupBox4.TabStop = false;
            this.groupBox4.Text = "Info";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("굴림", 20F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.label1.ForeColor = System.Drawing.Color.White;
            this.label1.Location = new System.Drawing.Point(86, 651);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(125, 27);
            this.label1.TabIndex = 9;
            this.label1.Text = "Status : ";
            // 
            // pic_status
            // 
            this.pic_status.Location = new System.Drawing.Point(218, 651);
            this.pic_status.Name = "pic_status";
            this.pic_status.Size = new System.Drawing.Size(25, 27);
            this.pic_status.TabIndex = 10;
            this.pic_status.TabStop = false;
            // 
            // l_close
            // 
            this.l_close.AutoSize = true;
            this.l_close.Cursor = System.Windows.Forms.Cursors.Hand;
            this.l_close.Font = new System.Drawing.Font("Arial", 20F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.l_close.ForeColor = System.Drawing.Color.White;
            this.l_close.Location = new System.Drawing.Point(326, 9);
            this.l_close.Name = "l_close";
            this.l_close.Size = new System.Drawing.Size(30, 27);
            this.l_close.TabIndex = 11;
            this.l_close.Text = "X";
            this.l_close.Click += new System.EventHandler(this.l_close_Click);
            this.l_close.MouseLeave += new System.EventHandler(this.l_close_MouseLeave);
            this.l_close.MouseHover += new System.EventHandler(this.l_close_MouseHover);
            // 
            // l_hide
            // 
            this.l_hide.AutoSize = true;
            this.l_hide.Cursor = System.Windows.Forms.Cursors.Hand;
            this.l_hide.Font = new System.Drawing.Font("Arial", 20F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.l_hide.ForeColor = System.Drawing.Color.White;
            this.l_hide.Location = new System.Drawing.Point(290, 9);
            this.l_hide.Name = "l_hide";
            this.l_hide.Size = new System.Drawing.Size(27, 27);
            this.l_hide.TabIndex = 13;
            this.l_hide.Text = "_";
            this.l_hide.Click += new System.EventHandler(this.l_hide_Click);
            this.l_hide.MouseLeave += new System.EventHandler(this.l_hide_MouseLeave);
            this.l_hide.MouseHover += new System.EventHandler(this.l_hide_MouseHover);
            // 
            // l_version
            // 
            this.l_version.AutoSize = true;
            this.l_version.Font = new System.Drawing.Font("Arial", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.l_version.ForeColor = System.Drawing.Color.White;
            this.l_version.Location = new System.Drawing.Point(17, 16);
            this.l_version.Name = "l_version";
            this.l_version.Size = new System.Drawing.Size(82, 19);
            this.l_version.TabIndex = 14;
            this.l_version.Text = "VERSION";
            // 
            // btn_notch
            // 
            this.btn_notch.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(224)))));
            this.btn_notch.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btn_notch.FlatAppearance.BorderSize = 0;
            this.btn_notch.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btn_notch.Font = new System.Drawing.Font("Arial", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btn_notch.Location = new System.Drawing.Point(111, 0);
            this.btn_notch.Name = "btn_notch";
            this.btn_notch.Size = new System.Drawing.Size(162, 36);
            this.btn_notch.TabIndex = 15;
            this.btn_notch.Text = "Notch";
            this.btn_notch.UseVisualStyleBackColor = false;
            this.btn_notch.MouseDown += new System.Windows.Forms.MouseEventHandler(this.btn_notch_MouseDown);
            this.btn_notch.MouseMove += new System.Windows.Forms.MouseEventHandler(this.btn_notch_MouseMove);
            this.btn_notch.MouseUp += new System.Windows.Forms.MouseEventHandler(this.btn_notch_MouseUp);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(44)))), ((int)(((byte)(44)))), ((int)(((byte)(54)))));
            this.ClientSize = new System.Drawing.Size(368, 769);
            this.ControlBox = false;
            this.Controls.Add(this.btn_notch);
            this.Controls.Add(this.l_version);
            this.Controls.Add(this.l_hide);
            this.Controls.Add(this.l_close);
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
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "Form1";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
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
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.GroupBox groupBox3;
        private System.Windows.Forms.RadioButton rb_slow;
        private System.Windows.Forms.RadioButton rb_normal;
        private System.Windows.Forms.RadioButton rb_fast;
        private System.Windows.Forms.CheckBox ch_auto_start;
        private System.Windows.Forms.CheckBox ch_trayicon_setting;
        private System.Windows.Forms.CheckBox ch_auto_update;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.LinkLabel linkLabel1;
        private System.Windows.Forms.GroupBox groupBox4;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.PictureBox pic_status;
        private System.Windows.Forms.Label l_close;
        private System.Windows.Forms.Label l_hide;
        private System.Windows.Forms.Label l_version;
        private System.Windows.Forms.CheckBox ch_graphic_temperature;
        private System.Windows.Forms.CheckBox ch_ram_temperature;
        private System.Windows.Forms.CheckBox ch_board_temperature;
        private System.Windows.Forms.CheckBox ch_cpu_temperature;
        private System.Windows.Forms.CheckBox chk_disable_alert;
        private System.Windows.Forms.Button btn_notch;
    }
}

