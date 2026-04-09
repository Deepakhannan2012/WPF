// ---------------------------------------------------------------------------------------
// Nexus ~ Separation of Concerns Demo
// Copyright (c) Trumpf Metamation India.
// ---------------------------------------------------------------------------------------
// FileDB.cs
// File-bases implementation of IDB<T>. Supports CSV and JSON formats.
// ---------------------------------------------------------------------------------------
using System.Reflection;
using System.Text.Json;
using Nexus.Core;

namespace Nexus.Data;

#region class FileDB<T> ----------------------------------------------------------------------------
/// <summary>A file-based implementation for entity type T. Supports CSV and JSON serialization formats</summary>
public class FileDB<T> : IDB<T> where T : class, IEntity, new() {

   #region Constructor -----------------------------------------------
   public FileDB (string filePath, EFileType type = EFileType.CSV) {
      mType = type; mFilePath = filePath;
   }
   #endregion

   #region IDB<T> implementation -------------------------------------
   /// <summary>Gets all entities from the file.</summary>
   /// <returns>A list of entities of type T</returns>
   public List<T> GetAll () {
      if (!File.Exists (mFilePath)) return [];
      try {
         return mType switch {
            EFileType.CSV => LoadCSV (),
            EFileType.Json => JsonSerializer.Deserialize<List<T>> (File.ReadAllText (mFilePath)) ?? [],
            _ => []
         };
      } catch { return []; }
   }

   /// <summary>Saves all entities to the file.</summary>
   /// <param name="items">The list of entities to save.</param>
   public void SaveAll (List<T> items) {
      switch (mType) {
         case EFileType.CSV: {
               using var sw = new StreamWriter (mFilePath);
               foreach (var item in items)
                  sw.WriteLine (string.Join (',',
                     sProps.Select (p => Escape (p.GetValue (item)?.ToString () ?? ""))));
               break;
            }
         case EFileType.Json:
            File.WriteAllText (mFilePath, JsonSerializer.Serialize (items));
            break;
      }
   }

   /// <summary>Loads the data from CSV file</summary>
   List<T> LoadCSV () {
      var lines = File.ReadAllLines (mFilePath);
      List<T> lst = [];
      foreach (var ln in lines) {
         var data = ln.Split (',');
         if (data.Length != sProps.Length) continue;
         var obj = new T ();
         for (int i = 0; i < data.Length; i++) {
            var prop = sProps[i];
            var value = Convert.ChangeType (data[i], prop.PropertyType);
            prop.SetValue (obj, value);
         }
         lst.Add (obj);
      }
      return lst;
   }

   string Escape (string s) => (s.Contains (',') || s.Contains ('"')) ? $"\"{s.Replace ("\"", "\"\"")}\"" : s;
   #endregion

   #region Nested Types ----------------------------------------------
   /// <summary>Supported file formats</summary>
   public enum EFileType { CSV, Json }
   #endregion

   #region Private Data ----------------------------------------------
   readonly EFileType mType;
   readonly string mFilePath;
   static readonly PropertyInfo[] sProps = typeof (T).GetProperties ();
   #endregion

}
#endregion