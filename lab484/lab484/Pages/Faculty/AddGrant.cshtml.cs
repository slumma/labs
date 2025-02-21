using InventoryManagement.Pages.DB;
using lab484.Pages.Data_Classes;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Data.SqlClient;
using System.Diagnostics;

namespace lab484.Pages.Faculty
{
    public class AddGrantModel : PageModel
    {
        [BindProperty]
        public GrantSimple newGrant { get; set; }
        public required List<GrantSupplier> SupplierList { get; set; }

        public void OnGet()
        {
            SupplierList = new List<GrantSupplier>();
            SqlDataReader SupplierReader = DBClass.GrantSupplierReader();

            while (SupplierReader.Read())
            {
                SupplierList.Add(new GrantSupplier
                {
                    SupplierID = Int32.Parse(SupplierReader["SupplierID"].ToString()),
                    SupplierName = SupplierReader["SupplierName"].ToString(),
                    OrgType = SupplierReader["OrgType"].ToString(),
                    BusinessAddress = SupplierReader["BusinessAddress"].ToString()
                });
            }
            

            SupplierReader.Close();
            DBClass.DBConnection.Close();
        }

        

        public IActionResult OnPost()
        {
            // Load the SupplierList to ensure it is not null or empty
            SqlDataReader SupplierReader = DBClass.GrantSupplierReader();
            SupplierList = new List<GrantSupplier>();

            if (SupplierReader.HasRows)
            {
                while (SupplierReader.Read())
                {
                    SupplierList.Add(new GrantSupplier
                    {
                        SupplierID = Int32.Parse(SupplierReader["SupplierID"].ToString()),
                        SupplierName = SupplierReader["SupplierName"].ToString(),
                        OrgType = SupplierReader["OrgType"].ToString(),
                        BusinessAddress = SupplierReader["BusinessAddress"].ToString()
                    });
                }
            }

            SupplierReader.Close();
            DBClass.DBConnection.Close();

            if (ModelState.IsValid)
            {
                // Print the selected supplier name for debugging
                Trace.WriteLine("Selected Supplier: " + newGrant.Supplier);

                // Get the SupplierID from the selected SupplierName
                GrantSupplier selectedSupplier = SupplierList.FirstOrDefault(s => s.SupplierName == newGrant.Supplier);

                if (selectedSupplier != null)
                {
                    int supplierID = selectedSupplier.SupplierID;
                    Trace.WriteLine("Supplier ID: " + supplierID); // Print SupplierID for debugging

                    // Insert the new grant with the correct SupplierID
                    DBClass.InsertGrant(newGrant, supplierID);
                    return RedirectToPage("FacultyLanding");
                }
                else
                {
                    ModelState.AddModelError("", "Invalid Supplier Selected");
                    Trace.WriteLine("Invalid Supplier Selected");
                }
            }

            // If there are validation errors, reload the supplier list
            OnGet();
            return Page();
        }



    }
}
