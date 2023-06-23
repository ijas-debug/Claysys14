using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace UserValidateForm
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void btnNew_Click(object sender, EventArgs e)
        {
            customerBindingSource.DataSource = new Customer();
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            // Retrieve the BindingSource from the form's components
            BindingSource customerBindingSource = customerBindingSource1;

            customerBindingSource.EndEdit();
            Customer customer = customerBindingSource.Current as Customer;
            if (customer != null)
            {
                ValidationContext context = new ValidationContext(customer, null, null);
                IList<ValidationResult> errors = new List<ValidationResult>();
                if (!Validator.TryValidateObject(customer, context, errors, true))
                {
                    foreach (ValidationResult result in errors)
                    {
                        MessageBox.Show(result.ErrorMessage, "Message", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        return;
                    }
                }
            }
        }
    }
}
