
namespace Goke.Web.Data.Models;

using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

public partial class Card 
{
    public string Id { get; set; } = Guid.NewGuid().ToString();

    [Required(ErrorMessage = "The Pin is a mandatory Field.")]
    [Display(Name = "Pin")]
    public string Pin { get; set; } = "0000-0000-0000-0000";

    [Required(ErrorMessage = "The From is a mandatory Field.")]
    [DataType(DataType.Date)]
    [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
    [Display(Name = "From")]
    public System.DateTime From { get; set; } = DateTime.UtcNow;

    [Required(ErrorMessage = "The To is a mandatory Field.")]
	[DataType(DataType.Date)]
	[DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
	[Display(Name = "To")]
	public System.DateTime To { get; set; } = DateTime.UtcNow.AddDays(1);

    [Required(ErrorMessage = "The Permission is a mandatory Field.")]
	[Display(Name = "Permission")]
	public short Permission { get; set; }

    public string? OwnerId { get; set; }
    public ApplicationUser?  Owner { get; set; }
    
    public string ToRecord()
    {
        var str = $@"Card {{ 
                Id = {Id},                 
                Pin = {Pin}, 
                From = {From}, 
                To = {To}, 
                Permission = {Permission}, 
                OwnerId= {OwnerId},
            ";
     
        OnToRecord(ref str);

        str += "}}";

        return str;
    }

    partial void OnToRecord(ref string str);        

    public string ToJson()
    {
        OnToJson();
        return System.Text.Json.JsonSerializer.Serialize(this);
    }
    partial void OnToJson();   

}
