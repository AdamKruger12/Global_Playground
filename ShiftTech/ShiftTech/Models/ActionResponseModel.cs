namespace ShiftTech.Models
{
  public class ActionResponseModel
  {
    public string? responseCode { get; set; }
    public string? message { get; set; }

    public Company[]? companies { get; set; }
  }
}
