using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Data.SqlClient;

namespace MyStore.Pages.Clients
{
    public class CreateModel : PageModel
    {
        public ClientInfo clientInfo = new ClientInfo();
        public String errorMessage = "";
		public String successMesssage = "";

        public void OnGet()
        {
        }

		public void OnPost()
		{
			clientInfo.name = Request.Form["name"];
			clientInfo.email = Request.Form["email"];
			clientInfo.phone = Request.Form["Phone"];
			clientInfo.address = Request.Form["address"];

			if (clientInfo.name.Length == 0 || clientInfo.email.Length == 0 ||
				clientInfo.phone.Length == 0 || clientInfo.address.Length == 0)
			{
				errorMessage = "All the fields are required";
				return;
			}

			//save the new client into the database
			try 
			{
				String connectionString = "Data Source=LAPTOP-CJ6SAAMH\\SQLEXPRESS;Initial Catalog=UIPATH;User ID=sa;Password=ijas10";
				using (SqlConnection connection = new SqlConnection(connectionString)) 
				{ 
					connection.Open();
					String sql = "INSERT INTO Clients" +
						"(name,email,phone,address) VALUES " +
						"(@name,@email,@phone,@address);";

					using(SqlCommand command =new SqlCommand(sql, connection))
					{
						command.Parameters.AddWithValue("@name", clientInfo.name);
						command.Parameters.AddWithValue("@email", clientInfo.email);
						command.Parameters.AddWithValue("@phone", clientInfo.phone);
						command.Parameters.AddWithValue("@address", clientInfo.email);

						command.ExecuteNonQuery();

					}
				}
			}
			catch (Exception ex) 
			{ 
				errorMessage = ex.Message;
				return;
			}
			clientInfo.name = ""; clientInfo.email = ""; clientInfo.phone = ""; clientInfo.address = "";
			successMesssage = "New Client Added Correctly";

			Response.Redirect("/Clients/Index");
		}

	}
}
