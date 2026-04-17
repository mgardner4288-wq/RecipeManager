using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using RecipeManager.Data;
using RecipeManager.Models;

namespace RecipeManager.Pages
{
    public class IndexModel : PageModel
    {
        private readonly AppDbContext _context;

        public IndexModel(AppDbContext context)
        {
            _context = context;
        }

        [BindProperty]
        public string Email { get; set; } = "";

        [BindProperty]
        public string Password { get; set; } = "";

        [BindProperty]
        public string ProductName { get; set; } = "";

        [BindProperty]
        public decimal Price { get; set; }

        [BindProperty]
        public int Inventory { get; set; }

        [BindProperty]
        public int SelectedProductId { get; set; }

        [BindProperty]
        public int Quantity { get; set; }

        public string? UserEmail { get; set; }
        public string Message { get; set; } = "";
        public List<Product> Products { get; set; } = new();

        public bool IsLoggedIn => !string.IsNullOrEmpty(HttpContext.Session.GetString("UserEmail"));

        public void OnGet()
        {
            UserEmail = HttpContext.Session.GetString("UserEmail");
            Products = _context.Products.ToList();
        }

        public IActionResult OnPostLogin()
        {
            if (!string.IsNullOrWhiteSpace(Email) && !string.IsNullOrWhiteSpace(Password))
            {
                HttpContext.Session.SetString("UserEmail", Email);
            }

            return RedirectToPage();
        }

        public IActionResult OnPostLogout()
        {
            HttpContext.Session.Remove("UserEmail");
            return RedirectToPage();
        }

        public IActionResult OnPostCreateProduct()
        {
            var loggedInUser = HttpContext.Session.GetString("UserEmail");

            if (string.IsNullOrEmpty(loggedInUser))
            {
                return RedirectToPage();
            }

            var product = new Product
            {
                ProductName = ProductName,
                Price = Price,
                Inventory = Inventory,
                SellerEmail = loggedInUser
            };

            _context.Products.Add(product);
            _context.SaveChanges();

            return RedirectToPage();
        }

        public IActionResult OnPostPurchase()
        {
            var loggedInUser = HttpContext.Session.GetString("UserEmail");

            if (string.IsNullOrEmpty(loggedInUser))
            {
                return RedirectToPage();
            }

            var product = _context.Products.FirstOrDefault(p => p.Id == SelectedProductId);

            if (product == null)
            {
                Message = "Product not found.";
                LoadPageData();
                return Page();
            }

            if (Quantity <= 0)
            {
                Message = "Invalid quantity.";
                LoadPageData();
                return Page();
            }

            if (Quantity > product.Inventory)
            {
                Message = "Not enough inventory.";
                LoadPageData();
                return Page();
            }

            var purchase = new Purchase
            {
                ProductId = product.Id,
                BuyerEmail = loggedInUser,
                Quantity = Quantity
            };

            product.Inventory -= Quantity;

            _context.Purchases.Add(purchase);
            _context.SaveChanges();

            Message = "Purchase successful!";
            LoadPageData();
            return Page();
        }
        public IActionResult OnPostCreateAccount()
    {
    if (!string.IsNullOrWhiteSpace(Email) && !string.IsNullOrWhiteSpace(Password))
    {
        HttpContext.Session.SetString("UserEmail", Email);
        Message = "Account created successfully!";
    }
    else
    {
        Message = "Email and password are required.";
    }

    UserEmail = HttpContext.Session.GetString("UserEmail");
    Products = _context.Products.ToList();
    return Page();
     }

        private void LoadPageData()
        {
            UserEmail = HttpContext.Session.GetString("UserEmail");
            Products = _context.Products.ToList();
        }
    }
}