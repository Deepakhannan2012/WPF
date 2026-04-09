// ---------------------------------------------------------------------------------------
// Nexus ~ Separation of Concerns Demo
// Copyright (c) Trumpf Metamation India.
// ---------------------------------------------------------------------------------------
// User.cs
// The User class
// ---------------------------------------------------------------------------------------
using System.ComponentModel.DataAnnotations;

namespace Nexus.Core;

#region class Book ---------------------------------------------------------------------------------
/// <summary>The Book class</summary>
public class Book : IEntity {
   /// <summary>The ID of the book</summary>
   public int ID { get; set; }
   /// <summary>The title of the book</summary>
   [Required]
   public string Title { get; set; }
   /// <summary>The author of the book</summary>
   [Required]
   public string AuthorName { get; set; }
   /// <summary>The price of the book</summary>
   [Required]
   public int Price { get; set; }
   /// <summary>The publishing date of the book</summary>
   [Required]
   [RegularExpression (@"^(0[1-9]|[12][0-9]|3[01])/(0[1-9]|1[0-2])/\d{4}$", ErrorMessage = "Enter date in dd/mm/yyyy format")]
   public string PublishDate { get; set; }
   /// <summary>The publisher of the book</summary>
   [Required]
   public string Publisher { get; set; }
}
#endregion