using Azure.Storage.Blobs;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using Nancy.IO;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using WhatsAppAPI.Data;
using WhatsAppAPI.IServices;
using WhatsAppAPI.Models;
using WhatsAppAPI.Services;
using WhatsAppAPI.ViewModels.WebHook;
using WhatsAppAPI.WhatsAppSettings;
using static System.Net.WebRequestMethods;

namespace WhatsAppAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class WhatsappController : Controller
    {
        private readonly IWhatsAppService _whatsAppService;
        private readonly ICustomerResponses _customerResponses;
        private readonly IRegistrationService _registrationService;

        public WhatsappController(IWhatsAppService whatsAppService, ICustomerResponses customerResponses, IRegistrationService registrationService)
        {
            _whatsAppService = whatsAppService;
            _customerResponses = customerResponses;
            _registrationService = registrationService;
        }



        [HttpPost("send-welcome-message")]
        public async Task<IActionResult> SendTextMessage(string mobile, string message)
        {
            var result = await _whatsAppService.SendRandomMessage(mobile, message);
            return Ok("Sent successfully");
        }


        [HttpGet("get-media")]
        public async Task<IActionResult> GetMediaByUrl()
        {
            var result = _customerResponses.ProcessDocumentSaving("6588652664550900", "Sample");
            return Ok("got successfully");
        }


        [HttpPost("webhookresponse")]
        [HttpGet("webhookresponse")]
        public async Task<IActionResult> Webhookresponse()
        {
            //string response = await new System.IO.StreamReader(Request.Body).ReadToEndAsync();            
            await _customerResponses.ProcessCustomerResponse(Request.Body);
            string token = Request.Query["hub.challenge"];
            return Ok(token);
        }

      
    }
}
