using Newtonsoft.Json;
using ShiftTech.Models.Context;

namespace ShiftTech.Models
{
  public class InMemoryFunctions
  {
    public static async Task<ActionResponseModel> SaveCard(string number, string company,CardContext _context) 
    {
      try
      {
        Guid cardUUID = Guid.NewGuid();

          await _context.Cards!.AddAsync(new Card
          {
            id = cardUUID.ToString(),
            cardCompany = company,
            number = long.Parse(number),
            saveDate = DateTime.Now
          });
          await _context.SaveChangesAsync();
        
        return new ActionResponseModel() {responseCode = "00", message = "Card Saved Successfuly" };
      }
      catch (Exception ex) 
      { 
        return new ActionResponseModel() {responseCode = "01", message = ex.Message };
      }
    }

    public static async Task<ActionResponseModel> GetConfigList(CardContext _context)
    {
      try
      {

        return new ActionResponseModel() { responseCode = "00", message = "Config list retrieved", companies = _context.Company!.ToArray() };
      }catch(Exception ex) 
      {
        return new ActionResponseModel() { responseCode = "01", message = ex.Message};
      }
    }
  }
}
