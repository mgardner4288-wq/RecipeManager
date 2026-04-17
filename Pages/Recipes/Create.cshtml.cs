using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.ComponentModel.DataAnnotations;
using RecipeManager.Data;
using RecipeManager.Models;

namespace RecipeManager.Pages.Recipes;

public class CreateModel : PageModel
{
    private readonly AppDbContext _context;

    public CreateModel(AppDbContext context)
    {
        _context = context;
    }

    [BindProperty]
    public InputModel Input { get; set; } = new();

    public class InputModel
    {
        [Required(ErrorMessage = "Name is required.")]
        [MaxLength(100)]
        public string Name { get; set; } = "";

        [MaxLength(500)]
        public string Description { get; set; } = "";

        [Required(ErrorMessage = "Ingredients are required.")]
        [MinLength(10)]
        public string Ingredients { get; set; } = "";

        [Required(ErrorMessage = "Instructions are required.")]
        [MinLength(10)]
        public string Instructions { get; set; } = "";
    }

    public void OnGet()
    {
        // Render empty form
    }

    public async Task<IActionResult> OnPostAsync()
    {
        if (!ModelState.IsValid)
            return Page();

        var recipe = new Recipe
        {
            Name = Input.Name.Trim(),
            Description = Input.Description?.Trim() ?? "",
            Ingredients = Input.Ingredients.Trim(),
            Instructions = Input.Instructions.Trim()
        };

        _context.Recipes.Add(recipe);
        await _context.SaveChangesAsync();

        return RedirectToPage("./Index");
    }
}
