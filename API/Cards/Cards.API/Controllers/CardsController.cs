using Cards.API.Data;
using Cards.API.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Cards.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class CardsController : Controller
    {
        private readonly CardsDbContext cardsDbcontext;
        public CardsController(CardsDbContext cardsDbcontext)
        {
            this.cardsDbcontext = cardsDbcontext;
        }
       //Get All Cards
       [HttpGet]
       public async Task<IActionResult> GetAllCards()
        {
            var cards = await cardsDbcontext.Cards.ToListAsync(); 
            return Ok(cards);
        }

        //Get single card
        [HttpGet]
        [Route("{id:guid}")]
        public async Task<IActionResult> GetCard([FromRoute] Guid id)
        {
            var card = await cardsDbcontext.Cards.FirstOrDefaultAsync(x => x.Id == id);
            if(card != null)
            {
                return Ok(card);
            }
            return NotFound("Card Not Found");
        }

        //Add card
        [HttpPost]
        public async Task<IActionResult> AddCard([FromBody] Card card)
        {
            card.Id = Guid.NewGuid();   
            await cardsDbcontext.Cards.AddAsync(card); 
            await cardsDbcontext.SaveChangesAsync();

            return CreatedAtAction(nameof(GetCard), new {id = card.Id }, card);
        }

        //update a card
        [HttpPut]
        [Route("{id:guid}")]
        public async Task<IActionResult> UpdateCard([FromRoute] Guid id, [FromBody] Card card)
        {
            var existingcard = await cardsDbcontext.Cards.FirstOrDefaultAsync(x => x.Id == id);
            if (existingcard != null)
            {
                existingcard.CardholderName = card.CardholderName;
                existingcard.CardNumber = card.CardNumber;
                existingcard.ExpiryMonth = card.ExpiryMonth;
                existingcard.ExpiryYear = card.ExpiryYear;
                existingcard.CVC = card.CVC;
                await cardsDbcontext.SaveChangesAsync();
                return Ok(existingcard);
            }
            return NotFound("Card not found");        
        }

        [HttpDelete]
        [Route("{id:guid}")]
        public async Task<IActionResult> DeleteCard([FromRoute] Guid id)
        {
            var existingcard = await cardsDbcontext.Cards.FirstOrDefaultAsync(x => x.Id == id);
            if (existingcard != null)
            {
                cardsDbcontext.Remove(existingcard);
                 await cardsDbcontext.SaveChangesAsync();
                return Ok(existingcard);
            }
            return NotFound("Card not found");
        }
    }
}
