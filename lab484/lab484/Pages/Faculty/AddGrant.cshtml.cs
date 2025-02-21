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
        public List<GrantSupplier> SupplierList { get; set; } = new List<GrantSupplier>();

        public void OnGet()
        {
            SupplierList = LoadSuppliers();
        }

        public IActionResult OnPostAddGrant()
        {
            SupplierList = LoadSuppliers();

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

            Trace.WriteLine("Error");

            return Page();
        }

        public IActionResult OnPostClear()
        {
            ModelState.Clear();
            newGrant = new GrantSimple
            {
                GrantID = newGrant.GrantID, // Keep the GrantID the same
                Supplier = string.Empty,
                Project = string.Empty,
                Amount = 0,
                Category = string.Empty,
                Status = string.Empty,
                Description = string.Empty,
                SubmissionDate = DateTime.Now,
                AwardDate = DateTime.Now
            };

            OnGet(); // just to reload the supplier list

            // Return the page with the cleared model
            return Page();
        }

        public IActionResult OnPostPopulate()
        {
            ModelState.Clear();
            newGrant = new GrantSimple
            {
                Supplier = "TechCorp",
                Project = "Education",
                Amount = 1000000,
                Category = "Federal",
                Status = "Pending",
                Description = "It's for the kids!",
                SubmissionDate = DateTime.Now,
                AwardDate = DateTime.Now
            };

            Trace.WriteLine("Populated");

            SupplierList = LoadSuppliers(); // Reload supplier list

            return Page();
        }

        private List<GrantSupplier> LoadSuppliers()
        {
            var suppliers = new List<GrantSupplier>();
            using (SqlDataReader reader = DBClass.GrantSupplierReader())
            {
                while (reader.Read())
                {
                    suppliers.Add(new GrantSupplier
                    {
                        SupplierID = int.Parse(reader["SupplierID"].ToString()),
                        SupplierName = reader["SupplierName"].ToString(),
                        OrgType = reader["OrgType"].ToString(),
                        BusinessAddress = reader["BusinessAddress"].ToString()
                    });
                }
            }

            DBClass.DBConnection.Close();

            return suppliers;
        }
    }
}
