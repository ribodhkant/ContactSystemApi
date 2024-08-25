using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json.Linq;
using static System.Net.Mime.MediaTypeNames;
using System;
using System.Text.Json;
using ContactManagementSystem;
using System.ComponentModel.Design;
using ContactManagementSystem.BusinessLib;


namespace WebApplication1.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class ContactManagement : ControllerBase
    {

        private readonly ILogger<ContactManagement> _logger;
        private readonly IConfiguration _configuration;
            private readonly IContactService _contactService;
        public ContactManagement(ILogger<ContactManagement> logger, IConfiguration configuration,IContactService contactService)
        {
            _logger = logger;
            _configuration = configuration;
            _contactService = contactService;
        }
        /// <summary>
        /// For getting All contact list
        /// </summary>
        /// <returns>ContactList</returns>
        [HttpGet]
        [Route("GetContactList")]
        public Response GetContactList()
        {
            return _contactService.GetContactList();
        }

        /// <summary>
        /// For new contact insert and existing contactinfo update
        /// </summary>
        /// <returns> responce message </returns>
        [HttpPost]
        [Route("InsertAndUpdateContact")]
        public Response InsertAndUpdateContact(ContactDto contactDto)
        {
            return _contactService.InsertAndUpdateContact(contactDto);           
        }
        /// <summary>
        /// For detele contact
        /// </summary>
        /// <returns> Response message</returns>
        [HttpDelete]
        [Route("DeleteContact")]
        public Response DeleteContact(int id)
        {
            return _contactService.DeleteContact(id);
        }
    }
}


