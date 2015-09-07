using TRex.Metadata;

namespace QuickLearn.Samples.NdefApiApp.Models
{
    public class VCardModel
    {

        [Metadata("Given Name", "The given name contained in the vCard data")]
        public string GivenName { get; set; }

        [Metadata("Family Name", "The family name contained in the vCard data")]
        public string FamilyName { get; set; }

        [Metadata("Email Address", "The first email address contained in the vCard data")]
        public string EmailAddress { get; set; }

    }
}
