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
            this.SuspendLayout();
            // 
            // l_core
            // 
            this.l_core.AutoSize = true;
            this.l_core.Font = new System.Drawing.Font("굴림", 28F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.l_core.Location = new System.Drawing.Point(12, 35);
            this.l_core.Name = "l_core";
            this.l_core.Size = new System.Drawing.Size(206, 38);
            this.l_core.TabIndex = 0;
            this.l_core.Text = "Core      : ";
            // 
            // l_th
            // 
            this.l_th.AutoSize = true;
            this.l_th.Font = new System.Drawing.Font("굴림", 28F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.l_th.Location = new System.Drawing.Point(12, 96);
            this.l_th.Name = "l_th";
            this.l_th.Size = new System.Drawing.Size(205, 38);
            this.l_th.TabIndex = 1;
            this.l_th.Text = "Thread   : ";
            // 
            // l_th_value
            // 
            this.l_th_value.AutoSize = true;
            this.l_th_value.Font = new System.Drawing.Font("굴림", 28F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.l_th_value.Location = new System.Drawing.Point(211, 96);
            this.l_th_value.Name = "l_th_value";
            this.l_th_value.Size = new System.Drawing.Size(39, 38);
            this.l_th_value.TabIndex = 3;
            this.l_th_value.Text = "0";
            // 
            // l_core_value
            // 
            this.l_core_value.AutoSize = true;
            this.l_core_value.Font = new System.Drawing.Font("굴림", 28F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.l_core_value.Location = new System.Drawing.Point(211, 35);
            this.l_core_value.Name = "l_core_value";
            this.l_core_value.Size = new System.Drawing.Size(39, 38);
            this.l_core_value.TabIndex = 2;
            this.l_core_value.Text = "0";
            // 
            // btn_tray
            // 
            this.btn_tray.Location = new System.Drawing.Point(19, 151);
            this.btn_tray.Name = "btn_tray";
            this.btn_tray.Size = new System.Drawing.Size(337, 44);
            this.btn_tray.TabIndex = 4;
            this.btn_tray.Text = "Toggle Tray";
            this.btn_tray.UseVisualStyleBackColor = true;
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
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(368, 210);
            this.Controls.Add(this.btn_tray);
            this.Controls.Add(this.l_th_value);
            this.Controls.Add(this.l_core_value);
            this.Controls.Add(this.l_th);
            this.Controls.Add(this.l_core);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "Form1";
            this.Text = "CoreTracker";
            this.Load += new System.EventHandler(this.Form1_Load);
            this.Resize += new System.EventHandler(this.Form1_Resize);
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
    }
}

