
namespace DeviceSubApp
{
    partial class FrmMain
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FrmMain));
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.TxtConnectionString = new System.Windows.Forms.TextBox();
            this.TxtClientID = new System.Windows.Forms.TextBox();
            this.TxtSubscriptionTopic = new System.Windows.Forms.TextBox();
            this.BtnConnect = new System.Windows.Forms.Button();
            this.BtnDisconnect = new System.Windows.Forms.Button();
            this.RtbSubscr = new System.Windows.Forms.RichTextBox();
            this.statusStrip1 = new System.Windows.Forms.StatusStrip();
            this.LblResult = new System.Windows.Forms.ToolStripStatusLabel();
            this.Timer = new System.Windows.Forms.Timer(this.components);
            this.statusStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(39, 31);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(105, 15);
            this.label1.TabIndex = 0;
            this.label1.Text = "Connection String";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(90, 81);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(54, 15);
            this.label2.TabIndex = 0;
            this.label2.Text = "Client ID";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(37, 131);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(107, 15);
            this.label3.TabIndex = 0;
            this.label3.Text = "Subscription Topic";
            // 
            // TxtConnectionString
            // 
            this.TxtConnectionString.Location = new System.Drawing.Point(159, 28);
            this.TxtConnectionString.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.TxtConnectionString.Name = "TxtConnectionString";
            this.TxtConnectionString.Size = new System.Drawing.Size(514, 23);
            this.TxtConnectionString.TabIndex = 1;
            this.TxtConnectionString.Text = "210.119.12.86";
            // 
            // TxtClientID
            // 
            this.TxtClientID.Location = new System.Drawing.Point(159, 78);
            this.TxtClientID.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.TxtClientID.Name = "TxtClientID";
            this.TxtClientID.Size = new System.Drawing.Size(514, 23);
            this.TxtClientID.TabIndex = 1;
            this.TxtClientID.Text = "SUBSCR01";
            // 
            // TxtSubscriptionTopic
            // 
            this.TxtSubscriptionTopic.Location = new System.Drawing.Point(159, 128);
            this.TxtSubscriptionTopic.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.TxtSubscriptionTopic.Name = "TxtSubscriptionTopic";
            this.TxtSubscriptionTopic.Size = new System.Drawing.Size(514, 23);
            this.TxtSubscriptionTopic.TabIndex = 1;
            this.TxtSubscriptionTopic.Text = "factory1/machine1/data/";
            // 
            // BtnConnect
            // 
            this.BtnConnect.Location = new System.Drawing.Point(717, 128);
            this.BtnConnect.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.BtnConnect.Name = "BtnConnect";
            this.BtnConnect.Size = new System.Drawing.Size(198, 30);
            this.BtnConnect.TabIndex = 2;
            this.BtnConnect.Text = "Connect";
            this.BtnConnect.UseVisualStyleBackColor = true;
            this.BtnConnect.Click += new System.EventHandler(this.BtnConnect_Click);
            // 
            // BtnDisconnect
            // 
            this.BtnDisconnect.Location = new System.Drawing.Point(950, 128);
            this.BtnDisconnect.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.BtnDisconnect.Name = "BtnDisconnect";
            this.BtnDisconnect.Size = new System.Drawing.Size(198, 30);
            this.BtnDisconnect.TabIndex = 2;
            this.BtnDisconnect.Text = "Disconnect";
            this.BtnDisconnect.UseVisualStyleBackColor = true;
            this.BtnDisconnect.Click += new System.EventHandler(this.BtnDisconnect_Click);
            // 
            // RtbSubscr
            // 
            this.RtbSubscr.Font = new System.Drawing.Font("돋움", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.RtbSubscr.Location = new System.Drawing.Point(31, 173);
            this.RtbSubscr.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.RtbSubscr.Name = "RtbSubscr";
            this.RtbSubscr.Size = new System.Drawing.Size(1117, 462);
            this.RtbSubscr.TabIndex = 3;
            this.RtbSubscr.Text = "";
            // 
            // statusStrip1
            // 
            this.statusStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.LblResult});
            this.statusStrip1.Location = new System.Drawing.Point(0, 639);
            this.statusStrip1.Name = "statusStrip1";
            this.statusStrip1.Size = new System.Drawing.Size(1184, 22);
            this.statusStrip1.TabIndex = 4;
            this.statusStrip1.Text = "statusStrip1";
            // 
            // LblResult
            // 
            this.LblResult.Name = "LblResult";
            this.LblResult.Size = new System.Drawing.Size(14, 17);
            this.LblResult.Text = "0";
            // 
            // FrmMain
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1184, 661);
            this.Controls.Add(this.statusStrip1);
            this.Controls.Add(this.RtbSubscr);
            this.Controls.Add(this.BtnDisconnect);
            this.Controls.Add(this.BtnConnect);
            this.Controls.Add(this.TxtSubscriptionTopic);
            this.Controls.Add(this.TxtClientID);
            this.Controls.Add(this.TxtConnectionString);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Font = new System.Drawing.Font("맑은 고딕", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.MaximizeBox = false;
            this.Name = "FrmMain";
            this.Text = "IoT Device Subscriber";
            this.statusStrip1.ResumeLayout(false);
            this.statusStrip1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox TxtConnectionString;
        private System.Windows.Forms.TextBox TxtClientID;
        private System.Windows.Forms.TextBox TxtSubscriptionTopic;
        private System.Windows.Forms.Button BtnConnect;
        private System.Windows.Forms.Button BtnDisconnect;
        private System.Windows.Forms.RichTextBox RtbSubscr;
        private System.Windows.Forms.StatusStrip statusStrip1;
        private System.Windows.Forms.ToolStripStatusLabel LblResult;
        private System.Windows.Forms.Timer Timer;
    }
}

