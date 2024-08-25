using System.Diagnostics;
using ContactManagementSystem.BusinessLib;
using Newtonsoft.Json.Linq;

namespace ContactManagementSystem.NewFolder
{
    public class ContactService: IContactService
    {
        public Response GetContactList()
        {
            string json = System.IO.File.ReadAllText(Path.Combine(Directory.GetCurrentDirectory(), @"JsonFile\test.json"));
            var response = new Response();
            try
            {
                var jObject = JObject.Parse(json);
                if (jObject != null)
                {
                    JArray contactArrary = (JArray)jObject["ContactInfo"];
                    var listOfContacts = new List<ContactDto>();
                    foreach (var item in contactArrary)
                    {
                        var contact = new ContactDto();
                        contact.Id = Convert.ToInt32(item["Id"].ToString());
                        contact.FirstName = item["FirstName"].ToString();
                        contact.LastName = item["LastName"].ToString();
                        contact.Email = item["Email"].ToString();
                        listOfContacts.Add(contact);
                    }
                    response.contactList = listOfContacts;
                    response.Massege = "Successfully get contact list";
                    response.IsSuccess = true;
                }
                return response;
            }
            catch (Exception ex)
            {
                response.Massege = ex.Message;
                response.IsSuccess = false;
                return response;
                throw ex;
            }
        }
        public Response InsertAndUpdateContact(ContactDto contactDto)
        {
            var response = new Response();
            var filePath = Path.Combine(Directory.GetCurrentDirectory(), @"JsonFile\test.json");
            string json = System.IO.File.ReadAllText(filePath);
            try
            {
                var jObject = JObject.Parse(json);
                if (jObject != null)
                {
                    JArray contactArrary = (JArray)jObject["ContactInfo"];

                    //for update the existing record
                    if (contactDto.Id > 0)
                    {
                        foreach (var contact in contactArrary.Where(obj => obj["Id"].Value<int>() == contactDto.Id))
                        {
                            contact["FirstName"] = contactDto.FirstName;
                            contact["LastName"] = contactDto.LastName;
                            contact["Email"] = contactDto.Email;
                        }
                        response.Massege = "Contact updated successfully";
                        response.IsSuccess = true;
                    }

                    //for insert new record
                    else
                    {
                        contactDto.Id = Convert.ToInt32(contactArrary.OrderByDescending(c => c["Id"]).FirstOrDefault()["Id"].ToString()) + 1;
                        var newContact = "{ Id: " + contactDto.Id + ",FirstName: '" + contactDto.FirstName + "',LastName: '" + contactDto.LastName + "',Email: '" + contactDto.Email + "'}";
                        var newContacts = JObject.Parse(newContact);
                        contactArrary.Add(newContacts);
                        response.Massege = "Contact added successfully";
                        response.IsSuccess = true;
                    }

                    jObject["ContactInfo"] = contactArrary;
                    string newJsonResult = Newtonsoft.Json.JsonConvert.SerializeObject(jObject, Newtonsoft.Json.Formatting.Indented);
                    System.IO.File.WriteAllText(filePath, newJsonResult);
                }

                return response;
            }
            catch (Exception ex)
            {
                response.Massege = ex.Message;
                response.IsSuccess = false;
                return response;
                throw ex;
            }
        }
        public Response DeleteContact(int id)
        {
            var response = new Response();
            var filePath = Path.Combine(Directory.GetCurrentDirectory(), @"JsonFile\test.json");
            string json = System.IO.File.ReadAllText(filePath);
            try
            {
                var jObject = JObject.Parse(json);
                if (jObject != null)
                {
                    JArray contactArrary = (JArray)jObject["ContactInfo"];

                    if (id > 0)
                    {
                        var contactToDeleted = contactArrary.FirstOrDefault(obj => obj["Id"].Value<int>() == id);
                        contactArrary.Remove(contactToDeleted);
                        string newJsonResult = Newtonsoft.Json.JsonConvert.SerializeObject(jObject, Newtonsoft.Json.Formatting.Indented);
                        System.IO.File.WriteAllText(filePath, newJsonResult);
                        response.Massege = "Contact delete successfully";
                        response.IsSuccess = true;
                    }
                }
                return response;
            }
            catch (Exception ex)
            {
                response.IsSuccess = false;
                response.Massege = ex.Message;
                throw ex;
            }
        }
    }

    
}
