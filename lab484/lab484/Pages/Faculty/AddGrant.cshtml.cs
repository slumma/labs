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
            // made a method at the bottom of the file so i dont have to copy and paste it a bunch of times 
            SupplierList = LoadSuppliers();
        }

        // executes when AddGrant is added, iinserts it into the db
        public IActionResult OnPostAddGrant()
        {
            SupplierList = LoadSuppliers();

            // if everything is valid in the form, add to db with the supplierID selected 
            if (ModelState.IsValid)
            {
                // used AI for help with this, it associates the SupplierName in the list with the SupplierID
                GrantSupplier selectedSupplier = SupplierList.FirstOrDefault(s => s.SupplierName == newGrant.Supplier);
                int supplierID = selectedSupplier.SupplierID;

                DBClass.InsertGrant(newGrant, supplierID);
                return RedirectToPage("FacultyLanding");
            }

            return Page();
        }

        // clears everything from form 
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

            // if i wasnt reloading the supplier list it would throw an error, this was an easy fix 
            OnGet(); // just to reload the supplier list

            // Return the page with the cleared model
            return Page();
        }


        // populate button 
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


        // make a method so i dont have to copy and paste it each time 
        // loads all of the suppliers in the db into the supplierlist so it can be shown in the dropdown menu 
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
