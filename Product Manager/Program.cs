using Product_Manager.Presenters;
using Product_Manager.Views;
using System;
using System.Windows.Forms;

namespace Product_Manager
{
    internal static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            ProductsView view = new ProductsView();
            ProductsPresenter presenter = ProductsPresenter.Instance(view);
            Application.Run(view);
        }
    }
}
