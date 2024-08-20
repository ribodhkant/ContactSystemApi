namespace ContactManagementSystem
{
    public class Response
    {
      
        public string Massege { get; set; }
        public bool IsSuccess { get; set; }
        public List<ContactDto> contactList { get; set; }
    }
}
