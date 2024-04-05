using ContactManager.Server.Contexts;
using ContactManager.Server.Exceptions;
using ContactManager.Server.Models.Dto;
using ContactManager.Server.Services;
using Microsoft.AspNetCore.Mvc;

namespace ContactManager.Server.Controllers;

[ApiController]
[Route("/")]
public class ContactController(ContactContext contactContext) : Controller
{
    private readonly ContactService _contactService = new(contactContext);

    [HttpGet("api/get-all")]
    public ActionResult<IEnumerable<ContactOutputDto>> GetAll()
    {
        try
        {
            IEnumerable<ContactOutputDto> contacts = _contactService.GetAllContacts();

            return this.Ok(contacts);
        }
        catch (Exception e)
        {
            return this.StatusCode(500, e.Message);
        }
    }

    [HttpPost("api/add")]
    public ActionResult Add([FromBody] ContactInputDto inputDto)
    {
        try
        {
            _contactService.CreateContact(inputDto);

            return this.Ok();
        }
        catch (Exception e)
        {
            return this.StatusCode(500, e.Message);
        }
    }

    [HttpPost("api/add-csv")]
    public ActionResult AddCsv([FromBody] string csvString)
    {
        try
        {
            _contactService.CreateFromCsv(csvString);

            return this.Ok();
        }
        catch (ArgumentException e)
        {
            return this.BadRequest(e.Message);
        }
        catch (Exception e)
        {
            return this.StatusCode(500, e.Message);
        }
    }

    [HttpPut("api/update")]
    public ActionResult Update([FromBody] ContactUpdateDto updateDto)
    {
        try
        {
            _contactService.EditContact(updateDto);

            return this.Ok();
        }
        catch (ContactNotFound e)
        {
            return this.NotFound(e.Message);
        }
        catch (Exception e)
        {
            return this.StatusCode(500, e.Message);
        }
    }

    [HttpDelete("api/delete/{id}")]
    public ActionResult Delete([FromRoute] long id)
    {
        try
        {
            _contactService.DeleteContact(id);

            return this.Ok();
        }
        catch (ContactNotFound e)
        {
            return this.NotFound(e.Message);
        }
        catch (Exception e)
        {
            return this.StatusCode(500, e.Message);
        }
    }
}
