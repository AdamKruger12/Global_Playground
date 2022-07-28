using ShiftTech.Models.Context;

namespace ShiftTech.Models.Database
{
  public static class DbInitializer
  {
    /// <summary>
    /// Initializes the in memory database with some basic information to make testing the frontend easier.
    /// </summary>
    /// <param name="context"></param>
    public static void Initialize(CardContext context) 
    {
      context.Database.EnsureCreated();

      if (context.Company!.Any())
      {
        return;
      }
      //this is just to populate some companies for the config page for me to use.
      var companies = new Company[]
      {
        new Company{name= "Visa", length="13,16",prefixes="4", checkDigit=true },
        new Company{name= "MasterCard", length="16",prefixes="51,52,53,54,55", checkDigit=true },
        new Company{name= "DinersClub", length="14,16",prefixes="36,38,54,55", checkDigit=true },
        new Company{name= "CarteBlanche", length="14",prefixes="300,301,302,303,304,305", checkDigit=true },
        new Company{name= "AmEx", length="15",prefixes="34,37", checkDigit=true },
        new Company{name= "Discover", length="16",prefixes="6011,622,64,65", checkDigit=true },
        new Company{name= "JCB", length="16",prefixes="35", checkDigit=true },
        new Company{name= "enRoute", length="15",prefixes="2014,2149", checkDigit=true },
        new Company{name= "Solo", length="16,18,19",prefixes="6334,6767", checkDigit=true },
        new Company{name= "Switch", length="16,18,19",prefixes="4903,4905,4911,4936,564182,633110,6333,6759", checkDigit=true },
        new Company{name= "Maestro", length="12,13,14,15,16,18,19",prefixes="5018,5020,5038,6304,6759,6761,6762,6763", checkDigit=true },
        new Company{name= "VisaElectron", length="16",prefixes="4026,417500,4508,4844,4913,4917", checkDigit=true },
        new Company{name= "LaserCard", length="16,17,18,19",prefixes="6304,6706,6771,6709", checkDigit=true },
      };

      var companies1 = new Company[]
     {
        new Company{name= "Visa", length="13,16",prefixes="4", checkDigit=true },
        new Company{name= "MasterCard", length="16",prefixes="51,52,53,54,55", checkDigit=true },
     };

      foreach (var company in companies)
      {
        context.Company!.Add(company);
      }
      context.SaveChanges();

    }
  }
}
