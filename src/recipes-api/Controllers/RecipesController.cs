using Microsoft.AspNetCore.Mvc;
using recipes_api.Services;
using recipes_api.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json;
using System.Text.Json.Serialization;
using Microsoft.AspNetCore.Razor.TagHelpers;

namespace recipes_api.Controllers;

[ApiController]
[Route("recipe")]
public class RecipesController : ControllerBase
{    
    public readonly IRecipeService _service;
    
    public RecipesController(IRecipeService service)
    {
        this._service = service;        
    }

    // 1 - Sua aplicação deve ter o endpoint GET /recipe
    //Read
    [HttpGet]
    public IActionResult Get()
    {
        return Ok(_service.GetRecipes());
    }

    // 2 - Sua aplicação deve ter o endpoint GET /recipe/:name
    //Read
    [HttpGet("{name}", Name = "GetRecipe")]
    public IActionResult Get(string name)
    {
        return Ok(_service.GetRecipe(name));
    }

    // 3 - Sua aplicação deve ter o endpoint POST /recipe
    [HttpPost]
    public IActionResult Create([FromBody]Recipe recipe)
    {
        _service.AddRecipe(recipe);
        return Created("", recipe);
    }

    // 4 - Sua aplicação deve ter o endpoint PUT /recipe
    [HttpPut("{name}")]
    public IActionResult Update(string name, [FromBody]Recipe recipe)
    {        
        try
        {
            bool existingRecipe = _service.RecipeExists(name);
            if (existingRecipe)
            {
                _service.UpdateRecipe(recipe);  
                return NoContent();
            }
            throw new Exception("Recipe not found");
        }
        catch (Exception e)
        {
            return BadRequest(e.Message);
        }        
    }

    // 5 - Sua aplicação deve ter o endpoint DEL /recipe
    [HttpDelete("{name}")]
    public IActionResult Delete(string name)
    {
        try
        {
            bool existingRecipe = _service.RecipeExists(name);
            if (existingRecipe)
            {
                _service.DeleteRecipe(name);
                return NoContent();
            }
            throw new Exception("Recipe not found");
        }
        catch (Exception e)
        {
            return BadRequest(e.Message);
        }
    }    
}
