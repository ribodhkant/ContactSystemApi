namespace ContactManagementSystem.BusinessLib
{
    public interface IContactService
    {
        Response GetContactList();
        Response InsertAndUpdateContact(ContactDto contactDto);
        Response DeleteContact(int id);
    }
}
