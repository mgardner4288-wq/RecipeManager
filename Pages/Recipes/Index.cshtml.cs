using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using RecipeManager.Data;
using RecipeManager.Models;

namespace RecipeManager.Pages.Recipes;

public class IndexModel : PageModel
{
    private readonly AppDbContext _context;

    public IndexModel(AppDbContext context)
    {
        _context = context;
    }

    public List<Recipe> Recipes { get; private set;} = new();

    public async Task OnGetAsync()
    {
        Recipes = await _context.Recipes
        .OrderBy(b => b.Name)
        .ToListAsync();
    }
}