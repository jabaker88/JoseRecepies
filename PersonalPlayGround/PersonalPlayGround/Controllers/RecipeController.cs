﻿using System.Collections.Generic;
using System.IO;
using System.Web.Mvc;
using PersonalPlayGround.ClientInfo;
using PersonalPlayGround.ClientInfo.Repository;
using PersonalPlayGround.Documents;
using PersonalPlayGround.RecipeData;
using PersonalPlayGround.RecipeData.Service;
using PersonalPlayGround.RecipeReviewData;
using PersonalPlayGround.RecipeReviewData.Service;

namespace PersonalPlayGround.Controllers
{
    [Authorize]
    public class RecipeController : BaseController
    {
        private readonly IRecipeService _recipeService;
        private readonly IRecipeReviewService _recipeReviewService;
        private readonly IClientService _clientService;
        public RecipeController() { }
        public RecipeController(IRecipeService recipeService, IRecipeReviewService recipeReviewService, IClientService clientService)
        {
            _recipeService = recipeService;
            _recipeReviewService = recipeReviewService;
            _clientService = clientService;
        }

        // GET: Recipe
        public ActionResult Index()
        {
            List<Recipe> recipes = _recipeService.GetActiveRecipes();

            return View(recipes);
        }

        // GET: Recipe/Id
        public ActionResult GetRecipeById(int? recipeId)
        {
            if (recipeId == null)
            {
                return Index();
            }
            
            // Get Recipe
            Recipe recipe = _recipeService.GetRecipeById(recipeId.Value);

            // check if recipe exists
            if(recipe == null)
            {
                return RedirectToAction("Index", "Recipe");
            }

            // check if recipe is active
            if(!recipe.Active)
            {
                return RedirectToAction("Index", "Recipe");
            }

            if(string.IsNullOrEmpty(recipe.ImageURL))
            {
                recipe.ImageURL = Path.Combine("~", FileDirectory.RecipesDatabaseFolder, FileDirectory.Image_Needed);
            }

            // Get Recipe Rating/Review
            recipe.Ratings = _recipeReviewService.GetRecipeReviewsByRecipeId(recipeId.Value);

            foreach(RecipeReview recipeReview in recipe.Ratings)
            {
                // Get Client by Recipe Review
                recipeReview.Client = _clientService.GetClientById(recipeReview.ClientId);
            }

            // Check if client left a review 
            Client client = _clientService.GetClientByUsername(User.Identity.Name);
            bool clientLeftReview = _clientService.ClientLeftReview(client.Id, recipeId.Value);

            ViewBag.ClientLeftReview = clientLeftReview;
            return View(recipe);
        }

    }
}