using System;
using System.Windows.Forms;
using Product_Manager.Presenters;

namespace Product_Manager.Views
{
    public partial class ProductsView : Form
    {
        private string message;
        public bool IsSuccssfull { get; set; }
        public ProductsView()
        {
            InitializeComponent();
        }

        // used only to catch exception on connection when form first loaded
        // other way was to load intial data in the presenter
        private void ProductsView_Load(object sender, EventArgs e)
        {

            Message = string.Empty;
            ProductsPresenter.Instance(this).LoadAllProducts();
            DisplayMessage();
        }
        public void SetBindingSource(BindingSource source)
        {
            productGridView.DataSource = source;
        }

        private void btnRead_Click(object sender, EventArgs e)
        {
            Message = string.Empty;
            ProductsPresenter.Instance(this).LoadAllProducts();
            DisplayMessage();
        }

        private void btnCreate_Click(object sender, EventArgs e)
        {
            Message = string.Empty;
            ProductsPresenter.Instance(this).CreateNewProduct();
            DisplayMessage();
        }

        private void btnUpdate_Click(object sender, EventArgs e)
        {
            Message = string.Empty;
            ProductsPresenter.Instance(this).UpdateProduct();
            DisplayMessage();
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            Message = string.Empty;
            ProductsPresenter.Instance(this).DeleteProduct();
            DisplayMessage();
        }

        private void btnSearch_Click(object sender, EventArgs e)
        {
            Message = string.Empty;
            ProductsPresenter.Instance(this).SearchList();
            DisplayMessage();
        }
        public string ProductId
        {
            get { return txtId.Text; }
            set { txtId.Text = value; }
        }
        public string ProductName
        {
            get { return txtName.Text; }
            set { txtName.Text = value; }
        }
        public string ProductPrice
        {
            get { return txtPrice.Text; }
            set { txtPrice.Text = value; }
        }
        public string SearchValue
        {
            get { return txtSearch.Text; }
            set { txtSearch.Text = value; }
        }
        public string Message
        {
            get { return message; }
            set { message = value; }
        }

        public void DisplayMessage()
        {
            if (!string.IsNullOrEmpty(Message))
            {
                string caption = IsSuccssfull ? "Success!" : "Error";
                MessageBoxIcon icon = IsSuccssfull ? MessageBoxIcon.Information : MessageBoxIcon.Error;
                MessageBox.Show(message, caption, MessageBoxButtons.OK, icon);
            }
        }
        private void productGridView_Click(object sender, EventArgs e)
        {
            int rowIndex = productGridView.CurrentRow.Index;
            if (rowIndex < productGridView.RowCount - 1)
            {
                ProductId = productGridView[0, rowIndex].Value.ToString();
                ProductName = productGridView[1, rowIndex].Value.ToString();
                ProductPrice = productGridView[2, rowIndex].Value.ToString();
            }
            else
            {
                ProductsPresenter.Instance(this).ClearFields();
            }
        }

        private void clearBtn_Click(object sender, EventArgs e)
        {
            ProductsPresenter.Instance(this).ClearFields();
        }

        private void txtSearch_KeyPress(object sender, KeyPressEventArgs e)
        {
            if(e.KeyChar.Equals((char)13))
            {
                Message = string.Empty;
                ProductsPresenter.Instance(this).SearchList();
                DisplayMessage();
            }
        }
    }
}
