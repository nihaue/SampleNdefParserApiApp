using NdefLibrary.Ndef;
using QuickLearn.Samples.NdefApiApp.Models;
using Swashbuckle.Swagger.Annotations;
using System;
using System.IO;
using System.Linq;
using System.Net;
using System.Web.Http;
using TRex.Metadata;
using VcardLibrary;

namespace QuickLearn.Samples.NdefApiApp.Controllers
{
    [RoutePrefix("vcard")]
    public class VCardController : ApiController
    {
        [Route, HttpGet]
        [Metadata("Read VCard from Tag",
            "Reads the first record within the NDEF message as a vCard and returns the name and email contained")]
        [SwaggerResponse(HttpStatusCode.OK, "vCard data successfully read from NDEF message", typeof(VCardModel))]
        [SwaggerResponse(HttpStatusCode.BadRequest, "Attempt to parse data failed")]
        public IHttpActionResult Get(string b64Content)
        {

            NdefMessage ndefMessage = null;

            try
            {
                ndefMessage = NdefMessage.FromByteArray(Convert.FromBase64String(b64Content));
            }
            catch
            {
                return BadRequest("Could not parse NDEF message");
            }

            var firstRecord = ndefMessage.FirstOrDefault();

            if (firstRecord.CheckSpecializedType(true) != typeof(NdefVcardRecordBase))
                return BadRequest("First record in NDEF message was not a vCard record");
            
            using (var contentStream = new MemoryStream(firstRecord.Payload))
            {
                using (var reader = new StreamReader(contentStream))
                {
                    vCard card = new vCard(reader);

                    return Ok(new VCardModel()
                    {
                        EmailAddress = card.EmailAddresses.Any() ? card.EmailAddresses.FirstOrDefault().Address : string.Empty,
                        FamilyName = card.FamilyName,
                        GivenName = card.GivenName

                    });
                }

            }
        }
    }
}
