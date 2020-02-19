using Library.BLL.Abstractions;
using Library.BLL.Model;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace Library.DAL
{
    public class Repository : IRepository
    {
        private string ConStr = "Server=.;Database=Library;User Id=admin; Password=admin;";
        // private string ConStr = "Server=.;Database=Library;Trusted_Connection=True;";
        SqlConnection connection;

        public Repository()
        {
            connection = new SqlConnection(ConStr);
        }

        public async Task<ObservableCollection<Press>> GetPresses()
        {

            if (connection.State != ConnectionState.Open)
                connection.Open();

            SqlCommand command = connection.CreateCommand();
            command.CommandText = "SELECT * FROM Press;";
            SqlDataReader reader = await command.ExecuteReaderAsync();
            ObservableCollection<Press> presses = new ObservableCollection<Press>();
            while (reader.Read())
            {
                Press press = new Press();
                press.Id = Convert.ToInt32(reader["Id"]);
                press.Name = reader["Name"].ToString();
                presses.Add(press);
            }
            connection.Close();
            return presses;

        }



        public async Task<ObservableCollection<Author>> GetAuthors()
        {
            try
            {
                if (connection.State != ConnectionState.Open)
                    connection.Open();

                SqlCommand command = connection.CreateCommand();
                command.CommandText = "SELECT * FROM Authors;";
                SqlDataReader reader = await command.ExecuteReaderAsync();
                ObservableCollection<Author> authors = new ObservableCollection<Author>();
                while (reader.Read())
                {
                    Author author = new Author();
                    author.Id = Convert.ToInt32(reader["Id"]);
                    author.FullName = reader["FirstName"].ToString() + " " + reader["LastName"].ToString();

                    authors.Add(author);
                }
                connection.Close();
                return authors;
            }
            catch (Exception)
            {

                connection.Close();
                return null;
            }
        }




        public async Task<ObservableCollection<Category>> GetCategories()
        {
            try
            {
                if (connection.State != ConnectionState.Open)
                    connection.Open();

                SqlCommand command = connection.CreateCommand();
                command.CommandText = "SELECT * FROM Categories;";
                SqlDataReader reader = await command.ExecuteReaderAsync();
                ObservableCollection<Category> categories = new ObservableCollection<Category>();
                while (reader.Read())
                {
                    Category category = new Category();
                    category.Id = Convert.ToInt32(reader["Id"]);
                    category.Name = reader["Name"].ToString();
                    categories.Add(category);
                }
                connection.Close();
                return categories;
            }
            catch (Exception)
            {

                connection.Close();
                return null;
            }
        }




        public async Task<ObservableCollection<Theme>> GetThemes()
        {
            try
            {
                if (connection.State != ConnectionState.Open)
                    connection.Open();

                SqlCommand command = connection.CreateCommand();
                command.CommandText = "SELECT * FROM Themes;";
                SqlDataReader reader = await command.ExecuteReaderAsync();
                ObservableCollection<Theme> themes = new ObservableCollection<Theme>();
                while (reader.Read())
                {
                    Theme theme = new Theme();
                    theme.Id = Convert.ToInt32(reader["Id"]);
                    theme.Name = reader["Name"].ToString();
                    themes.Add(theme);
                }
                connection.Close();
                return themes;
            }
            catch (Exception)
            {

                connection.Close();
                return null;
            }
        }

        public async Task<ObservableCollection<Book>> GetBooks()
        {

            if (connection.State != ConnectionState.Open)
                connection.Open();

            SqlCommand command = connection.CreateCommand();
            command.CommandText = "SELECT * FROM Books;";
            SqlDataReader reader = await command.ExecuteReaderAsync();
            ObservableCollection<Book> books = new ObservableCollection<Book>();

            while (reader.Read())
            {

                Book book = new Book();
                book.Id = Convert.ToInt32(reader["Id"]);

                book.Name = reader["Name"].ToString();
                book.Pages = Convert.ToInt32(reader["Pages"]);
                book.YearPress = Convert.ToInt32(reader["YearPress"]);
                book.Quantity = Convert.ToInt32(reader["Quantity"]);
                book.Comment = reader["Comment"].ToString();
                book.Author.Id = (Convert.ToInt32(reader["Id_Author"]));
                book.Category.Id = (Convert.ToInt32(reader["Id_Category"]));
                book.Press.Id = (Convert.ToInt32(reader["Id_Press"]));
                book.Theme.Id = (Convert.ToInt32(reader["Id_Themes"]));
                books.Add(book);
            }
            connection.Close();
            return books;




            //connection.Close();
            //return null;
        }

        public async Task<ObservableCollection<Student>> GetStudents()
        {
            if (connection.State != ConnectionState.Open)
                connection.Open();

            SqlCommand command = connection.CreateCommand();
            command.CommandText = "SELECT * FROM Students;";
            SqlDataReader reader = null;
            try
            {
                reader = await command.ExecuteReaderAsync();

            }
            catch (Exception ex)
            {

                throw;
            }
            ObservableCollection<Student> students = new ObservableCollection<Student>();
            while (reader.Read())
            {
                Student student = new Student();
                student.Id = Convert.ToInt32(reader["Id"]);
                student.LastName = reader["LastName"].ToString();
                student.FirstName = reader["FirstName"].ToString();
                students.Add(student);
            }
            connection.Close();
            return students;
        }

        public async Task<ObservableCollection<S_Card>> Get_Cards()
        {
            if (connection.State != ConnectionState.Open)
                connection.Open();
            SqlCommand command = connection.CreateCommand();
            command.CommandText = "SELECT * FROM S_Cards;";
            SqlDataReader reader = await command.ExecuteReaderAsync();
            ObservableCollection<S_Card> cards = new ObservableCollection<S_Card>();
            while (reader.Read())
            {
                S_Card _c = new S_Card();
                _c.Id = Convert.ToInt32(reader["Id"]);
                _c.Id_Book = Convert.ToInt32(reader["Id_Book"]);
                _c.Id_Student = Convert.ToInt32(reader["Id_Student"]);
                _c.DateOut = Convert.ToDateTime(reader["DateOut"]);
                if (!Convert.IsDBNull(reader["DateIn"])) _c.DateIn = Convert.ToDateTime(reader["DateIn"]);


                cards.Add(_c);
            }
            connection.Close();
            return cards;
        }

        public async Task InsertS_Cards(S_Card card)
        {
            if (connection.State != ConnectionState.Open)
                connection.Open();
            SqlCommand command = connection.CreateCommand();
            command.CommandText = "Insert into S_Cards(Id,Id_Student,Id_Book,DateOut,Id_Lib) " +
                                                " Values(@Id, @Id_Student, @Id_Book, @DateOut,2); " +
                                                " UPDATE Books " +
                                                " SET Quantity-= 1 " +
                                                 " WHERE Id = @Id_Book ;";
            command.Parameters.AddWithValue("@Id", card.Id);
            command.Parameters.AddWithValue("@Id_Student", card.Id_Student);
            command.Parameters.AddWithValue("@Id_Book", card.Id_Book);
            command.Parameters.AddWithValue("@DateOut", card.DateOut);
            // command.Parameters.AddWithValue("@DateIn", card.DateIn);
            try
            {
                await command.ExecuteNonQueryAsync();

            }
            catch (Exception ex)
            {

                throw;
            }
            var row = await command.ExecuteNonQueryAsync();

            connection.Close();
        }
        public async Task UpdateS_Cards(S_Card card)
        {
            if (connection.State != ConnectionState.Open)
                connection.Open();

            SqlCommand command = connection.CreateCommand();

            command.CommandText = "UPDATE S_Cards " +
                             " SET DateIn=@DateIn " +
                             " WHERE Id = @Id ;" +
                             " UPDATE Books " +
                             " SET Quantity+= 1 " +
                              " WHERE Id = @Id_Book ;";
            command.Parameters.AddWithValue("@Id", card.Id);
            command.Parameters.AddWithValue("@DateIn", card.DateIn);
            command.Parameters.AddWithValue("@Id_Book", card.Id_Book);
            try
            {

                await command.ExecuteNonQueryAsync();
            }
            catch (Exception ex)
            {

                throw;
            }
            // command.Parameters.AddWithValue("@DateIn", card.DateIn);
            connection.Close();
        }
        public async Task DELETEBook(int id)
        {
            if (connection.State != ConnectionState.Open)
                connection.Open();
            SqlCommand command = connection.CreateCommand();
            command.CommandText = " DELETE FROM Books " +
                                  " WHERE id=@Id;";
            command.Parameters.AddWithValue("@Id", id);
            int row = await command.ExecuteNonQueryAsync();

            connection.Close();
        }

        public async Task AddPress(Press press)
        {
            if (connection.State != ConnectionState.Open)
                connection.Open();

            SqlCommand command = connection.CreateCommand();
            command.CommandText = "INSERT INTO Press(Id,Name) " +
                " VALUES(@Id,@Name);";
            command.Parameters.AddWithValue("@Id", press.Id);
            command.Parameters.AddWithValue("@Name", press.Name);
            int row = await command.ExecuteNonQueryAsync();
            connection.Close();

        }
        public async Task AddAutor(Author author)
        {
            if (connection.State != ConnectionState.Open)
                connection.Open();

            SqlCommand command = connection.CreateCommand();
            command.CommandText = "INSERT INTO Authors(Id,LastName,FirstName) " +
                " VALUES(@Id,@lName,@FName);";
            string[] tmp = author.FullName.Split(' ');
            command.Parameters.AddWithValue("@Id", author.Id);
            command.Parameters.AddWithValue("@FName", tmp[0]);
            command.Parameters.AddWithValue("@lName", tmp[1]);
            int row = await command.ExecuteNonQueryAsync();

            connection.Close();

        }
        public async Task AddTheme(Theme theme)
        {
            if (connection.State != ConnectionState.Open)
                connection.Open();
            SqlCommand command = connection.CreateCommand();
            command.CommandText = "INSERT INTO Themes(Id,Name) " +
               " VALUES(@Id,@Name);";

            command.Parameters.AddWithValue("@Id", theme.Id);
            command.Parameters.AddWithValue("@Name", theme.Name);

            int row = await command.ExecuteNonQueryAsync();

            connection.Close();


        }
        public async Task AddCategory(Category category)
        {
            if (connection.State != ConnectionState.Open)
                connection.Open();
            SqlCommand command = connection.CreateCommand();

            command.CommandText = "INSERT INTO Categories(Id,Name) " +
               " VALUES(@Id,@Name);";

            command.Parameters.AddWithValue("@Id", category.Id);
            command.Parameters.AddWithValue("@Name", category.Name);

            int row = await command.ExecuteNonQueryAsync();

            connection.Close();




        }
        public async Task AddBook(Book book)
        {

            if (connection.State != ConnectionState.Open)
                connection.Open();
            SqlCommand command = connection.CreateCommand();

            command.CommandText = " INSERT INTO Books(Id,Name,Pages,YearPress,Quantity,Id_Themes,Id_Category,Id_Author,Id_Press) " +
               " VALUES(@Id,@Name,@Pages,@YearPress,@Quantity,@Id_Theme,@Id_Category,@Id_Author,@Id_Press); ";

            command.Parameters.AddWithValue("@Id", book.Id);
            command.Parameters.AddWithValue("@Name", book.Name);
            command.Parameters.AddWithValue("@Pages", book.Pages);
            command.Parameters.AddWithValue("@YearPress", book.YearPress);
            command.Parameters.AddWithValue("@Quantity", book.Quantity);
            command.Parameters.AddWithValue("@Id_Press", book.Press.Id);
            command.Parameters.AddWithValue("@Id_Category", book.Category.Id);
            command.Parameters.AddWithValue("@Id_Theme", book.Theme.Id);
            command.Parameters.AddWithValue("@Id_Author", book.Author.Id);

            int row = await command.ExecuteNonQueryAsync();

            connection.Close();
        }
    }
}

