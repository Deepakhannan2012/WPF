// ---------------------------------------------------------------------------------------
// Nexus ~ Separation of Concerns Demo
// Copyright (c) Trumpf Metamation India.
// ---------------------------------------------------------------------------------------
// UserVM.cs
// View Model for the User
// ---------------------------------------------------------------------------------------
using Nexus.Core;
using Nexus.Data;

namespace Nexus.App;

#region Class UserVM -------------------------------------------------------------------------------
/// <summary>View Model class for the Book</summary>
public class BookVM : EntityVM<Book> {

   #region Constructor -----------------------------------------------
   public BookVM (Book u, Hub<Book> m) : base (u, m) { mBook = u; }
   #endregion

   #region Properties ------------------------------------------------
   /// <summary>Title of the book</summary>
   public string Title {
      get => mBook.Title;
      set {
         if (mBook.Title != value) {
            mBook.Title = value;
            Notify ();
         }
      }
   }

   /// <summary>Author of the book</summary>
   public string AuthorName {
      get => mBook.AuthorName;
      set {
         if (mBook.AuthorName != value) {
            mBook.AuthorName = value;
            Notify ();
         }
      }
   }

   /// <summary>Price of the book</summary>
   public int Price {
      get => mBook.Price;
      set {
         if (mBook.Price != value) {
            mBook.Price = value;
            Notify ();
         }
      }
   }

   /// <summary>Publishing date of the book</summary>
   public string PublishDate {
      get => mBook.PublishDate;
      set {
         if (mBook.PublishDate != value) {
            mBook.PublishDate = value;
            Notify ();
         }
      }
   }

   /// <summary>Publisher of the book</summary>
   public string Publisher {
      get => mBook.Publisher;
      set {
         if (mBook.Publisher != value) {
            mBook.Publisher = value;
            Notify ();
         }
      }
   }
   #endregion

   #region Private Data ----------------------------------------------
   Book mBook;
   #endregion

}
#endregion