
namespace HTML_Crawler_3._0
{
    partial class HtmlConsole
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
            this.commandText = new System.Windows.Forms.TextBox();
            this.ConsoleTextBox = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.FileOpen = new System.Windows.Forms.Button();
            this.HtmlImageButton = new System.Windows.Forms.Button();
            this.panel1 = new System.Windows.Forms.Panel();
            this.htmlPictuerBox = new System.Windows.Forms.PictureBox();
            this.Save = new System.Windows.Forms.Button();
            this.label3 = new System.Windows.Forms.Label();
            this.panel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.htmlPictuerBox)).BeginInit();
            this.SuspendLayout();
            // 
            // commandText
            // 
            this.commandText.BackColor = System.Drawing.Color.Lavender;
            this.commandText.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.commandText.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.commandText.Location = new System.Drawing.Point(51, 75);
            this.commandText.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.commandText.Name = "commandText";
            this.commandText.Size = new System.Drawing.Size(397, 20);
            this.commandText.TabIndex = 0;
            this.commandText.KeyDown += new System.Windows.Forms.KeyEventHandler(this.commandText_KeyDown);
            // 
            // ConsoleTextBox
            // 
            this.ConsoleTextBox.BackColor = System.Drawing.Color.MidnightBlue;
            this.ConsoleTextBox.Font = new System.Drawing.Font("Courier New", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.ConsoleTextBox.ForeColor = System.Drawing.Color.Wheat;
            this.ConsoleTextBox.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.ConsoleTextBox.Location = new System.Drawing.Point(51, 127);
            this.ConsoleTextBox.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.ConsoleTextBox.MaxLength = 200000000;
            this.ConsoleTextBox.Multiline = true;
            this.ConsoleTextBox.Name = "ConsoleTextBox";
            this.ConsoleTextBox.ReadOnly = true;
            this.ConsoleTextBox.ScrollBars = System.Windows.Forms.ScrollBars.Both;
            this.ConsoleTextBox.Size = new System.Drawing.Size(516, 632);
            this.ConsoleTextBox.TabIndex = 1;
            this.ConsoleTextBox.WordWrap = false;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(48, 58);
            this.label1.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(100, 14);
            this.label1.TabIndex = 2;
            this.label1.Text = "Command Line";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(47, 110);
            this.label2.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(105, 14);
            this.label2.TabIndex = 3;
            this.label2.Text = "Output Console";
            // 
            // FileOpen
            // 
            this.FileOpen.BackColor = System.Drawing.Color.Black;
            this.FileOpen.Cursor = System.Windows.Forms.Cursors.Hand;
            this.FileOpen.FlatAppearance.BorderColor = System.Drawing.Color.Lime;
            this.FileOpen.FlatStyle = System.Windows.Forms.FlatStyle.System;
            this.FileOpen.Location = new System.Drawing.Point(51, 14);
            this.FileOpen.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.FileOpen.Name = "FileOpen";
            this.FileOpen.Size = new System.Drawing.Size(93, 25);
            this.FileOpen.TabIndex = 4;
            this.FileOpen.Text = "Open file...";
            this.FileOpen.UseVisualStyleBackColor = false;
            this.FileOpen.Click += new System.EventHandler(this.FileOpen_Click);
            // 
            // HtmlImageButton
            // 
            this.HtmlImageButton.BackColor = System.Drawing.Color.Black;
            this.HtmlImageButton.Cursor = System.Windows.Forms.Cursors.Hand;
            this.HtmlImageButton.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.HtmlImageButton.Location = new System.Drawing.Point(274, 14);
            this.HtmlImageButton.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.HtmlImageButton.Name = "HtmlImageButton";
            this.HtmlImageButton.Size = new System.Drawing.Size(98, 25);
            this.HtmlImageButton.TabIndex = 6;
            this.HtmlImageButton.Text = "Visualise";
            this.HtmlImageButton.UseVisualStyleBackColor = false;
            this.HtmlImageButton.Click += new System.EventHandler(this.HtmlImageButton_Click);
            // 
            // panel1
            // 
            this.panel1.AutoScroll = true;
            this.panel1.BackColor = System.Drawing.Color.Silver;
            this.panel1.Controls.Add(this.htmlPictuerBox);
            this.panel1.Location = new System.Drawing.Point(734, 127);
            this.panel1.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(628, 632);
            this.panel1.TabIndex = 7;
            // 
            // htmlPictuerBox
            // 
            this.htmlPictuerBox.Location = new System.Drawing.Point(0, 0);
            this.htmlPictuerBox.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.htmlPictuerBox.Name = "htmlPictuerBox";
            this.htmlPictuerBox.Size = new System.Drawing.Size(375, 452);
            this.htmlPictuerBox.SizeMode = System.Windows.Forms.PictureBoxSizeMode.AutoSize;
            this.htmlPictuerBox.TabIndex = 0;
            this.htmlPictuerBox.TabStop = false;
            // 
            // Save
            // 
            this.Save.BackColor = System.Drawing.Color.Black;
            this.Save.Cursor = System.Windows.Forms.Cursors.Hand;
            this.Save.FlatAppearance.BorderColor = System.Drawing.Color.Lime;
            this.Save.FlatStyle = System.Windows.Forms.FlatStyle.System;
            this.Save.Location = new System.Drawing.Point(162, 14);
            this.Save.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.Save.Name = "Save";
            this.Save.Size = new System.Drawing.Size(93, 25);
            this.Save.TabIndex = 8;
            this.Save.Text = "Save...";
            this.Save.UseVisualStyleBackColor = false;
            this.Save.Click += new System.EventHandler(this.Save_Click);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(731, 110);
            this.label3.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(119, 14);
            this.label3.TabIndex = 9;
            this.label3.Text = "Graphics Console";
            // 
            // HtmlConsole
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 14F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.MidnightBlue;
            this.ClientSize = new System.Drawing.Size(1411, 798);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.Save);
            this.Controls.Add(this.HtmlImageButton);
            this.Controls.Add(this.FileOpen);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.ConsoleTextBox);
            this.Controls.Add(this.commandText);
            this.Controls.Add(this.panel1);
            this.Font = new System.Drawing.Font("Orbitron", 8.249999F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.ForeColor = System.Drawing.Color.LimeGreen;
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
            this.ImeMode = System.Windows.Forms.ImeMode.Off;
            this.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "HtmlConsole";
            this.Text = "HTML Console";
            this.Load += new System.EventHandler(this.Form1_Load);
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.htmlPictuerBox)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox commandText;
        private System.Windows.Forms.TextBox ConsoleTextBox;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Button FileOpen;
        private System.Windows.Forms.Button HtmlImageButton;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.PictureBox htmlPictuerBox;
        private System.Windows.Forms.Button Save;
        private System.Windows.Forms.Label label3;
    }
}

