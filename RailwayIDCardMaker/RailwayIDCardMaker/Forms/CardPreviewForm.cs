using System;
using System.Drawing;
using System.Windows.Forms;
using RailwayIDCardMaker.Models;
using RailwayIDCardMaker.Services;

namespace RailwayIDCardMaker.Forms
{
    public partial class CardPreviewForm : Form
    {
        private Employee _employee;
        private Image _logoImage;
        private PrintService _printService;

        public CardPreviewForm()
        {
            InitializeComponent();
            _printService = new PrintService();
        }

        public void LoadPreview(Employee employee, Image logoImage = null)
        {
            _employee = employee;
            _logoImage = logoImage;

            UpdatePreview();
        }

        private void UpdatePreview()
        {
            if (_employee == null) return;

            using (Bitmap front = CardRenderer.RenderCardFront(_employee, _logoImage))
            {
                picFront.Image = new Bitmap(front);
            }

            using (Bitmap back = CardRenderer.RenderCardBack(_employee))
            {
                picBack.Image = new Bitmap(back);
            }

            lblName.Text = _employee.Name;
            lblID.Text = $"ID: {_employee.IDCardNumber}";
        }

        private void btnPrint_Click(object sender, EventArgs e)
        {
            _printService.PrintCard(_employee, _logoImage);
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
