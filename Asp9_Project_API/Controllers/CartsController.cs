﻿using Asp9_Project_API.HelperFunctions;
using Asp9_Project_Core.DTO_s;
using Asp9_Project_Core.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Asp9_Project_API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CartsController : ControllerBase
    {
        private readonly ICartRepository cartRepository;

        public CartsController(ICartRepository cartRepository)
        {
            this.cartRepository = cartRepository;
        }

        [HttpPost("add_to_cart")]
        public async Task<ActionResult> AddBulkItemsToCart([FromBody]CartItemDTO dto)
        {
            var token = Request.Headers["Authorization"].ToString().Replace("Bearer " , "");

            if (string.IsNullOrEmpty(token))
            {
                return Unauthorized("token is missing");
            }

            try
            {
                var userId = ExtractClaims.EtractUserId(token);
                if (!userId.HasValue)
                {
                    return Unauthorized("invalid user token");
                }
                var result = await cartRepository.AddBulkQuantityToCartAsync(dto, userId);
                if(result == "Item added to cart successfully")
                {
                    return Ok(result);
                }
                else
                {
                    return BadRequest(result);
                }
            }
            catch (Exception ex)
            {

                return Unauthorized("invalid Token: " + ex.Message);
            }

        }

        [HttpPost("add/one_to_cart")]
        public async Task<IActionResult> AddOneItemToCart([FromBody] CartItemDTO dto)
        {
            var token = Request.Headers["Authorization"].ToString().Replace("Bearer ", "");

            if (string.IsNullOrEmpty(token))
            {
                return Unauthorized("token is missing");
            }

            try
            {
                var userId = ExtractClaims.EtractUserId(token);
                if (!userId.HasValue)
                {
                    return Unauthorized("invalid user token");
                }
                var result = await cartRepository.AddOneQuantityToCartAsync(dto, userId);
                if (result == "Item added to cart successfully")
                {
                    return Ok(result);
                }
                else
                {
                    return BadRequest(result);
                }
            }
            catch (Exception ex)
            {

                return Unauthorized("invalid Token: " + ex.Message);
            }
        }


        [HttpGet("get/cart/allitems")]
        public async Task<IActionResult> GetAllItemsFromCart()
        {
            var token = Request.Headers["Authorization"].ToString().Replace("Bearer ", "");

            if (string.IsNullOrEmpty(token))
            {
                return Unauthorized("token is missing");
            }

            try
            {
                var userId = ExtractClaims.EtractUserId(token);
                if (!userId.HasValue)
                {
                    return Unauthorized("invalid user token");
                }
                
                var result = await cartRepository.getAllItemsFromCart(userId);
                
                if(result== null)
                {
                    return NotFound("No Items in your cart");
                }

                return Ok(result);
            }
            catch (Exception ex)
            {
                return BadRequest("Server Error " + ex.Message);
            }

        }
    }
}