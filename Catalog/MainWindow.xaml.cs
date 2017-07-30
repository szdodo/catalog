using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Configuration;
using System.Data.SqlClient;
using System.Data;

namespace Catalog
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {

        string connectionString;
        SqlConnection connection;

        public MainWindow()
        {
            InitializeComponent();
            connectionString = ConfigurationManager.ConnectionStrings["Catalog.Properties.Settings.testConnectionString"].ConnectionString;
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            PopulateBooks();
        }

        void PopulateBooks()
        {
            using (connection = new SqlConnection(connectionString)) //using will close the object automatically ---> it implements high disposable
            using (SqlDataAdapter adapter=new SqlDataAdapter("SELECT * FROM Book;", connection))
            {
                //connection.Open(); --sqladapter opens it automatically
                DataTable bookTable = new DataTable();
                adapter.Fill(bookTable);

                //ListBook.DisplayMemberPath = "Title";
                //ListBook.SelectedValuePath = "Author"; -- ha van foreignkey sztem akkor kell ez a cuccos
                //ListBook.DisplayMemberPath = "Author";
                //ListBook.DisplayMemberPath = "Genre";

                //ListBook.ItemsSource = bookTable.Rows;
                ListBook.ItemsSource = bookTable.DefaultView;
            }
        }

        private void Add_Click(object sender, RoutedEventArgs e)
        {
            string query = "INSERT INTO Book (Id, Title, Author, Genre) VALUES (10,'" + NewBook.Text + "', 'new', 'ssss') ";
            using (connection = new SqlConnection(connectionString)) //using will close the object automatically ---> it implements high disposable
            using (SqlCommand command=new SqlCommand(query, connection))
            {
                connection.Open();
                //command.Parameters.AddWithValue("@NewBook", NewBook.Text); -- ha paraméterekkel csinlnánk
                command.ExecuteScalar();
            }
        }
    }
}
