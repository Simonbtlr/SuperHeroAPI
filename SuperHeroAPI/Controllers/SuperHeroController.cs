using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SuperHeroAPI.Data;

namespace SuperHeroAPI.Controllers;

[Route("api/[controller]")]
[ApiController]
public class SuperHeroController : ControllerBase
{
    private readonly DataContext _context;

    public SuperHeroController(DataContext context)
    {
        _context = context;
    }

    [HttpGet]
    public async Task<ActionResult<List<SuperHero>>> GetSuperHeroes()
    {
        return Ok(await _context.SuperHeroes.ToListAsync());
    }

    [HttpPost]
    public async Task<ActionResult<List<SuperHero>>> CreateSuperHero(SuperHero superHero)
    {
        _context.SuperHeroes.Add(superHero);
        await _context.SaveChangesAsync();

        return await GetSuperHeroes();
    }

    [HttpPut]
    public async Task<ActionResult<List<SuperHero>>> UpdateSuperHero(SuperHero superHero)
    {
        var hero = await _context.SuperHeroes.FindAsync(superHero.Id);

        if (hero is null)
            return BadRequest("Hero not found");

        hero.Name = superHero.Name;
        hero.Place = superHero.Place;
        hero.FirstName = superHero.FirstName;
        hero.LastName = superHero.LastName;

        await _context.SaveChangesAsync();

        return await GetSuperHeroes();
    }

    [HttpDelete("{id}")]
    public async Task<ActionResult<List<SuperHero>>> DeleteSuperHero(int id)
    {
        var hero = await _context.SuperHeroes.FindAsync(id);

        if (hero is null)
            return BadRequest("Hero not found");

        _context.SuperHeroes.Remove(hero);
        await _context.SaveChangesAsync();

        return await GetSuperHeroes();
    }
}