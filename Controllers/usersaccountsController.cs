using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Asm_web_1.Data;
using Microsoft.Data.SqlClient;
using Microsoft.AspNetCore.Http;

namespace Asm_web_1.Models
{
    public class usersaccountsController : Controller
    {
        private readonly ApplicationDbContext _context;

        public usersaccountsController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: usersaccounts
        public async Task<IActionResult> Index()
        {
            return View(await _context.usersaccounts.ToListAsync());
        }

        // GET: usersaccounts/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var usersaccounts = await _context.usersaccounts
                .FirstOrDefaultAsync(m => m.Id == id);
            if (usersaccounts == null)
            {
                return NotFound();
            }

            return View(usersaccounts);
        }

        // GET: usersaccounts/Create
        public IActionResult Create()
        {
            return View();
        }
        public IActionResult AdminCreate()
        {
            return View();
        }

        
        public IActionResult login()
        {
            return View();
        }

        [HttpPost, ActionName("login")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> login(string na, string pa)
        {
            SqlConnection conn1 = new SqlConnection("Server=(localdb)\\mssqllocaldb;Database=aspnet-Asm_web_1-948E3AE2-9B41-4E1A-B7DB-38CF35D94089;Trusted_Connection=True;MultipleActiveResultSets=true");
            string sql;
            sql = "SELECT * FROM usersaccounts where name ='" + na + "' and  pass ='" + pa + "' ";
            SqlCommand comm = new SqlCommand(sql, conn1);
            conn1.Open();
            SqlDataReader reader = comm.ExecuteReader();

            if (reader.Read())
            {
                string role = (string)reader["role"];
                string id = Convert.ToString((int)reader["Id"]);
                HttpContext.Session.SetString("Name", na);
                HttpContext.Session.SetString("Role", role);
                HttpContext.Session.SetString("userid", id);
                reader.Close();
                conn1.Close();
                if (role == "customer")
                    return RedirectToAction("catalogue", "books");

                else
                    return RedirectToAction("Index", "books");

            }
            else
            {
                ViewData["Message"] = "wrong user name password";
                return View();
            }
        }


        // POST: usersaccounts/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,name,pass,email")] usersaccounts usersaccounts)
        {
            usersaccounts.role = "customer";
                _context.Add(usersaccounts);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(login));
            
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> AdminCreate([Bind("Id,name,pass,email")] usersaccounts usersaccounts)
        {
            usersaccounts.role = "customer";
            _context.Add(usersaccounts);
            await _context.SaveChangesAsync();
            return RedirectToAction("Index");

        }

        // GET: usersaccounts/Edit/5
        public async Task<IActionResult> Edit()
        {

            int id = Convert.ToInt32(HttpContext.Session.GetString("userid"));
            var usersaccounts = await _context.usersaccounts.FindAsync(id);
            
            return View(usersaccounts);
        }

        // POST: usersaccounts/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,name,pass,role,email")] usersaccounts usersaccounts)
        {
          
                {
                    _context.Update(usersaccounts);
                    await _context.SaveChangesAsync();
              
                return RedirectToAction(nameof(login));
            }
            return View(usersaccounts);
        }

        // GET: usersaccounts/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var usersaccounts = await _context.usersaccounts
                .FirstOrDefaultAsync(m => m.Id == id);
            if (usersaccounts == null)
            {
                return NotFound();
            }

            return View(usersaccounts);
        }

        // POST: usersaccounts/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var usersaccounts = await _context.usersaccounts.FindAsync(id);
            _context.usersaccounts.Remove(usersaccounts);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool usersaccountsExists(int id)
        {
            return _context.usersaccounts.Any(e => e.Id == id);
        }
    }
}
