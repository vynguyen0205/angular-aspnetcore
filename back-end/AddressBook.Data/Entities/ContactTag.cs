namespace AddressBook.Data.Entities
{
    public class ContactTag
    {
        public int ContactId { get; set; }

        public int TagId { get; set; }

        public virtual Contact Contact { get; set; }

        public virtual Tag Tag { get; set; }
    }
}
