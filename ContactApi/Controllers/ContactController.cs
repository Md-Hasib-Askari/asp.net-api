using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ContactApi.DTOs;
using ContactApi.Models;
using ContactApi.Services;

namespace ContactApi.Controllers;

[Authorize]
[ApiController]
[Route("api/[controller]")]
public class ContactsController : ControllerBase
{
    private readonly IContactService service;

    public ContactsController(IContactService service)
    {
        this.service = service;
    }

    [HttpPost]
    public IActionResult Add([FromBody] CreateContactDto dto)
    {
        try
        {
            if (dto == null)
                return BadRequest("Contact cannot be null.");

            var contact = new Contact(dto.Name, dto.Email);
            service.Add(contact);
            return Ok("Contact added successfully.");
        }
        catch (Exception ex)
        {
            return BadRequest(ex.Message);
        }
    }


    [HttpGet]
    public IActionResult GetAll()
    {
        try
        {
            var contacts = service.GetAll();
            var response = contacts.Select(c => new ContactResponseDto(c.Name, c.Email));
            return Ok(response);
        }
        catch (Exception ex)
        {
            return BadRequest(ex.Message);
        }
    }

    [HttpGet("search")]
    public IActionResult Search(
        [FromQuery]
        string? keyword = null,
        int pageNumber = 1,
        int pageSize = 10,
        string sortBy = "name",
        string sortOrder = "asc")
    {
        try
        {
            var contacts = service.GetPaged(keyword, pageNumber, pageSize, sortBy, sortOrder);
            var response = contacts.Select(c => new ContactResponseDto(c.Name, c.Email));
            return Ok(response);
        }
        catch (Exception ex)
        {
            return BadRequest(ex.Message);
        }
    }
}