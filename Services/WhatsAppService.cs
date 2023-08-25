using Azure.Core;
using Microsoft.AspNetCore.DataProtection.KeyManagement;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion.Internal;
using Microsoft.Extensions.Options;
using Nancy;
using Nancy.Json;
using Nancy.Responses;
using Newtonsoft.Json;
using System.Collections;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Reflection;
using System.Runtime;
using System.Text;
using System.Text.Json;
using WhatsAppAPI.Data;
using WhatsAppAPI.IServices;
using WhatsAppAPI.MessageObjects;
using WhatsAppAPI.Models.Registration;
using WhatsAppAPI.Repository;
using WhatsAppAPI.ViewModels;
using WhatsAppAPI.ViewModels.Media;
using WhatsAppAPI.ViewModels.Messages;
using WhatsAppAPI.WhatsAppSettings;

namespace WhatsAppAPI.Services
{
    public class WhatsAppService : IWhatsAppService
    {

        public async Task<SentMessageResponseViewModel> SendRandomMessage(string mobile, string message)
        {
            try
            {
                using HttpClient httpClient = new();
                var token = "EAANQItmsIj0BAJUMETUPiKLUx3O02aNGf1LO5wL907mFmYE278rIZA29sVCRyJLEYZA0YYBx75oeKqZBFk7WLU6l5TdiqcBtTnAvdFHwNBHg3oZAf9LgB1IHHy4fEzZBFrWje18HhZCihWDBQ5v3R9946hw6MOLubKmkKCvDZBfVCZB0cZCo6Vl31AIUslb2fTFeksykQnf9KtxnUb9LSZB3Tnb6c08Ums6FUZD";
                var uri = "https://graph.facebook.com/v17.0/104092452770724/messages";

                WhatsAppMessageRequest whatsAppMessageRequest = new WhatsAppMessageRequest()
                {
                    messaging_product = "whatsapp",
                    recipient_type = "individual",
                    to = mobile,
                    type = "text",
                    text = new TextMessage()
                    {
                        preview_url = false,
                        body = message
                    }
                };

                HttpRequestMessage requestmessage = new HttpRequestMessage(HttpMethod.Post, uri);
                requestmessage.Headers.Authorization = new AuthenticationHeaderValue("Bearer", token);
                requestmessage.Content = new StringContent(Newtonsoft.Json.JsonConvert.SerializeObject(whatsAppMessageRequest), Encoding.UTF8, "application/json");

                var response = await httpClient.SendAsync(requestmessage).Result.Content.ReadAsStringAsync();

                SentMessageResponseViewModel? sentMessageResponseViewModel = JsonConvert.DeserializeObject<SentMessageResponseViewModel>(response);
                var responseWithTimeStamp = DateTime.Now.ToString() + response;
                //new AzureStorageService().SaveRequestToAzureFile(responseWithTimeStamp);
                return sentMessageResponseViewModel;
            }
            catch (Exception ex)
            {

                throw ex;
            }


        }
        public async Task<SentMessageResponseViewModel> SendTextMessage(TextMessageObject textMessageObject)
        {
            try
            {
                using HttpClient httpClient = new();
                var token = "EAANQItmsIj0BAJUMETUPiKLUx3O02aNGf1LO5wL907mFmYE278rIZA29sVCRyJLEYZA0YYBx75oeKqZBFk7WLU6l5TdiqcBtTnAvdFHwNBHg3oZAf9LgB1IHHy4fEzZBFrWje18HhZCihWDBQ5v3R9946hw6MOLubKmkKCvDZBfVCZB0cZCo6Vl31AIUslb2fTFeksykQnf9KtxnUb9LSZB3Tnb6c08Ums6FUZD";
                var uri = "https://graph.facebook.com/v17.0/104092452770724/messages";

                HttpRequestMessage requestmessage = new HttpRequestMessage(HttpMethod.Post, uri);
                requestmessage.Headers.Authorization = new AuthenticationHeaderValue("Bearer", token);
                requestmessage.Content = new StringContent(Newtonsoft.Json.JsonConvert.SerializeObject(textMessageObject), Encoding.UTF8, "application/json");
                var response = await httpClient.SendAsync(requestmessage).Result.Content.ReadAsStringAsync();

                SentMessageResponseViewModel? sentMessageResponseViewModel = JsonConvert.DeserializeObject<SentMessageResponseViewModel>(response);
                var responseWithTimeStamp = DateTime.Now.ToString() + response;
                //new AzureStorageService().SaveRequestToAzureFile(responseWithTimeStamp);
                return sentMessageResponseViewModel;
            }
            catch (Exception ex)
            {

                throw ex;
            }


        }
        public async Task<SentMessageResponseViewModel> SendListMessage(InteractiveListMessageObject interactiveListMessageObject)
        {
            try
            {
                using HttpClient httpClient = new();
                var token = "EAANQItmsIj0BAJUMETUPiKLUx3O02aNGf1LO5wL907mFmYE278rIZA29sVCRyJLEYZA0YYBx75oeKqZBFk7WLU6l5TdiqcBtTnAvdFHwNBHg3oZAf9LgB1IHHy4fEzZBFrWje18HhZCihWDBQ5v3R9946hw6MOLubKmkKCvDZBfVCZB0cZCo6Vl31AIUslb2fTFeksykQnf9KtxnUb9LSZB3Tnb6c08Ums6FUZD";
                var uri = "https://graph.facebook.com/v17.0/104092452770724/messages";

                HttpRequestMessage requestmessage = new HttpRequestMessage(HttpMethod.Post, uri);
                requestmessage.Headers.Authorization = new AuthenticationHeaderValue("Bearer", token);
                requestmessage.Content = new StringContent(Newtonsoft.Json.JsonConvert.SerializeObject(interactiveListMessageObject), Encoding.UTF8, "application/json");
                var response = await httpClient.SendAsync(requestmessage).Result.Content.ReadAsStringAsync();

                SentMessageResponseViewModel? sentMessageResponseViewModel = JsonConvert.DeserializeObject<SentMessageResponseViewModel>(response);
                var responseWithTimeStamp = DateTime.Now.ToString() + response;
                //new AzureStorageService().SaveRequestToAzureFile(responseWithTimeStamp);
                return sentMessageResponseViewModel;
            }
            catch (Exception ex)
            {

                throw ex;
            }


        }
        public async Task<SentMessageResponseViewModel> SendButtonMessage(InteractiveButtonMessageObject interactiveButtonMessageObject)
        {
            try
            {
                using HttpClient httpClient = new();
                var token = "EAANQItmsIj0BAJUMETUPiKLUx3O02aNGf1LO5wL907mFmYE278rIZA29sVCRyJLEYZA0YYBx75oeKqZBFk7WLU6l5TdiqcBtTnAvdFHwNBHg3oZAf9LgB1IHHy4fEzZBFrWje18HhZCihWDBQ5v3R9946hw6MOLubKmkKCvDZBfVCZB0cZCo6Vl31AIUslb2fTFeksykQnf9KtxnUb9LSZB3Tnb6c08Ums6FUZD";
                var uri = "https://graph.facebook.com/v17.0/104092452770724/messages";

                HttpRequestMessage httpRequestMessage = new HttpRequestMessage(HttpMethod.Post, uri);
                httpRequestMessage.Headers.Authorization = new AuthenticationHeaderValue("Bearer", token);
                httpRequestMessage.Content = new StringContent(JsonConvert.SerializeObject(interactiveButtonMessageObject), Encoding.UTF8, "application/json");
                var response = await httpClient.SendAsync(httpRequestMessage).Result.Content.ReadAsStringAsync();

                SentMessageResponseViewModel? sentMessageResponseViewModel = JsonConvert.DeserializeObject<SentMessageResponseViewModel>(response);
                var responseWithTimeStamp = DateTime.Now.ToString() + response;
                //new AzureStorageService().SaveRequestToAzureFile(responseWithTimeStamp);
                return sentMessageResponseViewModel;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task<string> GetMediaUrlById(string mediaId)
        {
            try
            {
                using HttpClient httpClient = new();
                var token = "EAANQItmsIj0BAJUMETUPiKLUx3O02aNGf1LO5wL907mFmYE278rIZA29sVCRyJLEYZA0YYBx75oeKqZBFk7WLU6l5TdiqcBtTnAvdFHwNBHg3oZAf9LgB1IHHy4fEzZBFrWje18HhZCihWDBQ5v3R9946hw6MOLubKmkKCvDZBfVCZB0cZCo6Vl31AIUslb2fTFeksykQnf9KtxnUb9LSZB3Tnb6c08Ums6FUZD";
                //involve seperate endpoint for media in appsettings
                var getmediauri = "https://graph.facebook.com/v17.0/" + mediaId;

                httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
                var response = await httpClient.GetAsync(getmediauri).Result.Content.ReadAsStringAsync();

                MediaUrlResponseVM? mediaUrlResponseVM = JsonConvert.DeserializeObject<MediaUrlResponseVM>(response);
                string mediaUrl = mediaUrlResponseVM.url;
                return mediaUrl;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task<string> GetMediaPathByUrl(string mediaUrl)
        {
            try
            {
                //using (FileStream fileStream = new FileStream(@"C:\WhatsAppFiles\myfiledocs", FileMode.Create, FileAccess.Write, FileShare.None, 100000000))
                //{
                //    await response.CopyToAsync(fileStream);
                //}              
                var client = new HttpClient();
                var request = new HttpRequestMessage(HttpMethod.Get, mediaUrl);
                request.Headers.Add("Authorization", "Bearer EAANQItmsIj0BAJUMETUPiKLUx3O02aNGf1LO5wL907mFmYE278rIZA29sVCRyJLEYZA0YYBx75oeKqZBFk7WLU6l5TdiqcBtTnAvdFHwNBHg3oZAf9LgB1IHHy4fEzZBFrWje18HhZCihWDBQ5v3R9946hw6MOLubKmkKCvDZBfVCZB0cZCo6Vl31AIUslb2fTFeksykQnf9KtxnUb9LSZB3Tnb6c08Ums6FUZD");
                string path = @"C:\WhatsAppFiles\myfiletest.pdf";

                var response = await client.GetAsync(mediaUrl).Result.Content.ReadAsStringAsync();

                return path;
            }
            catch (Exception ex) 
            {
                throw ex;
            }
        }


    }
}






   
    