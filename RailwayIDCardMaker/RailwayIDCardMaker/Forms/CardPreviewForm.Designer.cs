namespace RailwayIDCardMaker.Forms
{
    partial class CardPreviewForm
    {
        private System.ComponentModel.IContainer components = null;

        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        private void InitializeComponent()
        {
            this.pnlHeader = new System.Windows.Forms.Panel();
            this.lblName = new System.Windows.Forms.Label();
            this.lblID = new System.Windows.Forms.Label();
            this.pnlCards = new System.Windows.Forms.Panel();
            this.picFront = new System.Windows.Forms.PictureBox();
            this.picBack = new System.Windows.Forms.PictureBox();
            this.lblFront = new System.Windows.Forms.Label();
            this.lblBack = new System.Windows.Forms.Label();
            this.pnlButtons = new System.Windows.Forms.Panel();
            this.btnPrint = new System.Windows.Forms.Button();
            this.btnClose = new System.Windows.Forms.Button();
            this.pnlHeader.SuspendLayout();
            this.pnlCards.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.picFront)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.picBack)).BeginInit();
            this.pnlButtons.SuspendLayout();
            this.SuspendLayout();
            // 
            // pnlHeader
            // 
            this.pnlHeader.BackColor = System.Drawing.Color.FromArgb(0, 51, 102);
            this.pnlHeader.Controls.Add(this.lblName);
            this.pnlHeader.Controls.Add(this.lblID);
            this.pnlHeader.Dock = System.Windows.Forms.DockStyle.Top;
            this.pnlHeader.Location = new System.Drawing.Point(0, 0);
            this.pnlHeader.Size = new System.Drawing.Size(600, 50);
            // 
            // lblName
            // 
            this.lblName.Font = new System.Drawing.Font("Segoe UI", 14F, System.Drawing.FontStyle.Bold);
            this.lblName.ForeColor = System.Drawing.Color.White;
            this.lblName.Location = new System.Drawing.Point(10, 8);
            this.lblName.Size = new System.Drawing.Size(400, 30);
            this.lblName.Text = "Employee Name";
            // 
            // lblID
            // 
            this.lblID.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.lblID.ForeColor = System.Drawing.Color.Gold;
            this.lblID.Location = new System.Drawing.Point(420, 15);
            this.lblID.Size = new System.Drawing.Size(170, 20);
            this.lblID.Text = "ID: 000000000000";
            this.lblID.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // pnlCards
            // 
            this.pnlCards.Controls.Add(this.lblFront);
            this.pnlCards.Controls.Add(this.lblBack);
            this.pnlCards.Controls.Add(this.picFront);
            this.pnlCards.Controls.Add(this.picBack);
            this.pnlCards.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pnlCards.Location = new System.Drawing.Point(0, 50);
            this.pnlCards.Size = new System.Drawing.Size(600, 400);
            // 
            // lblFront
            // 
            this.lblFront.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Bold);
            this.lblFront.Location = new System.Drawing.Point(50, 10);
            this.lblFront.Size = new System.Drawing.Size(200, 20);
            this.lblFront.Text = "FRONT";
            this.lblFront.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // lblBack
            // 
            this.lblBack.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Bold);
            this.lblBack.Location = new System.Drawing.Point(350, 10);
            this.lblBack.Size = new System.Drawing.Size(200, 20);
            this.lblBack.Text = "BACK";
            this.lblBack.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // picFront
            // 
            this.picFront.BackColor = System.Drawing.Color.White;
            this.picFront.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.picFront.Location = new System.Drawing.Point(50, 35);
            this.picFront.Size = new System.Drawing.Size(200, 340);
            this.picFront.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            // 
            // picBack
            // 
            this.picBack.BackColor = System.Drawing.Color.White;
            this.picBack.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.picBack.Location = new System.Drawing.Point(350, 35);
            this.picBack.Size = new System.Drawing.Size(200, 340);
            this.picBack.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            // 
            // pnlButtons
            // 
            this.pnlButtons.Controls.Add(this.btnPrint);
            this.pnlButtons.Controls.Add(this.btnClose);
            this.pnlButtons.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.pnlButtons.Location = new System.Drawing.Point(0, 450);
            this.pnlButtons.Size = new System.Drawing.Size(600, 50);
            // 
            // btnPrint
            // 
            this.btnPrint.BackColor = System.Drawing.Color.FromArgb(0, 102, 51);
            this.btnPrint.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnPrint.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Bold);
            this.btnPrint.ForeColor = System.Drawing.Color.White;
            this.btnPrint.Location = new System.Drawing.Point(200, 8);
            this.btnPrint.Size = new System.Drawing.Size(100, 35);
            this.btnPrint.Text = "🖨️ Print";
            this.btnPrint.UseVisualStyleBackColor = false;
            this.btnPrint.Click += new System.EventHandler(this.btnPrint_Click);
            // 
            // btnClose
            // 
            this.btnClose.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.btnClose.Location = new System.Drawing.Point(310, 8);
            this.btnClose.Size = new System.Drawing.Size(100, 35);
            this.btnClose.Text = "Close";
            this.btnClose.Click += new System.EventHandler(this.btnClose_Click);
            // 
            // CardPreviewForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.WhiteSmoke;
            this.CancelButton = this.btnClose;
            this.ClientSize = new System.Drawing.Size(600, 500);
            this.Controls.Add(this.pnlCards);
            this.Controls.Add(this.pnlButtons);
            this.Controls.Add(this.pnlHeader);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.Name = "CardPreviewForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Card Preview";
            this.pnlHeader.ResumeLayout(false);
            this.pnlCards.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.picFront)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.picBack)).EndInit();
            this.pnlButtons.ResumeLayout(false);
            this.ResumeLayout(false);
        }

        private System.Windows.Forms.Panel pnlHeader;
        private System.Windows.Forms.Label lblName;
        private System.Windows.Forms.Label lblID;
        private System.Windows.Forms.Panel pnlCards;
        private System.Windows.Forms.Label lblFront;
        private System.Windows.Forms.Label lblBack;
        private System.Windows.Forms.PictureBox picFront;
        private System.Windows.Forms.PictureBox picBack;
        private System.Windows.Forms.Panel pnlButtons;
        private System.Windows.Forms.Button btnPrint;
        private System.Windows.Forms.Button btnClose;
    }
}
