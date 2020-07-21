using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using TopsyTurvyCakes.Models;

namespace TopsyTurvyCakes.Pages.Admin
{
    public class AddEditRecipeModel : PageModel
    {
        private readonly IRecipesService recipesService;

        [FromRoute]  //nullable long because it is optional
        public long? Id { get; set; }

        public bool IsNewRecipe
        {
            get { return Id == null; }
        }
        //auto bind, With this property razor pages will iterate to match
        //name of each property with the value of the request. The key is to
        //make the names and fields match
        [BindProperty]
        public Recipe Recipe { get; set; }
       
        public AddEditRecipeModel(IRecipesService recipesService)
        {
            this.recipesService = recipesService;
        }
        public async Task OnGet()
        {
            Recipe = await recipesService.FindAsync(Id.GetValueOrDefault()) ?? new Recipe();
        }

        public async Task<IActionResult> OnPostAsync()
        {
            Recipe.Id = Id.GetValueOrDefault();
            await recipesService.SaveAsync(Recipe);
            return RedirectToPage("/Recipe", new { id = Id });
        }
    }
}