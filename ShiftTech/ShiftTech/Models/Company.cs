namespace ShiftTech.Models
{
  /// <summary>
  /// Card company data based on ISO/IEC 7812 standard
  /// </summary>
  public class Company
  {
    public int Id { get; set; }
    public string name { get; set; }
    public string length { get; set; }
    public string prefixes { get; set; }
    public bool checkDigit { get; set; }
  }
}
