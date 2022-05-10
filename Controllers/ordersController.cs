using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Asm_web_1.Data;
using Asm_web_1.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.Data.SqlClient;

namespace Asm_web_1.Controllers
{
    public class ordersController : Controller
    {
        private readonly ApplicationDbContext _context;

        public ordersController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: orders
        public async Task<IActionResult> Index()
        {
            return View(await _context.orders.ToListAsync());
        }

        // GET: orders/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var orders = await _context.orders
                .FirstOrDefaultAsync(m => m.Id == id);
            if (orders == null)
            {
                return NotFound();
            }

            return View(orders);
        }

        // GET: orders/Create
        public async Task<IActionResult> Create(int? id)
        {
            var book = await _context.Book.FindAsync(id);

            return View(book);
        }


        // POST: orders/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(int bookId, string bookName,int quantity)
        {
            orders order = new orders();
            order.bookId = bookId;
            order.quantity = quantity;
            order.bookName = bookName; 

            order.userid = Convert.ToInt32(HttpContext.Session.GetString("userid")); 
            order.orderdate = DateTime.Today;
            SqlConnection conn = new SqlConnection("Server=(localdb)\\mssqllocaldb;Database=aspnet-Asm_web_1-948E3AE2-9B41-4E1A-B7DB-38CF35D94089;Trusted_Connection=True;MultipleActiveResultSets=true");
            string sql;
            sql = "UPDATE book  SET bookquantity  = bookquantity   - '" + order.quantity + "'  where (id ='" + order.bookId + "' )";
            SqlCommand comm = new SqlCommand(sql, conn);
            conn.Open();
            comm.ExecuteNonQuery();


            _context.Add(order);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(myorders));
           
        }

       

        public async Task<IActionResult> myorders()
        {

            int userid = Convert.ToInt32(HttpContext.Session.GetString("userid"));
            var orItems = await _context.orders.FromSqlRaw("select *  from orders where  userid = '" + userid + "'  ").ToListAsync();
            return View(orItems);

        }

        public async Task<IActionResult> customerOrders(int? id)
        {


            var orItems = await _context.orders.FromSqlRaw("select *  from orders where  userid = '" + id + "'  ").ToListAsync();
            return View(orItems);

        }

        public async Task<IActionResult> customerreport()
        {
            var orItems = await _context.report.FromSqlRaw("select usersaccounts.id as Id, name as customername, sum (quantity * Price)  as total from book, orders,usersaccounts  where usersaccounts.id = orders.userid  and bookid= book.Id group by name,usersaccounts.id ").ToListAsync();
            return View(orItems);

        }





        // GET: orders/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var orders = await _context.orders.FindAsync(id);
            if (orders == null)
            {
                return NotFound();
            }
            return View(orders);
        }

        // POST: orders/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,bookId,bookName,userid,quantity,orderdate")] orders orders)
        {
            if (id != orders.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(orders);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ordersExists(orders.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(myorders));
            }
            return View(orders);
        }

        // GET: orders/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var orders = await _context.orders
                .FirstOrDefaultAsync(m => m.Id == id);
            if (orders == null)
            {
                return NotFound();
            }

            return View(orders);
        }

        // POST: orders/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var orders = await _context.orders.FindAsync(id);
            _context.orders.Remove(orders);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(myorders));
        }

        private bool ordersExists(int id)
        {
            return _context.orders.Any(e => e.Id == id);
        }
    }
}
