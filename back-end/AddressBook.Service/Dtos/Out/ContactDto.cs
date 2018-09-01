namespace AddressBook.Service.Dtos.Out
{
   public class ContactDto: ContactCompactDto
    {
        public string Phone { get; set; }

        public string Email { get; set; }

        public string LinkedIn { get; set; }

        public string Skype { get; set; }
    }
}
