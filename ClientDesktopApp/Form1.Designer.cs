namespace ClientDesktopApp
{
    partial class Form1
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
            this.textBoxServerUrl = new System.Windows.Forms.TextBox();
            this.buttonConnect = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.buttonReadText = new System.Windows.Forms.Button();
            this.textBoxReadText = new System.Windows.Forms.TextBox();
            this.buttonReadTextList = new System.Windows.Forms.Button();
            this.richTextBoxTextsList = new System.Windows.Forms.RichTextBox();
            this.buttonAddTextToList = new System.Windows.Forms.Button();
            this.textBoxTextToAdd = new System.Windows.Forms.TextBox();
            this.buttonReadObject = new System.Windows.Forms.Button();
            this.labelObject = new System.Windows.Forms.Label();
            this.buttonAuthorize = new System.Windows.Forms.Button();
            this.textBoxUsername = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.textBoxPassword = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.labelAuthorize = new System.Windows.Forms.Label();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.numericObjectId = new System.Windows.Forms.NumericUpDown();
            this.label4 = new System.Windows.Forms.Label();
            this.groupBox3 = new System.Windows.Forms.GroupBox();
            this.label5 = new System.Windows.Forms.Label();
            this.groupBox4 = new System.Windows.Forms.GroupBox();
            this.groupBox5 = new System.Windows.Forms.GroupBox();
            this.groupBox6 = new System.Windows.Forms.GroupBox();
            this.richTextBoxLog = new System.Windows.Forms.RichTextBox();
            this.groupBox1.SuspendLayout();
            this.groupBox2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numericObjectId)).BeginInit();
            this.groupBox3.SuspendLayout();
            this.groupBox4.SuspendLayout();
            this.groupBox5.SuspendLayout();
            this.groupBox6.SuspendLayout();
            this.SuspendLayout();
            // 
            // textBoxServerUrl
            // 
            this.textBoxServerUrl.Location = new System.Drawing.Point(9, 34);
            this.textBoxServerUrl.Name = "textBoxServerUrl";
            this.textBoxServerUrl.Size = new System.Drawing.Size(374, 20);
            this.textBoxServerUrl.TabIndex = 0;
            this.textBoxServerUrl.Text = "http://localhost:5220";
            // 
            // buttonConnect
            // 
            this.buttonConnect.Location = new System.Drawing.Point(6, 60);
            this.buttonConnect.Name = "buttonConnect";
            this.buttonConnect.Size = new System.Drawing.Size(377, 23);
            this.buttonConnect.TabIndex = 1;
            this.buttonConnect.Text = "Connect";
            this.buttonConnect.UseVisualStyleBackColor = true;
            this.buttonConnect.Click += new System.EventHandler(this.buttonConnect_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(6, 17);
            this.label1.Margin = new System.Windows.Forms.Padding(3, 1, 3, 1);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(57, 13);
            this.label1.TabIndex = 2;
            this.label1.Text = "Server Url:";
            // 
            // buttonReadText
            // 
            this.buttonReadText.Location = new System.Drawing.Point(6, 19);
            this.buttonReadText.Name = "buttonReadText";
            this.buttonReadText.Size = new System.Drawing.Size(99, 23);
            this.buttonReadText.TabIndex = 3;
            this.buttonReadText.Text = "Read text";
            this.buttonReadText.UseVisualStyleBackColor = true;
            // 
            // textBoxReadText
            // 
            this.textBoxReadText.Location = new System.Drawing.Point(6, 48);
            this.textBoxReadText.Name = "textBoxReadText";
            this.textBoxReadText.Size = new System.Drawing.Size(377, 20);
            this.textBoxReadText.TabIndex = 4;
            // 
            // buttonReadTextList
            // 
            this.buttonReadTextList.Location = new System.Drawing.Point(6, 19);
            this.buttonReadTextList.Name = "buttonReadTextList";
            this.buttonReadTextList.Size = new System.Drawing.Size(99, 23);
            this.buttonReadTextList.TabIndex = 5;
            this.buttonReadTextList.Text = "Read";
            this.buttonReadTextList.UseVisualStyleBackColor = true;
            // 
            // richTextBoxTextsList
            // 
            this.richTextBoxTextsList.Location = new System.Drawing.Point(6, 48);
            this.richTextBoxTextsList.Name = "richTextBoxTextsList";
            this.richTextBoxTextsList.ScrollBars = System.Windows.Forms.RichTextBoxScrollBars.ForcedVertical;
            this.richTextBoxTextsList.Size = new System.Drawing.Size(377, 58);
            this.richTextBoxTextsList.TabIndex = 6;
            this.richTextBoxTextsList.Text = "";
            // 
            // buttonAddTextToList
            // 
            this.buttonAddTextToList.Location = new System.Drawing.Point(6, 162);
            this.buttonAddTextToList.Name = "buttonAddTextToList";
            this.buttonAddTextToList.Size = new System.Drawing.Size(99, 23);
            this.buttonAddTextToList.TabIndex = 7;
            this.buttonAddTextToList.Text = "Add";
            this.buttonAddTextToList.UseVisualStyleBackColor = true;
            // 
            // textBoxTextToAdd
            // 
            this.textBoxTextToAdd.Location = new System.Drawing.Point(6, 136);
            this.textBoxTextToAdd.Name = "textBoxTextToAdd";
            this.textBoxTextToAdd.Size = new System.Drawing.Size(377, 20);
            this.textBoxTextToAdd.TabIndex = 8;
            // 
            // buttonReadObject
            // 
            this.buttonReadObject.Location = new System.Drawing.Point(158, 19);
            this.buttonReadObject.Name = "buttonReadObject";
            this.buttonReadObject.Size = new System.Drawing.Size(99, 20);
            this.buttonReadObject.TabIndex = 9;
            this.buttonReadObject.Text = "Read object";
            this.buttonReadObject.UseVisualStyleBackColor = true;
            // 
            // labelObject
            // 
            this.labelObject.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.labelObject.Location = new System.Drawing.Point(6, 48);
            this.labelObject.Margin = new System.Windows.Forms.Padding(3);
            this.labelObject.Name = "labelObject";
            this.labelObject.Size = new System.Drawing.Size(377, 86);
            this.labelObject.TabIndex = 10;
            this.labelObject.Text = "[object data]";
            // 
            // buttonAuthorize
            // 
            this.buttonAuthorize.Location = new System.Drawing.Point(6, 64);
            this.buttonAuthorize.Name = "buttonAuthorize";
            this.buttonAuthorize.Size = new System.Drawing.Size(99, 23);
            this.buttonAuthorize.TabIndex = 11;
            this.buttonAuthorize.Text = "Authorize";
            this.buttonAuthorize.UseVisualStyleBackColor = true;
            // 
            // textBoxUsername
            // 
            this.textBoxUsername.Location = new System.Drawing.Point(6, 38);
            this.textBoxUsername.Name = "textBoxUsername";
            this.textBoxUsername.Size = new System.Drawing.Size(175, 20);
            this.textBoxUsername.TabIndex = 12;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(6, 21);
            this.label2.Margin = new System.Windows.Forms.Padding(3, 1, 3, 1);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(55, 13);
            this.label2.TabIndex = 13;
            this.label2.Text = "Username";
            // 
            // textBoxPassword
            // 
            this.textBoxPassword.Location = new System.Drawing.Point(208, 38);
            this.textBoxPassword.Name = "textBoxPassword";
            this.textBoxPassword.Size = new System.Drawing.Size(175, 20);
            this.textBoxPassword.TabIndex = 12;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(205, 21);
            this.label3.Margin = new System.Windows.Forms.Padding(3, 1, 3, 1);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(53, 13);
            this.label3.TabIndex = 13;
            this.label3.Text = "Password";
            // 
            // labelAuthorize
            // 
            this.labelAuthorize.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.labelAuthorize.Location = new System.Drawing.Point(6, 93);
            this.labelAuthorize.Margin = new System.Windows.Forms.Padding(3);
            this.labelAuthorize.Name = "labelAuthorize";
            this.labelAuthorize.Size = new System.Drawing.Size(377, 78);
            this.labelAuthorize.TabIndex = 10;
            this.labelAuthorize.Text = "[connection info]";
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.label2);
            this.groupBox1.Controls.Add(this.labelAuthorize);
            this.groupBox1.Controls.Add(this.buttonAuthorize);
            this.groupBox1.Controls.Add(this.label3);
            this.groupBox1.Controls.Add(this.textBoxUsername);
            this.groupBox1.Controls.Add(this.textBoxPassword);
            this.groupBox1.Location = new System.Drawing.Point(12, 538);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(389, 177);
            this.groupBox1.TabIndex = 14;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Authorize";
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.label4);
            this.groupBox2.Controls.Add(this.numericObjectId);
            this.groupBox2.Controls.Add(this.buttonReadObject);
            this.groupBox2.Controls.Add(this.labelObject);
            this.groupBox2.Location = new System.Drawing.Point(12, 392);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(389, 140);
            this.groupBox2.TabIndex = 15;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "Read object as json";
            // 
            // numericObjectId
            // 
            this.numericObjectId.Location = new System.Drawing.Point(32, 19);
            this.numericObjectId.Name = "numericObjectId";
            this.numericObjectId.Size = new System.Drawing.Size(120, 20);
            this.numericObjectId.TabIndex = 16;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(7, 21);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(19, 13);
            this.label4.TabIndex = 17;
            this.label4.Text = "Id:";
            // 
            // groupBox3
            // 
            this.groupBox3.Controls.Add(this.label5);
            this.groupBox3.Controls.Add(this.buttonReadTextList);
            this.groupBox3.Controls.Add(this.richTextBoxTextsList);
            this.groupBox3.Controls.Add(this.textBoxTextToAdd);
            this.groupBox3.Controls.Add(this.buttonAddTextToList);
            this.groupBox3.Location = new System.Drawing.Point(12, 191);
            this.groupBox3.Name = "groupBox3";
            this.groupBox3.Size = new System.Drawing.Size(389, 195);
            this.groupBox3.TabIndex = 16;
            this.groupBox3.TabStop = false;
            this.groupBox3.Text = "Read texts list";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(7, 119);
            this.label5.Margin = new System.Windows.Forms.Padding(3, 10, 3, 1);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(108, 13);
            this.label5.TabIndex = 9;
            this.label5.Text = "Add a new text to list:";
            // 
            // groupBox4
            // 
            this.groupBox4.Controls.Add(this.label1);
            this.groupBox4.Controls.Add(this.textBoxServerUrl);
            this.groupBox4.Controls.Add(this.buttonConnect);
            this.groupBox4.Location = new System.Drawing.Point(12, 12);
            this.groupBox4.Name = "groupBox4";
            this.groupBox4.Size = new System.Drawing.Size(389, 90);
            this.groupBox4.TabIndex = 17;
            this.groupBox4.TabStop = false;
            this.groupBox4.Text = "Connection";
            // 
            // groupBox5
            // 
            this.groupBox5.Controls.Add(this.buttonReadText);
            this.groupBox5.Controls.Add(this.textBoxReadText);
            this.groupBox5.Location = new System.Drawing.Point(12, 108);
            this.groupBox5.Name = "groupBox5";
            this.groupBox5.Size = new System.Drawing.Size(389, 77);
            this.groupBox5.TabIndex = 18;
            this.groupBox5.TabStop = false;
            this.groupBox5.Text = "Test connection";
            // 
            // groupBox6
            // 
            this.groupBox6.Controls.Add(this.richTextBoxLog);
            this.groupBox6.Location = new System.Drawing.Point(12, 723);
            this.groupBox6.Name = "groupBox6";
            this.groupBox6.Size = new System.Drawing.Size(389, 100);
            this.groupBox6.TabIndex = 19;
            this.groupBox6.TabStop = false;
            this.groupBox6.Text = "Log";
            // 
            // richTextBoxLog
            // 
            this.richTextBoxLog.Location = new System.Drawing.Point(6, 19);
            this.richTextBoxLog.Name = "richTextBoxLog";
            this.richTextBoxLog.ScrollBars = System.Windows.Forms.RichTextBoxScrollBars.ForcedVertical;
            this.richTextBoxLog.Size = new System.Drawing.Size(377, 75);
            this.richTextBoxLog.TabIndex = 6;
            this.richTextBoxLog.Text = "";
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(413, 835);
            this.Controls.Add(this.groupBox6);
            this.Controls.Add(this.groupBox5);
            this.Controls.Add(this.groupBox4);
            this.Controls.Add(this.groupBox3);
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.groupBox1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
            this.Name = "Form1";
            this.Text = "Client sample";
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numericObjectId)).EndInit();
            this.groupBox3.ResumeLayout(false);
            this.groupBox3.PerformLayout();
            this.groupBox4.ResumeLayout(false);
            this.groupBox4.PerformLayout();
            this.groupBox5.ResumeLayout(false);
            this.groupBox5.PerformLayout();
            this.groupBox6.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TextBox textBoxServerUrl;
        private System.Windows.Forms.Button buttonConnect;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button buttonReadText;
        private System.Windows.Forms.TextBox textBoxReadText;
        private System.Windows.Forms.Button buttonReadTextList;
        private System.Windows.Forms.RichTextBox richTextBoxTextsList;
        private System.Windows.Forms.Button buttonAddTextToList;
        private System.Windows.Forms.TextBox textBoxTextToAdd;
        private System.Windows.Forms.Button buttonReadObject;
        private System.Windows.Forms.Label labelObject;
        private System.Windows.Forms.Button buttonAuthorize;
        private System.Windows.Forms.TextBox textBoxUsername;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox textBoxPassword;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label labelAuthorize;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.NumericUpDown numericObjectId;
        private System.Windows.Forms.GroupBox groupBox3;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.GroupBox groupBox4;
        private System.Windows.Forms.GroupBox groupBox5;
        private System.Windows.Forms.GroupBox groupBox6;
        private System.Windows.Forms.RichTextBox richTextBoxLog;
    }
}

